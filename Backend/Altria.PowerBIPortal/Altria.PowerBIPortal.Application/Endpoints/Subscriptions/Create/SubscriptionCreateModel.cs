namespace Altria.PowerBIPortal.Application.Endpoints.Subscriptions.Create;

public class SubscriptionCreateModel
{
    public required string ReportPath { get; set; }

    public required string Email { get; set; }
}
