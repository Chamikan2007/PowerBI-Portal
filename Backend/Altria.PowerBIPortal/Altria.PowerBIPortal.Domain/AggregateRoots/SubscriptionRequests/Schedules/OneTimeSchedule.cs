using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;

public class OneTimeSchedule : IJsonEntity
{
    public required string StartTime { get; set; }

    public required string StartDate { get; set; }

    public string? StoptDate { get; set; }
}
