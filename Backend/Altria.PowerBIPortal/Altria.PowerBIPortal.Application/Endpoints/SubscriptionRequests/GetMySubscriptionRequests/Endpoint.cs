using Altria.PowerBIPortal.Application.Helpers;
using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests.GetMySubscriptionRequests;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapGet("/my",
            async (RequestContext requestContext, ISubscriptionRequestRepository subscriptionRepository, [FromQuery(Name = "all")] bool includeAll = false) =>
            {
                var subscriptions = await subscriptionRepository.GeMySubscritionRequestsAsync(requestContext.UserId, includeAll);

                var results = subscriptions.Select(s => CastToSubscriptionRequestListModel.Cast(s));

                return Result<IEnumerable<SubscriptionRequestListModel>>.Success(results);
            });
    }
}
