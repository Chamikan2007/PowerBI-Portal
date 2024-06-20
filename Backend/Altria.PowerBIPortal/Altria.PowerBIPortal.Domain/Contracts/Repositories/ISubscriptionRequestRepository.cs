using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;

namespace Altria.PowerBIPortal.Domain.Contracts.Repositories;

public interface ISubscriptionRequestRepository
{
    Task<SubscriptionRequest?> FetchByIdAsync(Guid requestId, bool includeApprovalLevels = false);

    Task<SubscriptionRequest?> GetByIdAsync(Guid requestId);

    void Create(SubscriptionRequest subscription);

    Task<List<SubscriptionRequest>> GeSubscritionRequestsToApproveAsync(Guid approvalOfficeId, int[] applicableApprovalLevels, bool includeAll);

    Task<List<SubscriptionRequest>> GeMySubscritionRequestsAsync(Guid requesterId, bool includeAll);

    Task<List<SubscriptionRequest>> FetchSubscriptionsToProcessAsync();
}
