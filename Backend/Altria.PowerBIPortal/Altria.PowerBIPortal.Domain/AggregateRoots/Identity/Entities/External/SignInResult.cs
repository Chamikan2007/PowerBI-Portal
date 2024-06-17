namespace Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities.External;

public class SignInResult
{
    public bool IsAuthenticated { get; set; }

    public ExternalUser? User { get; set; }
}
