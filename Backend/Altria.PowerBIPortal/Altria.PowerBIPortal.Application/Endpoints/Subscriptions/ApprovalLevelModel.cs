using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Application.Endpoints.Subscriptions;

public class ApprovalLevelModel
{
    public Guid ApprovalLevelId { get; init; }

    public ApprovalStatus Status { get; init; }

    public Guid? ApprovalOfficerId { get; init; }

    public string? ApprovalOfficerName { get; init; }

    public int ApprovalLevel { get; init; }

    public string? Comment { get; init; }
}
