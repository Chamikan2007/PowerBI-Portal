using System.Net.Mail;

namespace Altria.PowerBIPortal.Domain.Validators;

public static class EmailValidator
{
    public static (bool isValid, string domain, string email) IsValidEmail(string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);
            return (true, mailAddress.Host, mailAddress.Address);
        }
        catch
        {
            return (false, string.Empty, string.Empty);
        }
    }
}
