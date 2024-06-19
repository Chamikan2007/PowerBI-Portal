using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;

namespace Altria.PowerBIPortal.Application.Endpoints.Subscriptions.GetById;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapGet("/{subscriptionId}",
            async (Guid subscriptionId, ISubscriptionRepository subscriptionRepository) =>
            {
                var subscription = await subscriptionRepository.GetByIdAsync(subscriptionId);

                if (subscription == null)
                {
                    return Result.Faliour(SubscriptionErrors.NotFound);
                }

                var result = new SubscriptionModel
                {
                    SubscriptionId = subscription.Id,
                    Email = subscription.Email,
                    Report = ReportModel.FromPath(subscription.ReportPath),
                    Status = subscription.Status,
                    RequesterId = subscription.Requester.Id,
                    RequesterName = subscription.Requester.Name,
                    ApprovalLevels = subscription.ApprovalRequestLevels.Select(l => new ApprovalLevelModel
                    {
                        ApprovalLevelId = l.Id,
                        ApprovalLevel = l.ApprovalLevel,
                        Status = l.Status,
                        ApprovalOfficerId = l.ApprovalOfficer?.Id,
                        ApprovalOfficerName = l.ApprovalOfficer?.Name,
                        Comment = l.Comment,
                    }).ToList(),
                };

                return Result<SubscriptionModel>.Success(result);
            });
    }
}
