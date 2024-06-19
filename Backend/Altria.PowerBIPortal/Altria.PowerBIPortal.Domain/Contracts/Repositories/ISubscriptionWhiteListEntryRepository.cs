namespace Altria.PowerBIPortal.Domain.Contracts.Repositories;

public interface ISubscriptionWhiteListEntryRepository
{
    Task<bool> IsAllowedEntryAsync(string email, string domain);
}
