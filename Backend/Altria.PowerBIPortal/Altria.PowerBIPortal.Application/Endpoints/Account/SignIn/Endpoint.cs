using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;
using Microsoft.AspNetCore.Identity;

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

                await signInManager.SignInAsync(user, true);

                return Result.Success();
            });
    }
}
