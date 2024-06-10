using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.Infrastructure;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.ApprovalConfigs;

public class ApprovalOfficer : AggregateRoot
{
    public ApprovalRequestType ApprovalRequestType { get; set; }

    public int StepIndex { get; init; }

    public required User Officer { get; init; }
}
