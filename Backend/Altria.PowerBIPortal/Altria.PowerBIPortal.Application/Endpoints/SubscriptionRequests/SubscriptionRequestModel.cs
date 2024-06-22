using Altria.PowerBIPortal.Application.Endpoints.Subscriptions;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests;

public class SubscriptionRequestModel
{
    public Guid SubscriptionId { get; init; }

    public required ReportModel Report { get; init; }

    public required string Email { get; init; }

    public ApprovalStatus Status { get; init; }

    public required string RequesterName { get; init; }

    public required Guid RequesterId { get; init; }

    public List<SubscriptionRequestApprovalLevelModel> ApprovalLevels { get; set; } = new();
}
