using Altria.PowerBIPortal.Domain.Infrastructure;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules.Abstractions;

public class Schedule 
{
    public HourlySchedule? HourlySchedule { get; set; }

    public DailySchedule? DailySchedule { get; set; }

    public WeeklySchedule? WeeklySchedule { get; set; }

    public MonthlySchedule? MonthlySchedule { get; set; }

    public OneTimeSchedule? OneTimeSchedule { get; set; }
}
