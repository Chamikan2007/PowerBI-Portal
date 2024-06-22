using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;
using Altria.PowerBIPortal.Domain.Contracts;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests.Cancel
{
    public class Endpoint : IGroupedEndpoint<EndpointGroup>
    {
        public void Configure(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{subscriptionId}",
                async (Guid subscriptionId, ISubscriptionRequestRepository subscriptionRepository, IUnitOfWork unitOfWork) =>
                {
                    var subscription = await subscriptionRepository.FetchByIdAsync(subscriptionId);
                    if (subscription == null)
                    {
                        return Result.Faliour(SubscriptionRequestErrors.NotFound);
                    }

                    if (subscription.Status != ApprovalStatus.Pending)
                    {
                        return Result.Faliour(SubscriptionRequestErrors.InvalidStatus);
                    }

                    subscription.Cancelled();
                    await unitOfWork.SaveChangesAsync();

                    return Result.Success();
                });
        }
    }
}
