using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;

public static class SubscriptionRequestErrors
{
    public static Error InvalidEmail => new("SubscriptionRequest.InvalidEmail");

    public static Error InvalidStatus => new("SubscriptionRequest.InvalidStatus");

    public static Error NotFound => new("SubscriptionRequest.NotFound");

    public static Error InvalidScheduleType => new("SubscriptionRequest.InvalidScheduleType");
}
