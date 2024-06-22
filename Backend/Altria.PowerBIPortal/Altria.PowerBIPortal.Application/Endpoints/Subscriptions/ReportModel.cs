namespace Altria.PowerBIPortal.Application.Endpoints.Subscriptions;

public class ReportModel
{
    public required string Name { get; set; }

    public required string Path { get; set; }

    public static ReportModel FromPath(string path)
    {
        return new ReportModel
        {
            Name = GetLastPathElement(path),
            Path = path,
        };
    }

    private static string GetLastPathElement(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("The path cannot be null or empty", nameof(path));
        }

        var elements = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        if (elements.Length == 0)
        {
            throw new ArgumentException("The path does not contain any elements", nameof(path));
        }

        return elements[elements.Length - 1];
    }
}
