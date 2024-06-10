using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionApprovals;
using Altria.PowerBIPortal.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Altria.PowerBIPortal.Persistence.Repositories.SubscriptionRequests;

public class SubscriptionRequestRepository : Repository<SubscriptionRequest>, ISubscriptionRequestRepository
{
    public SubscriptionRequestRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Task<SubscriptionRequest?> GetByIdAsync(Guid requestId)
    {
        return _store.Include(r => r.ApprovalRequestSteps).FirstOrDefaultAsync(r => r.Id == requestId);
    }
}
