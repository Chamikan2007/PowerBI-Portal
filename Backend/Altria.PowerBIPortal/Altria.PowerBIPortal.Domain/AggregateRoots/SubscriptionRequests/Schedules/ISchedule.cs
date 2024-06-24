namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;

public interface ISchedule
{
    public DateTime StartDateTime { get; set; }

    public DateTime? StoptDate { get; set; }
}
