using System.ComponentModel;

namespace Cursive.Application.Utils;

public static class EnumUtils
{
    public static TEnum GetEnumByName<TEnum>(string enumName) where TEnum : Enum
    {
        if (!Enum.TryParse(typeof(TEnum), enumName, out object? @enum) || @enum == null)
        {
            throw new InvalidEnumArgumentException("Invalid enum name.");
        }

        return (TEnum)@enum;
    }

    public static TEnum CastEnumByNumber<TEnum>(this int enumNumber) where TEnum : Enum
    {
        if (Enum.IsDefined(typeof(TEnum), enumNumber))
            return (TEnum)(object)enumNumber;

        throw new InvalidEnumArgumentException($"Invalid enum number: {enumNumber}");
    }
}
