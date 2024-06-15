using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionWhiteList;

public static class SubscriptionWhiteListErrors
{
    public static Error NotAllowed => new("SubscriptionWhiteList.NotAllowed");
}
