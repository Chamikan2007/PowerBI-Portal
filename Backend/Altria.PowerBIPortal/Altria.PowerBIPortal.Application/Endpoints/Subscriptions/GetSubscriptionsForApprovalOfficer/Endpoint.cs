using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain;
using Altria.PowerBIPortal.Domain.Contracts;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;
using Microsoft.AspNetCore.Mvc;

namespace Altria.PowerBIPortal.Application.Endpoints.Subscriptions.GetSubscriptionsForApprovalOfficer;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapGet("/forApprovalOfficer",
            async ([FromQuery(Name = "all")] bool includeAll, RequestContext requestContext,
             IApprovalOfficerRepository approvalOfficerRepository, ISubscriptionRepository subscriptionRepository) =>
        {
            var approvalLevels = await approvalOfficerRepository.GetApplicableApprovalLevelsAsync(requestContext.UserId, ApprovalRequestType.SubscriptionApproval);

            var subscriptions = await subscriptionRepository.GeSubscritionRequestsToApproveAsync(requestContext.UserId, approvalLevels, includeAll);

            var results = subscriptions.Select(s => new SubscriptionModel
            {
                SubscriptionId = s.Id,
                Email = s.Email,
                Report = s.Report,
                Status = s.Status,
                RequesterId = s.Requester.Id,
                RequesterName = s.Requester.Name,
            });

            return Result<IEnumerable<SubscriptionModel>>.Success(results);
        });
    }
}
