namespace Altria.PowerBIPortal.Application.Endpoints.Account.IsAuthenticated;

public class IsAuthenticatedResponse
{
    public bool IsAuthenticated { get; set; }

    public RequestContextModel? RequestContext{ get; set; }
}
