using Altria.PowerBIPortal.Application.Endpoints.Subscriptions;
using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests.GetMySubscriptionRequests;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapGet("/mySubscriptions",
            async (RequestContext requestContext, ISubscriptionRequestRepository subscriptionRepository, [FromQuery(Name = "all")] bool includeAll = false) =>
            {
                var subscriptions = await subscriptionRepository.GeMySubscritionRequestsAsync(requestContext.UserId, includeAll);

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
