using Altria.PowerBIPortal.Application.Endpoints.Subscriptions;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules.Enums;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;

namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests;

public class SubscriptionRequestModel
{
    public required ReportModel Report { get; set; }

    public required SubscrptionInfo SubscrptionInfo { get; set; }

    public ScheduleType ScheduleType { get; set; }

    public required DeliveryOption DeliveryOption { get; set; }

    public required Schedule Schedule { get; set; }
}
