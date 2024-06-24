using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;
using Altria.PowerBIPortal.Domain.Contracts.IPowerBIService.Entities;

namespace Altria.PowerBIPortal.Domain.Contracts.IPowerBIService;

public interface IPowerBIReportService
{
    public Task<List<Report>?> GetReportsAsync();

    Task<Report?> GetReportsByIdAsync(string reportPath);

    Task<Guid> CreateSubscriptionAsync(Subscription subscription);

    Task GetSubscriptionDetailsAsync(string subscriptionId);
}
