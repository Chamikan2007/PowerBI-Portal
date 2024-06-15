using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;

public static class SubscriptionErrors
{
    public static Error InvalidEmail => new("Subscription.InvalidEmail");
}
