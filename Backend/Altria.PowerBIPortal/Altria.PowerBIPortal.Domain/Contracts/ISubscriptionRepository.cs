using Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;

namespace Altria.PowerBIPortal.Domain.Contracts;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByIdAsync(Guid requestId);

    void Create(Subscription subscription);
}
