namespace Altria.PowerBIPortal.Domain.Helpers;

public static class EnumHelper<T> where T : struct, Enum
{
    public static bool TryParse(string value, out T result)
    {
        if (Enum.TryParse(value, true, out result) && Enum.IsDefined(typeof(T), result))
        {
            return true;
        }

        result = default;
        return false;
    }
}