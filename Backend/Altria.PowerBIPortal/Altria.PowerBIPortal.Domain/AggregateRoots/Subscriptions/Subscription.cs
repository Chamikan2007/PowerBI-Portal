using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;

public class Subscription : ApprovalRequest<SubscriptionApprovalLevel>
{
    private Subscription() : base(ApprovalRequestType.SubscriptionApproval)
    {
    }

    public required string Report { get; set; }

    public required string Email { get; set; }

    public static Subscription Create(string report, string email, User requester)
    {
        var subscription =  new Subscription() { Report = report, Email = email, Requester = requester };
        subscription.ApprovalRequestSteps.Add(SubscriptionApprovalLevel.Create(1));

        return subscription;
    }

    public new void Approved(User approvalOfficer, SubscriptionApprovalLevel currentApprovalLevel)
    {
        base.Approved(approvalOfficer, currentApprovalLevel);

        switch (currentApprovalLevel.ApprovalLevel)
        {
            case 1:
                ApprovalRequestSteps.Add(SubscriptionApprovalLevel.Create(currentApprovalLevel.ApprovalLevel + 1));
                break;

            case 2:
                Status = ApprovalStatus.Approved;
                break;
        }
    }
}
