using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;

namespace Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

public class ApprovalRequestStep : Entity
{
    protected ApprovalRequestStep()
    {
        Status = ApprovalStatus.Pending;
    }

    public ApprovalStatus Status { get; private set; }

    public User? ApprovalOfficer { get; init; }

    public int StepIndex { get; init; }

    public string? Comment { get; set; }

    public static ApprovalRequestStep Create(int stepIndex)
    {
        return new ApprovalRequestStep { StepIndex = stepIndex };
    }

    public void Approved()
    {
        Status = ApprovalStatus.Approved;
    }

    public void Rejected(string comment)
    {
        Comment = comment;
        Status = ApprovalStatus.Rejected;
    }
}
