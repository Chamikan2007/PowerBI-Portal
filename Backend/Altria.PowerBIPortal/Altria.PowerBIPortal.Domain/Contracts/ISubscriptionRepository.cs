using Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;

namespace Altria.PowerBIPortal.Domain.Contracts;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByIdAsync(Guid requestId);

    void Create(Subscription subscription);

    Task<List<Subscription>> GeSubscritionRequestsToApproveAsync(Guid approvalOfficeId, int[] applicableApprovalLevels, bool includeAll);
}
