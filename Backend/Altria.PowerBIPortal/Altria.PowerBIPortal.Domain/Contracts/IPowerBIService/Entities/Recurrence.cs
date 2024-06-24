namespace Altria.PowerBIPortal.Domain.Contracts.IPowerBIService.Entities;

public class Recurrence
{
    public MinuteRecurrence MinuteRecurrence { get; set; }
    public object DailyRecurrence { get; set; }
    public object WeeklyRecurrence { get; set; }
    public object MonthlyRecurrence { get; set; }
    public object MonthlyDOWRecurrence { get; set; }
}
