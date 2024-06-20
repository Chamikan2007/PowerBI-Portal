using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;

public class SubscriptionRequest : ApprovalRequest<SubscriptionRequestApprovalLevel>
{
    private SubscriptionRequest()
    {
    }

    public required string ReportPath { get; set; }

    public required string Email { get; set; }

    public bool IsProcessed { get; set; }

    public Guid? SubscriptionId { get; set; }

    public static SubscriptionRequest Create(string reportPath, string email, User requester)
    {
        var subscription = new SubscriptionRequest() { ReportPath = reportPath, Email = email, Requester = requester };
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
        SubscriptionId = subscritionId;
    }
}
