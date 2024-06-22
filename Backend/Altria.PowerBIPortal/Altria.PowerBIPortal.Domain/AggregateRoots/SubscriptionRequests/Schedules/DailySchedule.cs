using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules.Enums;
using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;

public class DailySchedule : IJsonEntity
{
    public required string StartTime { get; set; }

    public required string StartDate { get; set; }

    public string? StoptDate { get; set; }

    public List<Day>? Days { get; set; }

    public bool? EveryWeekDay { get; set; }

    public int? RepeatIn { get; set; }
}
