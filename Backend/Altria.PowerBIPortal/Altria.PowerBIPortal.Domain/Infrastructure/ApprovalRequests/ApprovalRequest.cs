using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;

namespace Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

public abstract class ApprovalRequest : AggregateRoot
{
    protected ApprovalRequest(ApprovalRequestType approvalRequest)
    {
        ApprovalRequestSteps = new List<ApprovalRequestStep>();
        Type = approvalRequest;
    }

    public IList<ApprovalRequestStep> ApprovalRequestSteps { get; init; }

    public ApprovalStatus Status { get; protected set; }

    public required User Requester { get; init; }

    public ApprovalRequestType Type { get; init; }

    protected ApprovalRequestStep Approved()
    {
        var currentApprovalStep = ApprovalRequestSteps.MaxBy(s => s.StepIndex)!;
        currentApprovalStep.Approved();
        return currentApprovalStep;
    }

    public void Rejected(string comment)
    {
        var currentApprovalStep = ApprovalRequestSteps.MaxBy(s => s.StepIndex)!;
        currentApprovalStep.Rejected(comment);
        Status = ApprovalStatus.Rejected;
    }
}
