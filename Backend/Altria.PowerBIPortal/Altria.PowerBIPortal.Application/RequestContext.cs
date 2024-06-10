namespace Altria.PowerBIPortal.Application;

public class RequestContext
{
    public Guid UserId { get; set; }

    public required string DisplayName { get; set; }
}
