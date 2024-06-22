using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;

public class StandardSubscription : IJsonEntity
{
    public required string Description { get; set; }

    public required DeliveryOption DeliveryOption { get; set; }
}
