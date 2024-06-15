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

    public Task<bool> IsValidApprovalOfficerAsync(Guid id, ApprovalRequestType approvalRequestType, int approvalLevel)
    {
        return _readOnlyStore.AnyAsync(a => a.Officer.Id == id && a.ApprovalRequestType == approvalRequestType && a.ApprovalLevel == approvalLevel);
    }
}
