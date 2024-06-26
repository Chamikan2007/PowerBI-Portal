using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;

public class SubscriptionRequest : ApprovalRequest<SubscriptionRequestApprovalLevel>
{
    private SubscriptionRequest()
    {
    }

    public required string ReportPath { get; set; }

    public required string Owner { get; set; }

    public required SubscrptionInfo SubscrptionInfo { get; set; }

    public required DeliveryOption DeliveryOption { get; set; }

    public Schedule? Schedule { get; set; }

    public Guid? SharedScheduleReference { get; set; }

    public bool IsProcessed { get; private set; }

    public Guid? SubscriptionReference { get; private set; }

    public static SubscriptionRequest Create(string reportPath, string owner, SubscrptionInfo subscrptionInfo, Schedule schedule, DeliveryOption deliveryOption, User requester)
    {
        var subscription = new SubscriptionRequest
        {
            Requester = requester,
            ReportPath = reportPath,
            Owner = owner,
            SubscrptionInfo = subscrptionInfo,
            DeliveryOption = deliveryOption,
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
