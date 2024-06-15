using Altria.PowerBIPortal.Domain.AggregateRoots.ApprovalConfigs;
using Altria.PowerBIPortal.Domain.Contracts;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;
using Microsoft.EntityFrameworkCore;

namespace Altria.PowerBIPortal.Persistence.Repositories.ApprovalConfigs;

public class ApprovalOfficerRepository : Repository<ApprovalOfficer>, IApprovalOfficerRepository
{
    public ApprovalOfficerRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Task<bool> IsValidApprovalOfficerAsync(Guid approvalOfficerid, ApprovalRequestType approvalRequestType, int approvalLevel)
    {
        return _readOnlyStore.AnyAsync(a => a.Officer.Id == approvalOfficerid && a.ApprovalRequestType == approvalRequestType && a.ApprovalLevel == approvalLevel);
    }

    public Task<int[]> GetApplicableApprovalLevelsAsync(Guid approvalOfficerid, ApprovalRequestType approvalRequestType)
    {
        return _readOnlyStore.Where(a => a.Officer.Id == approvalOfficerid && a.ApprovalRequestType == approvalRequestType)
            .Select(a => a.ApprovalLevel).ToArrayAsync();
    }
}
