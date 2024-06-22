using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules.Abstractions;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;

public class HourlySchedule
{

    public TimeOnly StartTime { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? StoptDate { get; set; }

    public int Hours { get; set; }

    public int Minutes { get; set; }
}
