using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionApprovals;

public class SubscriptionApprovalLevel : ApprovalRequestLevel
{
    public static SubscriptionApprovalLevel Create(int approvalLevel)
    {
        return new SubscriptionApprovalLevel { ApprovalLevel = approvalLevel };
    }
}
