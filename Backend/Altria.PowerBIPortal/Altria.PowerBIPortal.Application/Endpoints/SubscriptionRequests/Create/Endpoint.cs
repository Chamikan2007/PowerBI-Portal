﻿using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriberWhiteListEntries;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;
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
            async (SubscriptionRequestModel model, RequestContext requestContext,
            UserManager<User> userManager, ISubscriptionRequestRepository subscriptionRequestRepository,
            ISubscriberWhiteListEntryRepository subscriptionWhiteListEntryRepository, IUnitOfWork unitOfWork) =>
            {
                var requester = await userManager.FindByIdAsync(requestContext.UserId.ToString());
                if (requester == null)
                {
                    return Result.Faliour(IdentityErrors.UserNotFound);
                }

                #region Validate Email Delivery Option

                var emailDeliveryOption = model.DeliveryOption.EmailDeliveryOption;
                if (emailDeliveryOption != null)
                {
                    var tos = emailDeliveryOption.To.Split(";").Where(e => !string.IsNullOrWhiteSpace(e));
                    var ccs = (emailDeliveryOption.Cc?.Split(";") ?? []).Where(e => !string.IsNullOrWhiteSpace(e));
                    var bccs = (emailDeliveryOption.Bcc?.Split(";") ?? []).Where(e => !string.IsNullOrWhiteSpace(e));
                    var replyTo = (emailDeliveryOption.ReplyTo?.Split(";") ?? []).Where(e => !string.IsNullOrWhiteSpace(e));

                    var allEmails = tos.Concat(ccs).Concat(bccs).Concat(replyTo);

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

                    emailDeliveryOption.To = string.Join(";", tos);
                    emailDeliveryOption.Cc = string.Join(";", ccs);
                    emailDeliveryOption.Bcc = string.Join(";", bccs);
                    emailDeliveryOption.ReplyTo = string.Join(";", replyTo);
                }

                #endregion

                if (model.ScheduleType == Domain.AggregateRoots.SubscriptionRequests.Schedules.Enums.ScheduleType.None)
                {
                    return Result.Faliour(SubscriptionRequestErrors.InvalidScheduleType);
                }

                var subscription = SubscriptionRequest.Create(model.Report.Path, model.Report.Owner, model.Description, model.SubscriptionType, model.SubscriptionInfo, model.ScheduleType, model.Schedule, model.DeliveryOption, requester);
                subscriptionRequestRepository.Create(subscription);

                await unitOfWork.SaveChangesAsync();
                return Result.Success();
            });
    }
}
