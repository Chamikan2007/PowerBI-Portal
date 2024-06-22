using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;
using Altria.PowerBIPortal.Domain.Contracts;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Altria.PowerBIPortal.Application.Endpoints.Users.GetUsers;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RequestContext requestContext, UserManager<User> userManager, ISubscriptionRequestRepository subscriptionRequestRepository, IUnitOfWork unitOfWork) =>
        {
            var requester = await userManager.FindByIdAsync(requestContext.UserId.ToString());

            var subinfo = new SubscrptionInfo
            {
                StandardSubscription = new StandardSubscription
                {
                    Description = "Test Sub",
                    DeliveryOption = new DeliveryOption
                    {
                        EmailDeliveryOption = new EmailDeliveryOption
                        {
                            Subject = "Test Sub",
                            To = "chamika@gmail.com",
                        }
                    }
                }
            };

            var sch = new Schedule
            {
                WeeklySchedule = new WeeklySchedule
                {
                    StartDate = new DateOnly(2024, 3, 30),
                    RepeatIn = 2,
                }
            };

            var subscription = SubscriptionRequest.Create("/Countries", subinfo, sch, requester!);
            subscriptionRequestRepository.Create(subscription);

            //var x = await subscriptionRequestRepository.GetByIdAsync(Guid.Parse("587895BD-28C7-40F3-32EC-08DC92D897DB"));

            await unitOfWork.SaveChangesAsync();
        });
    }
}
