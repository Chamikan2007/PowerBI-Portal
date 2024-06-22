namespace Altria.PowerBIPortal.Domain.Contracts.IPowerBIService.Entities;

public class Report
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Path { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public bool Hidden { get; set; }

    public long Size { get; set; }

    public string ModifiedBy { get; set; } = string.Empty;

    public DateTime ModifiedDate { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public Guid ParentFolderId { get; set; }

    public bool IsFavorite { get; set; }

    public string ContentType { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public bool HasDataSources { get; set; }

    public bool HasSharedDataSets { get; set; }

    public bool HasParameters { get; set; }

    public List<object> Roles { get; set; } = new List<object>();
}
