using Altria.PowerBIPortal.Application.Infrastructure;

namespace Altria.PowerBIPortal.Application.Endpoints.Subscriptions;

public class EndpointGroup : IEndpointGroup
{
    public IEndpointRouteBuilder Configure(IEndpointRouteBuilder app)
    {
        return app
            .MapGroup("/subscriptions")
            .WithTags("Subscription Requests")
            .WithOpenApi();
    }
}
