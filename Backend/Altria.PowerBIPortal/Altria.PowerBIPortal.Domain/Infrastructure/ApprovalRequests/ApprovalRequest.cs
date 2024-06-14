using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;

namespace Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

public abstract class ApprovalRequest<TApprovalRequestStep> : AggregateRoot where TApprovalRequestStep : ApprovalRequestLevel
{
    protected ApprovalRequest(ApprovalRequestType approvalRequest)
    {
        ApprovalRequestSteps = new List<TApprovalRequestStep>();
        Type = approvalRequest;
    }

    public IList<TApprovalRequestStep> ApprovalRequestSteps { get; init; }

    public ApprovalStatus Status { get; protected set; }

    public required User Requester { get; init; }

    public ApprovalRequestType Type { get; init; }

    protected ApprovalRequestLevel Approved(User approvalOfficer)
    {
        var currentApprovalStep = ApprovalRequestSteps.MaxBy(s => s.ApprovalLevel)!;
        currentApprovalStep.Approved(approvalOfficer);
        return currentApprovalStep;
    }

    public void Rejected(User approvalOfficer, string comment)
    {
        var currentApprovalStep = ApprovalRequestSteps.MaxBy(s => s.ApprovalLevel)!;
        currentApprovalStep.Rejected(approvalOfficer, comment);
        Status = ApprovalStatus.Rejected;
    }
}
