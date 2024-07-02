using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules.Enums;
using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;

public class DailySchedule : ISchedule, IJsonEntity
{
    public DateTime StartDateTime { get; set; }

    public DateTime? StoptDate { get; set; }

    public List<KeyValuePair<Day, bool>>? Days { get; set; }

    public bool? EveryWeekDay { get; set; }

    public int? RepeatIn { get; set; }
}
