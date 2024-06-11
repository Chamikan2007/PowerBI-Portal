using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.Identity;

public static class IdentityErrors
{
    public static Error UserNotFound => new("Identity.UserNotFound");

    public static Error InvalidLoginDetails => new("Identity.InvalidLoginDetails");
}
