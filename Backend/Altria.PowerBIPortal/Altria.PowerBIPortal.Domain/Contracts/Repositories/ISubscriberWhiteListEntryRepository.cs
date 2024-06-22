namespace Altria.PowerBIPortal.Domain.Contracts.Repositories;

public interface ISubscriberWhiteListEntryRepository
{
    Task<bool> IsAllowedEntryAsync(string email, string domain);
}
