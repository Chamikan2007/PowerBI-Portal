using Altria.PowerBIPortal.Application.Endpoints.Subscriptions;
using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;

namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests.GetById;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapGet("/{subscriptionRequestId}",
            async (Guid subscriptionRequestId, ISubscriptionRequestRepository subscriptionRepository) =>
            {
                var subscription = await subscriptionRepository.GetByIdAsync(subscriptionRequestId);

                if (subscription == null)
                {
                    return Result.Faliour(SubscriptionRequestErrors.NotFound);
                }

                var result = new SubscriptionRequestModel
                {
                    Report = ReportModel.Create(subscription.ReportPath, subscription.Owner),
                    SubscriptionInfo = subscription.SubscriptionInfo,
                    DeliveryOption = subscription.DeliveryOption,
                    Schedule = subscription.Schedule,
                    ScheduleType = subscription.ScheduleType,
                };

                return Result<SubscriptionRequestModel>.Success(result);
            });
    }
}
