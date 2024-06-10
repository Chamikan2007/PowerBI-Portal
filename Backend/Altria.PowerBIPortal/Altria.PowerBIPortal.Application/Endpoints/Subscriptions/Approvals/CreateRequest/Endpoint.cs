using Altria.PowerBIPortal.Application.Endpoints.Subscriptions.Approvals;
using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain.Contracts;

namespace Altria.PowerBIPortal.Application.Endpoints.Subscriptions.Approvals.CreateSubscriptionApprovalRequest;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapPost("/createSubscription",
            async (Request request,
            ISubscriptionRequestRepository approvalRequestRepository, IUnitOfWork unitOfWork) =>
            {
                //approvalRequestRepository.
            });
    }
}
