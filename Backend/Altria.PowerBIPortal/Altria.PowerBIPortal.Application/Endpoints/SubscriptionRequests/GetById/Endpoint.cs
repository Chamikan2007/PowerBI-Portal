using Altria.PowerBIPortal.Application.Endpoints.Subscriptions;
using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;

namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests.GetById;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapGet("/{subscriptionId}",
            async (Guid subscriptionId, ISubscriptionRequestRepository subscriptionRepository) =>
            {
                var subscription = await subscriptionRepository.GetByIdAsync(subscriptionId);

                if (subscription == null)
                {
                    return Result.Faliour(SubscriptionRequestErrors.NotFound);
                }

                var result = new SubscriptionRequestModel
                {
                    SubscriptionId = subscription.Id,
                    Email = "",
                    Report = ReportModel.FromPath(subscription.ReportPath),
                    Status = subscription.Status,
                    RequesterId = subscription.Requester.Id,
                    RequesterName = subscription.Requester.Name,
                    ApprovalLevels = subscription.ApprovalRequestLevels.Select(l => new SubscriptionRequestApprovalLevelModel
                    {
                        ApprovalLevelId = l.Id,
                        ApprovalLevel = l.ApprovalLevel,
                        Status = l.Status,
                        ApprovalOfficerId = l.ApprovalOfficer?.Id,
                        ApprovalOfficerName = l.ApprovalOfficer?.Name,
                        Comment = l.Comment,
                    }).ToList(),
                };

                return Result<SubscriptionRequestModel>.Success(result);
            });
    }
}
