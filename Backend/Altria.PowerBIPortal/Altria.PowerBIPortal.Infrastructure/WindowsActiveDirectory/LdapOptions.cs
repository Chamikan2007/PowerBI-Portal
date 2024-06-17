namespace Altria.PowerBIPortal.Infrastructure.WindowsActiveDirectory;

public class LdapOptions
{
    public static string Options => "LDAP";

    public required string Server{ get; set; }

    public int Port { get; set; } = 389;

    public required string SearchBase { get; set; }

}
