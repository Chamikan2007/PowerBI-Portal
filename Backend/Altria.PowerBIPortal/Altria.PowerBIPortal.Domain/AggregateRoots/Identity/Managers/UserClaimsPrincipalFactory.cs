using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Managers;

public class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
{
    private readonly UserManager<User> _userManager;

    public UserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
    {
        _userManager = userManager;
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        identity.RemoveClaim(identity.FindFirst(ClaimTypes.Name));
        identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));

        var roles = await _userManager.GetRolesAsync(user);
        identity.AddClaim(new Claim(ClaimTypes.Role, roles != null ? string.Join(",", roles) : string.Empty));

        return identity;
    }
}