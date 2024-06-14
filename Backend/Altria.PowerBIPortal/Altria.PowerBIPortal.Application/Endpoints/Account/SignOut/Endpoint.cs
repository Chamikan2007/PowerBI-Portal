using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Altria.PowerBIPortal.Application.Endpoints.Account.SignOut;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapPost("/signout", async (SignInManager<User> signInManager) =>
        {
            await signInManager.SignOutAsync();
        })
            .RequireAuthorization("AuthenticatedUser");
    }
}
