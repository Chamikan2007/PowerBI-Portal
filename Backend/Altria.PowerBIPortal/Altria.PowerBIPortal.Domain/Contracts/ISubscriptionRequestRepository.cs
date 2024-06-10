using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionApprovals;

namespace Altria.PowerBIPortal.Domain.Contracts;

public interface ISubscriptionRequestRepository
{
    Task<SubscriptionRequest?> GetByIdAsync(Guid requestId);
}
