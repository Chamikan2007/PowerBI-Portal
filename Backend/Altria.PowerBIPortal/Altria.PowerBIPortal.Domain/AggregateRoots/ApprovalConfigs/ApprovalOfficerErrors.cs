using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.ApprovalConfigs;

public static class ApprovalOfficerErrors
{
    public static Error InvalidOfficer => new("ApprovalOfficer.InvalidOfficer");
}
