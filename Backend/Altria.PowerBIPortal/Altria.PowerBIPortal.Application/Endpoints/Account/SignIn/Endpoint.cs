using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Altria.PowerBIPortal.Application.Endpoints.Account.SignIn;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapPost("/signIn",
            async (SignInModel request,
            UserManager<User> userManager, SignInManager<User> signInManager) =>
            {

                // Todo:- Implement LDAP relate implementation 

                var user = await userManager.FindByEmailAsync(request.UserName);
                if (user == null)
                {
                    user = User.Create("", "", request.UserName);
                    await userManager.CreateAsync(user);

                    var loginInfo = new UserLoginInfo("LDAP", request.UserName, "LDAP");
                    await userManager.AddLoginAsync(user, loginInfo);
                }                

                await signInManager.SignInAsync(user, true);
            });
    }
}
