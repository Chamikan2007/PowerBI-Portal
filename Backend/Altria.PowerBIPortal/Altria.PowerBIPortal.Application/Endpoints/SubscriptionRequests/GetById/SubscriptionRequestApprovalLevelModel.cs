using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests.GetById;

public class SubscriptionRequestApprovalLevelModel
{
    public Guid ApprovalLevelId { get; init; }

    public ApprovalStatus Status { get; init; }

    public Guid? ApprovalOfficerId { get; init; }

    public string? ApprovalOfficerName { get; init; }

    public int ApprovalLevel { get; init; }

    public string? Comment { get; init; }

    public DateTime ApprovedAtUtc { get; set; }
}
