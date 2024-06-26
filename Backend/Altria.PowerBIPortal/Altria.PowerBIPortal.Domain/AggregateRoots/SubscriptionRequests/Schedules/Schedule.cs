using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;

public class Schedule : IJsonEntity
{
    public HourlySchedule? HourlySchedule { get; set; }

    public DailySchedule? DailySchedule { get; set; }

    public WeeklySchedule? WeeklySchedule { get; set; }

    public MonthlySchedule? MonthlySchedule { get; set; }

    public OneTimeSchedule? OneTimeSchedule { get; set; }
}
