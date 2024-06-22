using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos.Abstractions;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;

public class DataDrivenSubscription
{
    public required string Description { get; set; }

    public required DeliveryOption DeliveryOption { get; set; }
}
