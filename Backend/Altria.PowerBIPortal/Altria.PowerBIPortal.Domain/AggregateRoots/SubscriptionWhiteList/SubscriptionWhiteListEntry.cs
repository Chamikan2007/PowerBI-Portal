using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionWhiteList;

public class SubscriptionWhiteListEntry : AggregateRoot
{
    public required string WhiteListEntry { get; set; }

    public SubscriptionWhiteListEntryType EntryType { get; set; }
}
