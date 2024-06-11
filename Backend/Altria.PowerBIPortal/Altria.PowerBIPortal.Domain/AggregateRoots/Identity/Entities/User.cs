using Microsoft.AspNetCore.Identity;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;

public class User : IdentityUser<Guid>
{
    private User()
    {        
    }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public static User Create(string firstName, string lastName, string email)
    {
        var user = new User { FirstName = firstName, LastName = lastName, Email = email, UserName = email };
        return user;
    }
}
