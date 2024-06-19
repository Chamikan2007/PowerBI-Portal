namespace Altria.PowerBIPortal.Application.Endpoints.Subscriptions.GetReports;

public class ReportModel
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Path { get; set; }
}
