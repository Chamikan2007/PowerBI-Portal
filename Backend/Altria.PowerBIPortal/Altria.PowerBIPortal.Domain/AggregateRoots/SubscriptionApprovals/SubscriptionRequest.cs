using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionApprovals;

public class SubscriptionRequest : ApprovalRequest
{
    private SubscriptionRequest() : base(ApprovalRequestType.SubscriptionApproval)
    {
    }

    public required string Report { get; set; }

    public required string Email { get; set; }

    public new void Approved()
    {
        var currentApprovalStep = base.Approved();

        switch (currentApprovalStep.StepIndex)
        {
            case 1:
                ApprovalRequestSteps.Add(ApprovalRequestStep.Create(currentApprovalStep.StepIndex + 1));
                break;

            case 2:
                Status = ApprovalStatus.Approved;
                break;
        }
    }
}
