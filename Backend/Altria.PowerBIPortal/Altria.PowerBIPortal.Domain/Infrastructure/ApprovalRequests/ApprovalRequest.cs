﻿using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;

namespace Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

public abstract class ApprovalRequest<TApprovalRequestLevel> : AggregateRoot where TApprovalRequestLevel : ApprovalRequestLevel
{
    protected ApprovalRequest()
    {
        ApprovalRequestLevels = new List<TApprovalRequestLevel>();
        Status = ApprovalStatus.Pending;
    }

    public IList<TApprovalRequestLevel> ApprovalRequestLevels { get; init; }

    public ApprovalStatus Status { get; protected set; }

    public required User Requester { get; init; }

    protected void Approved(User approvalOfficer, TApprovalRequestLevel currentApprovalLevel)
    {
        currentApprovalLevel.Approved(approvalOfficer);
    }

    public void Rejected(User approvalOfficer, TApprovalRequestLevel currentApprovalLevel, string comment)
    {
        currentApprovalLevel.Rejected(approvalOfficer, comment);
        Status = ApprovalStatus.Rejected;
    }

    public void Cancelled()
    {
        Status = ApprovalStatus.Cancelled;
    }
}
