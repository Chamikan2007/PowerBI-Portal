using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;
using Altria.PowerBIPortal.Domain.Contracts;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Application.Endpoints.Subscriptions.Cancel
{
    public class Endpoint : IGroupedEndpoint<EndpointGroup>
    {
        public void Configure(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{subscriptionId}",
                async (Guid subscriptionId, ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork) =>
                {
                    var subscription = await subscriptionRepository.FetchByIdAsync(subscriptionId);
                    if (subscription == null)
                    {
                        return Result.Faliour(SubscriptionErrors.NotFound);
                    }

                    if (subscription.Status != ApprovalStatus.Pending)
                    {
                        return Result.Faliour(SubscriptionErrors.InvalidStatus);
                    }

                    subscription.Cancelled();
                    await unitOfWork.SaveChangesAsync();

                    return Result.Success();
                });
        }
    }
}
