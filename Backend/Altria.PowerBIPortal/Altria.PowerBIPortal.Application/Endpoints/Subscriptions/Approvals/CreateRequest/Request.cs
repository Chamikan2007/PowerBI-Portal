namespace Altria.PowerBIPortal.Application.Endpoints.Subscriptions.Approvals.CreateSubscriptionApprovalRequest;

public class Request
{
    public required string Report { get; set; }

    public required string Email { get; set; }
}
