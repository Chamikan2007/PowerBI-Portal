namespace Altria.PowerBIPortal.Application.Endpoints.Account.IsAuthenticated;

public class RequestContextModel
{
    public Guid UserId { get; set; }

    public required string DisplayName { get; set; }

    public required string Email { get; set; }
}
