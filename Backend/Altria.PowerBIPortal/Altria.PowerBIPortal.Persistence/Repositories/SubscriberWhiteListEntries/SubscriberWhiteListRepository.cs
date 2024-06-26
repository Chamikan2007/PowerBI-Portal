﻿using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriberWhiteListEntries;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Altria.PowerBIPortal.Persistence.Repositories.SubscriberWhiteListEntries;

public class SubscriberWhiteListRepository : Repository<SubscriberWhiteList>, ISubscriberWhiteListEntryRepository
{
    public SubscriberWhiteListRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Task<bool> IsAllowedEntryAsync(string email, string domain)
    {
        return _readOnlyStore.AnyAsync(e => e.EntryType == SubscriberWhiteListType.Domain && string.Equals(e.WhiteListEntry, domain) ||
                                            e.EntryType == SubscriberWhiteListType.Email && string.Equals(e.WhiteListEntry, email));
    }

    public Task<bool> IsAllowedEntryAsync(string[] emails, string[] domains)
    {
        return _readOnlyStore.AnyAsync(e => e.EntryType == SubscriberWhiteListType.Domain && domains.All(d => e.WhiteListEntry.Contains(d)) ||
                                            e.EntryType == SubscriberWhiteListType.Email && emails.All(d => e.WhiteListEntry.Contains(d)));
    }
}
