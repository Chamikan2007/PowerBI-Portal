namespace Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities.External;

public class ExternalUser
{
    public required string ExternalId { get; set; }

    public required string Email { get; set; }

    public required string DisplayName { get; set; }
}
