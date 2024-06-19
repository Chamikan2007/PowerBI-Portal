using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Altria.PowerBIPortal.Application.Endpoints.Subscriptions.GetMySubscriptions;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapGet("/mySubscriptions",
            async (RequestContext requestContext, ISubscriptionRepository subscriptionRepository, [FromQuery(Name = "all")] bool includeAll = false) =>
            {
                var subscriptions = await subscriptionRepository.GeMySubscritionRequestsAsync(requestContext.UserId, includeAll);

                var results = subscriptions.Select(s => new SubscriptionModel
                {
                    SubscriptionId = s.Id,
                    Email = s.Email,
                    Report = ReportModel.FromPath(s.ReportPath),
                    Status = s.Status,
                    RequesterId = s.Requester.Id,
                    RequesterName = s.Requester.Name,
                });

                return Result<IEnumerable<SubscriptionModel>>.Success(results);
            });
    }
}
