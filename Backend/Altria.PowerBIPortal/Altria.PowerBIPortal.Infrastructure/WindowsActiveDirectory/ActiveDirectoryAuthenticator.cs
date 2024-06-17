using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities.External;
using Altria.PowerBIPortal.Domain.Contracts;
using Microsoft.Extensions.Options;
using System.DirectoryServices.Protocols;
using System.Net;

namespace Altria.PowerBIPortal.Infrastructure.WindowsActiveDirectory;

public class ActiveDirectoryAuthenticator : IExternalUserAuthenticator
{
    private readonly LdapOptions _options;

    public ActiveDirectoryAuthenticator(IOptions<LdapOptions> options)
    {
        _options = options.Value;
    }

    public SignInResult AuthenticateUser(string username, string password)
    {
        try
        {
            var identifier = new LdapDirectoryIdentifier(_options.Server, _options.Port);
            using var connection = new LdapConnection(identifier);

            var credential = new NetworkCredential(username, password);

            connection.Credential = credential;
            connection.Bind();

            var searchFilter = $"(sAMAccountName={username})";

            var searchRequest = new SearchRequest(
                _options.SearchBase,
                searchFilter,
                SearchScope.Subtree,
                null
            );

            var searchResponse = (SearchResponse)connection.SendRequest(searchRequest);

            var entry = searchResponse.Entries[0];

            var user = new ExternalUser
            {
                DisplayName = (string)entry.Attributes["name"][0],
                Email = (string)entry.Attributes["userprincipalname"][0],
                ExternalId = username,
            };

            return new SignInResult { IsAuthenticated = true, User = user, };
        }
        catch (Exception e)
        {
            return new SignInResult { IsAuthenticated = false, };
        }
    }
}
