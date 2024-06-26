using Altria.PowerBIPortal.Application.Endpoints.Subscriptions;
using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Domain.Contracts.IPowerBIService;

namespace Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests.GetReports;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapGet("reports", async (IPowerBIReportService powerBIReportService) =>
        {
            var reports = await powerBIReportService.GetReportsAsync();

            var result = reports?.Select(r => new ReportModel
            {
                Name = r.Name,
                Path = r.Path,
                Owner = r.CreatedBy
            });

            return Result<IEnumerable<ReportModel>?>.Success(result);
        });
    }
}
