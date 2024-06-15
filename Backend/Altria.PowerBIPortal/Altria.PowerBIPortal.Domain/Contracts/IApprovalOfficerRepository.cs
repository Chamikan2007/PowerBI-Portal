using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Domain.Contracts;

public interface IApprovalOfficerRepository
{
    Task<bool> IsValidApprovalOfficerAsync(Guid id, ApprovalRequestType approvalRequestType, int approvalLevel);
}
