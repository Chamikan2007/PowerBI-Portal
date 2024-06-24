namespace Altria.PowerBIPortal.Domain.Contracts.IPowerBIService.Entities;

public class ExtensionSettings
{
    public string Extension { get; set; }
    public List<ParameterValue> ParameterValues { get; set; }
}
