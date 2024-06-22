using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos.Abstractions;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;

public class StandardSubscription
{
    public required string Description { get; set; }

    public required DeliveryOption DeliveryOption { get; set; }
}
