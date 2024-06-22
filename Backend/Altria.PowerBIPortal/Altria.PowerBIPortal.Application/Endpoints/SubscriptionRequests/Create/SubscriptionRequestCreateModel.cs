using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules.Abstractions;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos.Abstractions;

namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests.Create;

public class SubscriptionRequestCreateModel
{
    public required string ReportPath { get; set; }

    public required string Email { get; set; }

    public required SubscrptionInfo SubscrptionInfo { get; set; }

    public required Schedule Schedule { get; set; }
}
