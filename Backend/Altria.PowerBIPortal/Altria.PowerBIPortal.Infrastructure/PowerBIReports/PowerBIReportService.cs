using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;
using Altria.PowerBIPortal.Domain.Contracts.IPowerBIService;
using Altria.PowerBIPortal.Domain.Contracts.IPowerBIService.Entities;
using System.Net.Http.Json;

namespace Altria.PowerBIPortal.Infrastructure.PowerBIReports;

public class PowerBIReportService : IPowerBIReportService
{
    private readonly HttpClient _powerBIClient;

    public PowerBIReportService(HttpClient powerBIClient)
    {
        _powerBIClient = powerBIClient;
    }

    public async Task<List<Report>?> GetReportsAsync()
    {
        var response = await _powerBIClient.GetFromJsonAsync<ReportsResponse<List<Report>>>("reports");
        return response?.Value;
    }

    public async Task<Report?> GetReportsByIdAsync(string reportPath)
    {
        var response = await _powerBIClient.GetFromJsonAsync<Report>($"Reports(path='{reportPath}')");
        return response;
    }

    public Task<Guid> ProcessSubscriptionAsync(SubscriptionRequest subscriptions)
    {
        throw new NotImplementedException();
    }
}
