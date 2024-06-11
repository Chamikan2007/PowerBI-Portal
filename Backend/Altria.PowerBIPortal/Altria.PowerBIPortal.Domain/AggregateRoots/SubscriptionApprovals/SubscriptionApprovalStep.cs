using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionApprovals;

public class SubscriptionApprovalStep : ApprovalRequestStep
{
    public static SubscriptionApprovalStep Create(int stepIndex)
    {
        return new SubscriptionApprovalStep { StepIndex = stepIndex };
    }
}
