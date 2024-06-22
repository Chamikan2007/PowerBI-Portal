using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;

public class HourlySchedule : IJsonEntity
{

    public required string StartTime { get; set; }

    public required string StartDate { get; set; }

    public string? StoptDate { get; set; }

    public int Hours { get; set; }

    public int Minutes { get; set; }
}
