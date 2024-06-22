using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriberWhiteListEntries;

public class SubscriberWhiteList : AggregateRoot
{
    public required string WhiteListEntry { get; set; }

    public SubscriberWhiteListType EntryType { get; set; }
}
