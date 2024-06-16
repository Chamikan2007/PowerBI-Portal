using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;

public class SubscriptionApprovalLevel : ApprovalRequestLevel
{
    public Subscription Subscription { get; init; }

    public static SubscriptionApprovalLevel Create(Subscription subscription, int approvalLevel)
    {
        return new SubscriptionApprovalLevel { Subscription = subscription, ApprovalLevel = approvalLevel };
    }
}
