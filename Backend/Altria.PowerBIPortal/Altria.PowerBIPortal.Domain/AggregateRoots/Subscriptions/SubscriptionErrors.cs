using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;

public static class SubscriptionErrors
{
    public static Error InvalidEmail => new("Subscription.InvalidEmail");
    
    public static Error InvalidStatus => new("Subscription.InvalidStatus");

    public static Error NotFound => new("Subscription.NotFound");
}
