using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionApprovals;

public class SubscriptionRequest : ApprovalRequest<SubscriptionApprovalLevel>
{
    private SubscriptionRequest() : base(ApprovalRequestType.SubscriptionApproval)
    {
    }

    public required string Report { get; set; }

    public required string Email { get; set; }

    public new void Approved(User approvalOfficer)
    {
        var currentApprovalStep = base.Approved(approvalOfficer);

        switch (currentApprovalStep.ApprovalLevel)
        {
            case 1:
                ApprovalRequestSteps.Add(SubscriptionApprovalLevel.Create(currentApprovalStep.ApprovalLevel + 1));
                break;

            case 2:
                Status = ApprovalStatus.Approved;
                break;
        }
    }
}
