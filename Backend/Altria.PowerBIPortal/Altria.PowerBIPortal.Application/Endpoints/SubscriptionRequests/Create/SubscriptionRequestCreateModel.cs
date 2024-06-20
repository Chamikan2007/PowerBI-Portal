namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests.Create;

public class SubscriptionRequestCreateModel
{
    public required string ReportPath { get; set; }

    public required string Email { get; set; }
}
