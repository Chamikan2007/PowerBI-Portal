using Altria.PowerBIPortal.Application.Endpoints.Subscriptions;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;

namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests.Create;

public class SubscriptionRequestCreateModel
{
    public required ReportModel Report { get; set; }

    //public required SubscrptionInfo SubscrptionInfo { get; set; }

    //public required Schedule Schedule { get; set; }
}
