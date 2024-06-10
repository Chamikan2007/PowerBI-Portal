namespace Altria.PowerBIPortal.Domain.Infrastructure;

public class Error
{
    public Error(string errorCode)
    {
        ErrorCode = errorCode;
    }

    public string ErrorCode { get; }
}
