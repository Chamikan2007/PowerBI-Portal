using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionWhiteList;
using Altria.PowerBIPortal.Domain.Contracts;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;
using Altria.PowerBIPortal.Domain.Validators;
using Microsoft.AspNetCore.Identity;

namespace Altria.PowerBIPortal.Application.Endpoints.Subscriptions.Create;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        _ = app.MapPost("/",
            async (SubscriptionCreateModel model, RequestContext requestContext,
            UserManager<User> userManager, ISubscriptionRepository subscriptionRequestRepository,
            ISubscriptionWhiteListEntryRepository subscriptionWhiteListEntryRepository, IUnitOfWork unitOfWork) =>
            {
                var requester = await userManager.FindByIdAsync(requestContext.UserId.ToString());
                if (requester == null)
                {
                    return Result.Faliour(IdentityErrors.UserNotFound);
                }

                var (isValid, domain) = EmailValidator.IsValidEmail(model.Email);
                if (!isValid)
                {
                    return Result.Faliour(SubscriptionErrors.InvalidEmail);
                }

                var isWhiteListEntry = await subscriptionWhiteListEntryRepository.IsAllowedEntryAsync(model.Email, domain);
                if (!isWhiteListEntry)
                {
                    return Result.Faliour(SubscriptionWhiteListErrors.NotAllowed);
                }

                var subscription = Subscription.Create(model.ReportPath, model.Email, requester);
                subscriptionRequestRepository.Create(subscription);

                await unitOfWork.SaveChangesAsync();
                return Result.Success();
            });
    }
}
