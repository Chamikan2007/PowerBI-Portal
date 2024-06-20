using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;

public class SubscriptionRequestApprovalLevel : ApprovalRequestLevel
{
    public required SubscriptionRequest Subscription { get; init; }
}
