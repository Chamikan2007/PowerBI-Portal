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
                var subscriptionRequest = await subscriptionRepository.GetByIdAsync(subscriptionRequestId);

                if (subscriptionRequest == null)
                {
                    return Result.Faliour(SubscriptionRequestErrors.NotFound);
                }

                var result = new SubscriptionRequestModel
                {
                    Report = ReportModel.Create(subscriptionRequest.ReportPath, subscriptionRequest.Owner),
                    Description = subscriptionRequest.Description,
                    SubscriptionType = subscriptionRequest.SubscriptionType,
                    SubscriptionInfo = subscriptionRequest.SubscriptionInfo,
                    DeliveryOption = subscriptionRequest.DeliveryOption,
                    Schedule = subscriptionRequest.Schedule,
                    ScheduleType = subscriptionRequest.ScheduleType,
                };

                return Result<SubscriptionRequestModel>.Success(result);
            });
    }
}
