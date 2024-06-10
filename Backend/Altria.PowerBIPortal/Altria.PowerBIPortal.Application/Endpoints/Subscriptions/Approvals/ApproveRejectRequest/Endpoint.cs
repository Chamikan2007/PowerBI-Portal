using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.Contracts;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;
using Microsoft.AspNetCore.Identity;

namespace Altria.PowerBIPortal.Application.Endpoints.Subscriptions.Approvals.ApproveRejectRequest;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapPost("/{requestId}/steps/{stepId}/action/{action}",
            async (Guid requestId, Guid stepId, Request request, ApprovalStatus action,
            ISubscriptionRequestRepository subscriptionRequestRepository, UserManager<User> UserManager, IUnitOfWork unitOfWork) =>
            {
                if (action == ApprovalStatus.Approved || action == ApprovalStatus.Rejected)
                {
                    return Result.Faliour(ApprovalRequestErrors.InvalidAction);
                }

                if (action == ApprovalStatus.Rejected && string.IsNullOrWhiteSpace(request.Comment))
                {
                    return Result.Faliour(ApprovalRequestErrors.CommentCannotBeEmpty);
                }

                var approvalRequest = await subscriptionRequestRepository.GetByIdAsync(requestId);
                if (approvalRequest == null || approvalRequest.ApprovalRequestSteps == null || approvalRequest.ApprovalRequestSteps.Count == 0)
                {
                    return Result.Faliour(ApprovalRequestErrors.InvalidRequest);
                }

                var currentApprovalStep = approvalRequest.ApprovalRequestSteps.MaxBy(s => s.StepIndex)!;
                if (approvalRequest.Status != ApprovalStatus.Pending || currentApprovalStep.Status != ApprovalStatus.Pending)
                {
                    return Result.Faliour(ApprovalRequestErrors.InvalidStatus);
                }

                if (action == ApprovalStatus.Approved)
                {
                    approvalRequest.Approved();
                }
                else
                {
                    approvalRequest.Rejected(request.Comment!);
                }

                await unitOfWork.SaveChangesAsync();

                return Result.Success();
            });
    }
}
