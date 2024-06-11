namespace Altria.PowerBIPortal.Domain.Infrastructure;

public abstract class Entity
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }
}
