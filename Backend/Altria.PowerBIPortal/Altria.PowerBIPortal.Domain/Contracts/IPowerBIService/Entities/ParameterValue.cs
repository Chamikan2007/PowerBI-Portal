namespace Altria.PowerBIPortal.Domain.Contracts.IPowerBIService.Entities;

public class ParameterValue
{
    public string Name { get; set; }
    public string Value { get; set; }
    public bool IsValueFieldReference { get; set; }
}