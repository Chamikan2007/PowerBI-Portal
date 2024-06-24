namespace Altria.PowerBIPortal.Domain.Contracts.IPowerBIService.Entities;

public class Subscription
{
    public string Id { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public bool IsDataDriven { get; set; }
    public required string Description { get; set; }
    public string Report { get; set; }
    public bool IsActive { get; set; }
    public required string EventType { get; set; }
    public  string ScheduleDescription { get; set; }
    public DateTime LastRunTime { get; set; }
    public string LastStatus { get; set; }
    public string DeliveryExtension { get; set; }
    public string LocalizedDeliveryExtensionName { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Schedule Schedule { get; set; }
    public object DataQuery { get; set; }
    public ExtensionSettings ExtensionSettings { get; set; }
    public List<ParameterValue> ParameterValues { get; set; }
}
