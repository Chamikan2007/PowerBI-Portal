using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;

public class SubscrptionInfo : IJsonEntity
{
    public StandardSubscription? StandardSubscription { get; set; }

    public DataDrivenSubscription? DataDrivenSubscription { get; set; }
}
