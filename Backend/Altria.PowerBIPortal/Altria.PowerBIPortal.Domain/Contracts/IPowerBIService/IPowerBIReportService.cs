using Altria.PowerBIPortal.Domain.Contracts.IPowerBIService.Entities;

namespace Altria.PowerBIPortal.Domain.Contracts.IPowerBIService;

public interface IPowerBIReportService
{
    public Task<List<Report>?> GetReportsAsync();

    Task<Report?> GetReportsByIdAsync(string reportId);
}
