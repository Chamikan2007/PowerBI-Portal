using Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;
using Altria.PowerBIPortal.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Altria.PowerBIPortal.Persistence.Repositories.Subscriptions;

public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
{
    public SubscriptionRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Task<Subscription?> GetByIdAsync(Guid requestId)
    {
        return _store.Include(r => r.ApprovalRequestSteps).FirstOrDefaultAsync(r => r.Id == requestId);
    }

    public void Create(Subscription subscription)
    {
        _store.Add(subscription);
    }
}
