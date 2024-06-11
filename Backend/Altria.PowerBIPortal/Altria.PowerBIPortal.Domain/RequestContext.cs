namespace Altria.PowerBIPortal.Domain;

public class RequestContext
{
    public Guid UserId { get; set; }

    public required string DisplayName { get; set; }
}
