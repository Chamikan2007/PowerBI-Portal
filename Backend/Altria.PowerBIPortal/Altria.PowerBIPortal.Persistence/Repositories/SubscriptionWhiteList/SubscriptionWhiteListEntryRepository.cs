using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionWhiteList;
using Altria.PowerBIPortal.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Altria.PowerBIPortal.Persistence.Repositories.SubscriptionWhiteList;

public class SubscriptionWhiteListEntryRepository : Repository<SubscriptionWhiteListEntry>, ISubscriptionWhiteListEntryRepository
{
    public SubscriptionWhiteListEntryRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Task<bool> IsAllowedEntryAsync(string email, string domain)
    {
        return _readOnlyStore.AnyAsync(e => (e.EntryType == SubscriptionWhiteListEntryType.Domain && string.Equals(e.WhiteListEntry, domain)) ||
                                            (e.EntryType == SubscriptionWhiteListEntryType.Email && string.Equals(e.WhiteListEntry, email)));
    }
}
