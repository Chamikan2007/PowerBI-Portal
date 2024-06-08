namespace Altria.PowerBIPortal.Application.Endpoints.Users;

public class UserModel
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }
}
