using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities.External;

namespace Altria.PowerBIPortal.Domain.Contracts.Repositories;

public interface IExternalUserAuthenticator
{
    public SignInResult AuthenticateUser(string username, string password);
}
