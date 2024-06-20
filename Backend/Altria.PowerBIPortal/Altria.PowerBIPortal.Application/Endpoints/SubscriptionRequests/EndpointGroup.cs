using Altria.PowerBIPortal.Application.Infrastructure;

namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests;

public class EndpointGroup : IEndpointGroup
{
    public IEndpointRouteBuilder Configure(IEndpointRouteBuilder app)
    {
        return app
            .MapGroup("/subscriptionRequests")
            .WithTags("Subscriptions")
            .WithOpenApi();
    }
}
