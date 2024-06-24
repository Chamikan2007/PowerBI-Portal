using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules.Enums;
using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;

public class MonthlySchedule : ISchedule, IJsonEntity
{
    public DateTime StartDateTime { get; set; }

    public DateTime? StoptDate { get; set; }

    public required List<Month> Months { get; set; }

    public WeekOfMonth? WeekOfMonth { get; set; }

    public List<Day>? Days { get; set; }

    public string? CalenderDays { get; set; }
}
