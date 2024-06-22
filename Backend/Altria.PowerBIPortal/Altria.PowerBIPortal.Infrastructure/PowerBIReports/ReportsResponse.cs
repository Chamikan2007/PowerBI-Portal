namespace Altria.PowerBIPortal.Infrastructure.PowerBIReports;

public class ReportsResponse<T> where T : new()
{
    public string OdataContext { get; set; } = string.Empty;

    public T Value { get; set; } = new T();
}
