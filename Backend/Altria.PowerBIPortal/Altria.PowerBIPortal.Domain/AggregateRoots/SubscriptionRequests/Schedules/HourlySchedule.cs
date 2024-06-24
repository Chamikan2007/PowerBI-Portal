using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;

public class HourlySchedule : ISchedule, IJsonEntity
{
    public DateTime StartDateTime { get; set; }

    public DateTime? StoptDate { get; set; }

    public int Hours { get; set; }

    public int Minutes { get; set; }
}
