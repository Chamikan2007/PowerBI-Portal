using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;

public class OneTimeSchedule : ISchedule, IJsonEntity
{
    public DateTime StartDateTime { get; set; }

    public DateTime? StoptDate { get; set; }
}
