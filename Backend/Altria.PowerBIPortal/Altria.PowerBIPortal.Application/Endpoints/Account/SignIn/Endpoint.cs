using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Altria.PowerBIPortal.Application.Endpoints.Account.SignIn;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapPost("/signIn",
            async (SignInModel request,
            UserManager<User> userManager, IExternalUserAuthenticator externalUserAuthenticator, SignInManager<User> signInManager) =>
            {
                var result = externalUserAuthenticator.AuthenticateUser(request.UserName, request.Password);
                if (!result.IsAuthenticated)
                {
                    return Result.Faliour(IdentityErrors.InvalidLoginDetails);
                }

                var user = await userManager.FindByEmailAsync(result.User!.Email);
                if (user == null)
                {
                    user = User.Create(result.User!.DisplayName, result.User!.Email);
                    await userManager.CreateAsync(user);

                    var loginInfo = new UserLoginInfo("LDAP", result.User!.ExternalId, "LDAP");
                    await userManager.AddLoginAsync(user, loginInfo);
                }
                var roles = await userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Role, roles != null ? string.Join(",", roles) : string.Empty),
                };

                await signInManager.SignInAsync(user, true);

                return Result.Success();
            });
    }
}
