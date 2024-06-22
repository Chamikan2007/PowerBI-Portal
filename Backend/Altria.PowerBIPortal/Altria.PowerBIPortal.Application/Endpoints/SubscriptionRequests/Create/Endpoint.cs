using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriberWhiteListEntries;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;
using Altria.PowerBIPortal.Domain.Contracts;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;
using Altria.PowerBIPortal.Domain.Validators;
using Microsoft.AspNetCore.Identity;

namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests.Create;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        _ = app.MapPost("/",
            async (SubscriptionRequestCreateModel model, RequestContext requestContext,
            UserManager<User> userManager, ISubscriptionRequestRepository subscriptionRequestRepository,
            ISubscriberWhiteListEntryRepository subscriptionWhiteListEntryRepository, IUnitOfWork unitOfWork) =>
            {
                var requester = await userManager.FindByIdAsync(requestContext.UserId.ToString());
                if (requester == null)
                {
                    return Result.Faliour(IdentityErrors.UserNotFound);
                }

                var (isValid, domain) = EmailValidator.IsValidEmail(model.Email);
                if (!isValid)
                {
                    return Result.Faliour(SubscriptionRequestErrors.InvalidEmail);
                }

                var isWhiteListEntry = await subscriptionWhiteListEntryRepository.IsAllowedEntryAsync(model.Email, domain);
                if (!isWhiteListEntry)
                {
                    return Result.Faliour(SubscriberWhiteListErrors.NotAllowed);
                }

                var subscription = SubscriptionRequest.Create(model.ReportPath, model.SubscrptionInfo, model.Schedule, requester);
                subscriptionRequestRepository.Create(subscription);

                await unitOfWork.SaveChangesAsync();
                return Result.Success();
            });
    }
}
