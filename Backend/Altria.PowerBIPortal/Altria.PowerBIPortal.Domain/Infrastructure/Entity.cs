namespace Altria.PowerBIPortal.Domain.Infrastructure;

public abstract class Entity
{
    public Guid Id { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    public Guid UpdatedBy { get; set; }
}
