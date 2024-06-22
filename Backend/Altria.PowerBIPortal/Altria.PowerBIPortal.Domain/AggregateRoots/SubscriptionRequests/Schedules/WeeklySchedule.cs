using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules.Abstractions;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules.Enums;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;

public class WeeklySchedule
{
    public TimeOnly StartTime { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? StoptDate { get; set; }
    public List<Day>? Days { get; set; }

    public int? RepeatIn { get; set; }
}
