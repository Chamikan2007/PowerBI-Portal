using Microsoft.AspNetCore.Identity;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;

public class User : IdentityUser<Guid>
{
    private User()
    {        
    }

    public required string Name { get; set; }

    public static User Create(string name, string email)
    {
        var user = new User { Name = name, Email = email, UserName = email };
        return user;
    }
}
