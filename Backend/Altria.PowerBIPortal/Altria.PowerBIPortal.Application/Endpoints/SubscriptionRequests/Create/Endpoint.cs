using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriberWhiteListEntries;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;
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

                #region Validate Email Delivery Option

                EmailDeliveryOption? emailDeliveryOption = null;

                if (model.SubscrptionInfo.StandardSubscription != null)
                {
                    if (model.SubscrptionInfo.StandardSubscription.DeliveryOption.EmailDeliveryOption != null)
                    {
                        emailDeliveryOption = model.SubscrptionInfo.StandardSubscription.DeliveryOption.EmailDeliveryOption;
                    }
                }

                if (emailDeliveryOption != null)
                {
                    var tos = emailDeliveryOption.To.Split(";");
                    var ccs = emailDeliveryOption.Cc?.Split(";") ?? [];
                    var bccs = emailDeliveryOption.Bcc?.Split(";") ?? [];

                    var allEmails = tos.Concat(ccs).Concat(bccs);

                    var validatedEmails = allEmails.Select(e => EmailValidator.IsValidEmail(e));
                    if (validatedEmails.Any(x => !x.isValid))
                    {
                        return Result.Faliour(SubscriptionRequestErrors.InvalidEmail);
                    }

                    var emails = validatedEmails.Select(e => e.email).ToArray();
                    var domains = validatedEmails.Select(e => e.domain).ToArray();

                    var isAllowed = await subscriptionWhiteListEntryRepository.IsAllowedEntryAsync(emails, domains);
                    if (!isAllowed)
                    {
                        return Result.Faliour(SubscriberWhiteListErrors.NotAllowed);
                    }
                }

                #endregion

                var subscription = SubscriptionRequest.Create(model.ReportPath, model.SubscrptionInfo, model.Schedule, requester);
                subscriptionRequestRepository.Create(subscription);

                await unitOfWork.SaveChangesAsync();
                return Result.Success();
            });
    }
}
