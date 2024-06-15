using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;

public class SubscriptionApprovalLevel : ApprovalRequestLevel
{
    public SubscriptionApprovalLevel(Subscription subscription)
    {
        Subscription = subscription;
    }

    public Subscription Subscription { get; }

    public static SubscriptionApprovalLevel Create(Subscription subscription, int approvalLevel)
    {
        return new SubscriptionApprovalLevel(subscription) { ApprovalLevel = approvalLevel };
    }
}
