namespace Altria.PowerBIPortal.Domain.Contracts;

public interface ISubscriptionWhiteListEntryRepository
{
    Task<bool> IsAllowedEntryAsync(string email, string domain);
}
