using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests;

public class SubscriptionRequestListModel
{
    public Guid SubscriptionId { get; init; }

    public required string Description { get; set; }

    public required string ReportPath { get; init; }

    public ApprovalStatus Status { get; init; }

    public required string RequesterName { get; init; }

    public required Guid RequesterId { get; init; }
}
