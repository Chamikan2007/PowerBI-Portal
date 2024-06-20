using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;
using Microsoft.EntityFrameworkCore;

namespace Altria.PowerBIPortal.Persistence.Repositories.SubscriptionRequests;

public class SubscriptionRequestRepository : Repository<SubscriptionRequest>, ISubscriptionRequestRepository
{
    public SubscriptionRequestRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Task<SubscriptionRequest?> FetchByIdAsync(Guid requestId, bool includeApprovalLevels = false)
    {
        var store = includeApprovalLevels ? _store.Include(r => r.ApprovalRequestLevels) : _store.Select(s => s);
        return store.FirstOrDefaultAsync(r => r.Id == requestId);
    }

    public Task<SubscriptionRequest?> GetByIdAsync(Guid requestId)
    {
        return _readOnlyStore
            .Include(s => s.Requester)
            .Include(r => r.ApprovalRequestLevels)
                .ThenInclude(l => l.ApprovalOfficer)
            .FirstOrDefaultAsync(r => r.Id == requestId);
    }

    public void Create(SubscriptionRequest subscription)
    {
        _store.Add(subscription);
    }

    public Task<List<SubscriptionRequest>> GeSubscritionRequestsToApproveAsync(Guid approvalOfficeId, int[] applicableApprovalLevels, bool includeAll)
    {
        return _readOnlyStore.Include(s => s.ApprovalRequestLevels)
                .Where(a => includeAll || a.Status != ApprovalStatus.Cancelled)
                .SelectMany(a => a.ApprovalRequestLevels).Include(l => l.ApprovalOfficer).Include(l => l.Subscription.Requester)
                .Where(l => l.Status == ApprovalStatus.Pending && applicableApprovalLevels.Contains(l.ApprovalLevel) ||
                            includeAll && l.ApprovalOfficer != null && l.ApprovalOfficer.Id == approvalOfficeId)
                .Select(l => l.Subscription).Distinct()
                .ToListAsync();
    }

    public Task<List<SubscriptionRequest>> GeMySubscritionRequestsAsync(Guid requesterId, bool includeAll)
    {
        return _readOnlyStore.Include(s => s.Requester)
            .Where(s => s.Requester.Id == requesterId && (includeAll || s.Status == ApprovalStatus.Pending))
            .ToListAsync();
    }

    public Task<List<SubscriptionRequest>> FetchSubscriptionsToProcessAsync()
    {
        return _store.Where(s => s.Status == ApprovalStatus.Approved && !s.IsProcessed).ToListAsync();
    }
}
