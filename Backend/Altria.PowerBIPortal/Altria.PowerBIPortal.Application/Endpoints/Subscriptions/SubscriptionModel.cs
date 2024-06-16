using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Application.Endpoints.Subscriptions;

public class SubscriptionModel
{
    public Guid SubscriptionId { get; init; }

    public required string Report { get; init; }

    public required string Email { get; init; }

    public ApprovalStatus Status { get; init; }

    public required string RequesterName { get; init; }

    public required Guid RequesterId { get; init; }

    public List<ApprovalLevelModel> ApprovalLevels { get; set; } = new();
}
