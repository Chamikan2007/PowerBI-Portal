using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain;
using Altria.PowerBIPortal.Domain.AggregateRoots.ApprovalConfigs;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.Contracts;
using Altria.PowerBIPortal.Domain.Helpers;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;
using Microsoft.AspNetCore.Identity;

namespace Altria.PowerBIPortal.Application.Endpoints.Subscriptions.ApproveReject;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        _ = app.MapPost("/{subscriptionId}/level/{levelId}/action/{action}",
            async (Guid subscriptionId, Guid levelId, SubscriptionApproveRejectModel request, string action,
            ISubscriptionRepository subscriptionRepository, UserManager<User> userManager,
            IApprovalOfficerRepository approvalOfficerRepository, RequestContext requestContext, IUnitOfWork unitOfWork) =>
            {
                if(!EnumHelper<ApprovalStatus>.TryParse(action , out var approvalAction))
                {
                    return Result.Faliour(ApprovalRequestErrors.InvalidAction);
                }

                var approvalOfficer = await userManager.FindByIdAsync(requestContext.UserId.ToString());
                if (approvalOfficer == null)
                {
                    return Result.Faliour(IdentityErrors.UserNotFound);
                }

                if (approvalAction == ApprovalStatus.Approved || approvalAction != ApprovalStatus.Rejected)
                {
                    return Result.Faliour(ApprovalRequestErrors.InvalidAction);
                }

                if (approvalAction == ApprovalStatus.Rejected && string.IsNullOrWhiteSpace(request.Comment))
                {
                    return Result.Faliour(ApprovalRequestErrors.CommentCannotBeEmpty);
                }

                var subscription = await subscriptionRepository.GetByIdAsync(subscriptionId);
                if (subscription == null || subscription.ApprovalRequestSteps == null || subscription.ApprovalRequestSteps.Count == 0)
                {
                    return Result.Faliour(ApprovalRequestErrors.InvalidRequest);
                }

                var currentApprovalLevel = subscription.ApprovalRequestSteps.MaxBy(s => s.ApprovalLevel)!;
                if (currentApprovalLevel.Id != levelId)
                {
                    return Result.Faliour(ApprovalRequestErrors.InvalidLevel);
                }

                if (subscription.Status != ApprovalStatus.Pending || currentApprovalLevel.Status != ApprovalStatus.Pending)
                {
                    return Result.Faliour(ApprovalRequestErrors.InvalidStatus);
                }

                var isValidApprovalOfficer = await approvalOfficerRepository.IsValidApprovalOfficerAsync(approvalOfficer.Id, ApprovalRequestType.SubscriptionApproval, currentApprovalLevel.ApprovalLevel);
                if (!isValidApprovalOfficer)
                {
                    return Result.Faliour(ApprovalOfficerErrors.InvalidOfficer);
                }

                if (approvalAction == ApprovalStatus.Approved)
                {
                    subscription.Approved(approvalOfficer, currentApprovalLevel);
                }
                else
                {
                    subscription.Rejected(approvalOfficer, currentApprovalLevel, request.Comment!);
                }

                await unitOfWork.SaveChangesAsync();

                return Result.Success();
            });
    }
}
