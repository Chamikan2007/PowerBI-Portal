namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;

public class SubscrptionInfo
{
    public StandardSubscription? StandardSubscription { get; set; }

    public DataDrivenSubscription? DataDrivenSubscription { get; set; }
}
