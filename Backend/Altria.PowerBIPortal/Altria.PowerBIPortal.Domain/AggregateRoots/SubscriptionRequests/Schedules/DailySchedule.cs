using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules.Abstractions;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules.Enums;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;

public class DailySchedule
{
    public TimeOnly StartTime { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? StoptDate { get; set; }

    public List<Day>? Days { get; set; }

    public bool? EveryWeekDay { get; set; }

    public int? RepeatIn { get; set; }
}
