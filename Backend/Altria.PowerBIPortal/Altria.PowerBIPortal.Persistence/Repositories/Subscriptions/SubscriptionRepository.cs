using Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Altria.PowerBIPortal.Persistence.Repositories.Subscriptions;

public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
{
    public SubscriptionRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Task<Subscription?> FetchByIdAsync(Guid requestId, bool includeApprovalLevels = false)
    {
        var store = includeApprovalLevels ? _store.Include(r => r.ApprovalRequestLevels) : _store.Select(s => s);
        return store.FirstOrDefaultAsync(r => r.Id == requestId);
    }

    public Task<Subscription?> GetByIdAsync(Guid requestId)
    {
        return _readOnlyStore
            .Include(s => s.Requester)
            .Include(r => r.ApprovalRequestLevels)
                .ThenInclude(l => l.ApprovalOfficer)
            .FirstOrDefaultAsync(r => r.Id == requestId);
    }

    public void Create(Subscription subscription)
    {
        _store.Add(subscription);
    }

    public Task<List<Subscription>> GeSubscritionRequestsToApproveAsync(Guid approvalOfficeId, int[] applicableApprovalLevels, bool includeAll)
    {
        return _readOnlyStore.Include(s => s.ApprovalRequestLevels)
                .SelectMany(a => a.ApprovalRequestLevels).Include(l => l.ApprovalOfficer).Include(l => l.Subscription.Requester)
                .Where(l => (l.Status == ApprovalStatus.Pending && applicableApprovalLevels.Contains(l.ApprovalLevel)) ||
                            (includeAll && l.ApprovalOfficer != null && l.ApprovalOfficer.Id == approvalOfficeId))
                .Select(l => l.Subscription).Distinct()
                .Where(a => includeAll || a.Status != ApprovalStatus.Cancelled)
                .ToListAsync();
    }

    public Task<List<Subscription>> GeMySubscritionRequestsAsync(Guid requesterId, bool includeAll)
    {
        return _readOnlyStore.Include(s => s.Requester)
            .Where(s => s.Requester.Id == requesterId && (includeAll || s.Status == ApprovalStatus.Pending))
            .ToListAsync();
    }
}
