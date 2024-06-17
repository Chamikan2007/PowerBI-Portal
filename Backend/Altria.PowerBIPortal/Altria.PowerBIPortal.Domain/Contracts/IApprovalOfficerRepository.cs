using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Domain.Contracts;

public interface IApprovalOfficerRepository
{
    Task<bool> IsValidApprovalOfficerAsync(Guid approvalOfficerid, ApprovalRequestType approvalRequestType, int approvalLevel);

    Task<int[]> GetApplicableApprovalLevelsAsync(Guid approvalOfficerid, ApprovalRequestType approvalRequestType);
}
