using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriberWhiteListEntries;

public static class SubscriberWhiteListErrors
{
    public static Error NotAllowed => new("SubscriberWhiteList.NotAllowed");
}
