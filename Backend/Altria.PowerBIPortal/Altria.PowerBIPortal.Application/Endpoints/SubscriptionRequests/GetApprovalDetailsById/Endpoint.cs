using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;

namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests.GetApprovalDetailsById;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapGet("/{subscriptionRequestId}/approvalDetails",
            async (Guid subscriptionRequestId, ISubscriptionRequestRepository subscriptionRepository) =>
            {
                var approvalDetails = await subscriptionRepository.GetApprovalDetailsByRequestIdAsync(subscriptionRequestId);

                var results = approvalDetails.Select(s => new SubscriptionRequestApprovalLevelModel
                {
                    ApprovalLevelId = s.Id,
                    ApprovalLevel = s.ApprovalLevel,
                    Status = s.Status,
                    ApprovalOfficerId = s.ApprovalOfficer?.Id,
                    ApprovalOfficerName = s.ApprovalOfficer?.Name,
                    ApprovedAtUtc = s.UpdatedAtUtc,
                    Comment = s.Comment,
                });

                return Result<IEnumerable<SubscriptionRequestApprovalLevelModel>>.Success(results);
            });
    }
}
