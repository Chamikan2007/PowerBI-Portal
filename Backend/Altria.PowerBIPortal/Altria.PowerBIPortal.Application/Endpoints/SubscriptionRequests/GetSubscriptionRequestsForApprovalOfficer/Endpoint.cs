using Altria.PowerBIPortal.Application.Endpoints.Subscriptions;
using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;
using Microsoft.AspNetCore.Mvc;

namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests.GetSubscriptionRequestsForApprovalOfficer;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapGet("/forApprovalOfficer",
            async (RequestContext requestContext, IApprovalOfficerRepository approvalOfficerRepository,
            ISubscriptionRequestRepository subscriptionRepository, [FromQuery(Name = "all")] bool includeAll = false) =>
        {
            var approvalLevels = await approvalOfficerRepository.GetApplicableApprovalLevelsAsync(requestContext.UserId, ApprovalRequestType.SubscriptionApproval);

            var subscriptions = await subscriptionRepository.GeSubscritionRequestsToApproveAsync(requestContext.UserId, approvalLevels, includeAll);

            var results = subscriptions.Select(s => new SubscriptionRequestModel
            {
                SubscriptionId = s.Id,
                Email = "",
                Report = ReportModel.FromPath(s.ReportPath),
                Status = s.Status,
                RequesterId = s.Requester.Id,
                RequesterName = s.Requester.Name,
            });

            return Result<IEnumerable<SubscriptionRequestModel>>.Success(results);
        });
    }
}
