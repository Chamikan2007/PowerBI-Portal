using Altria.PowerBIPortal.Application.Infrastructure;

namespace Altria.PowerBIPortal.Application.Endpoints.Users;

public class EndpointGroup : IEndpointGroup
{
    public IEndpointRouteBuilder Configure(IEndpointRouteBuilder app)
    {
        return app
            .MapGroup("/users")
            .WithTags("Users")
            .WithOpenApi();
    }
}
