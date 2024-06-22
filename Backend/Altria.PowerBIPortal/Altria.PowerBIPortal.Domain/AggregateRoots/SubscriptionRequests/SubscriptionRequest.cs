using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules.Enums;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;

public class SubscriptionRequest : ApprovalRequest<SubscriptionRequestApprovalLevel>
{
    private SubscriptionRequest()
    {
    }

    public required string ReportPath { get; set; }

    public required SubscrptionInfo SubscrptionInfo { get; set; }

    public Schedule? Schedule { get; set; }

    public Guid? SharedScheduleReference { get; set; }

    public bool IsProcessed { get; private set; }

    public Guid? SubscriptionReference { get; private set; }

    public static SubscriptionRequest Create(string reportPath, SubscrptionInfo subscrptionInfo, Schedule schedule , User requester)
    {
        var subscription = new SubscriptionRequest
        {
            Requester = requester,
            ReportPath = reportPath,
            SubscrptionInfo = subscrptionInfo,
            Schedule = schedule
        };

        subscription.ApprovalRequestLevels.Add(new SubscriptionRequestApprovalLevel { Subscription = subscription, ApprovalLevel = 1 });

        return subscription;
    }

    public new void Approved(User approvalOfficer, SubscriptionRequestApprovalLevel currentApprovalLevel)
    {
        base.Approved(approvalOfficer, currentApprovalLevel);

        switch (currentApprovalLevel.ApprovalLevel)
        {
            case 1:
                ApprovalRequestLevels.Add(new SubscriptionRequestApprovalLevel { Subscription = this, ApprovalLevel = currentApprovalLevel.ApprovalLevel + 1 });
                break;

            case 2:
                Status = ApprovalStatus.Approved;
                break;
        }
    }

    internal void Processed(Guid subscritionId)
    {
        if (IsProcessed)
        {
            return;
        }

        IsProcessed = true;
        SubscriptionReference = subscritionId;
    }
}
