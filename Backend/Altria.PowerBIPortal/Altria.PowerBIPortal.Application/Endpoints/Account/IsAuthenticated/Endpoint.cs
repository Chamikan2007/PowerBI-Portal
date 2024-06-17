using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain;

namespace Altria.PowerBIPortal.Application.Endpoints.Account.IsAuthenticated;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapGet("/IsAuthenticated", (HttpContext context, RequestContext requestContext) =>
        {
            if (!context.User.Identity!.IsAuthenticated)
            {
                return Result<IsAuthenticatedResponse>.Success(new IsAuthenticatedResponse());
            }

            var requestContextModel = new RequestContextModel
            {
                UserId = requestContext.UserId,
                DisplayName = requestContext.DisplayName,
                Email = requestContext.Email,
                Roles = requestContext.Roles,
            };
            return Result<IsAuthenticatedResponse>.Success(new IsAuthenticatedResponse { IsAuthenticated = true, RequestContext = requestContextModel });
        });
    }
}
