using System.Net.Mail;

namespace Altria.PowerBIPortal.Domain.Validators;

public static class EmailValidator
{
    public static (bool isValid, string domain) IsValidEmail(string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);
            return (true, mailAddress.Host);
        }
        catch (FormatException)
        {
            return (false, string.Empty);
        }
    }
}
