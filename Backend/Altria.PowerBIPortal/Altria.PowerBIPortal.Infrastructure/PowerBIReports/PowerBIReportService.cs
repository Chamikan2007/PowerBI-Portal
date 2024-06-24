using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;
using Altria.PowerBIPortal.Domain.Contracts.IPowerBIService;
using Altria.PowerBIPortal.Domain.Contracts.IPowerBIService.Entities;
using System.Data.Common;

// using PowerBI;
using System.Net.Http.Json;
using System.Reflection.Metadata;

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

    public Task<Guid> CreateSubscriptionAsync(Subscription subscription)
    {
        //var client = new PowerBI.ReportingService2010SoapClient(EndpointConfiguration.ReportingService2010Soap, "http://4.145.104.202/ReportServer/ReportService2010.asmx");

        //var definition = new PowerBI.ScheduleDefinition();
        //definition.StartDateTime = DateTime.Now;
        //definition.EndDate = DateTime.Now;
        //definition.EndDateSpecified = true;
        //definition.Item = new DailyRecurrence
        //{
        //    DaysInterval = 1,

        //};

        return Task.FromResult(Guid.NewGuid());
    }

    public async Task CreateSubscriptionAsync()
    {
        var response = await _powerBIClient.GetStringAsync("Subscriptions");
    }

    public async Task GetSubscriptionDetailsAsync(string subscriptionId)
    {
        var response = await _powerBIClient.GetStringAsync($"Subscriptions({subscriptionId})?$expand=DataSource"); 
    }
}
