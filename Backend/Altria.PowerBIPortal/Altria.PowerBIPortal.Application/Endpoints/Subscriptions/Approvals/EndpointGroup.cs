using Altria.PowerBIPortal.Application.Infrastructure;

namespace Altria.PowerBIPortal.Application.Endpoints.Subscriptions.Approvals;

public class EndpointGroup : IEndpointGroup
{
    public IEndpointRouteBuilder Configure(IEndpointRouteBuilder app)
    {
        return app
            .MapGroup("/subscriptions/approvals")
            .WithTags("Approvals")
            .WithOpenApi();
    }
}
