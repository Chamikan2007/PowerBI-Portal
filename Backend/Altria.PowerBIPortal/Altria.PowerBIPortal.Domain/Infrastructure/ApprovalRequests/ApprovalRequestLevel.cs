using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;

namespace Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

public abstract class ApprovalRequestLevel : Entity
{
    protected ApprovalRequestLevel()
    {
        Status = ApprovalStatus.Pending;
    }

    public ApprovalStatus Status { get; private set; }

    public User? ApprovalOfficer { get; private set; }

    public int ApprovalLevel { get; init; }

    public string? Comment { get; private set; }

    public void Approved(User approvalOfficer)
    {
        ApprovalOfficer = approvalOfficer;
        Status = ApprovalStatus.Approved;
    }

    public void Rejected(User approvalOfficer, string comment)
    {
        Comment = comment;
        ApprovalOfficer = approvalOfficer;
        Status = ApprovalStatus.Rejected;
    }
}
