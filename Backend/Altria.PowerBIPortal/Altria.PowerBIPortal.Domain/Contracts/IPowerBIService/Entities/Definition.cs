namespace Altria.PowerBIPortal.Domain.Contracts.IPowerBIService.Entities;

public class Definition
{
    public DateTime StartDateTime { get; set; }
    public DateTime EndDate { get; set; }
    public bool EndDateSpecified { get; set; }
    public Recurrence Recurrence { get; set; }
}
