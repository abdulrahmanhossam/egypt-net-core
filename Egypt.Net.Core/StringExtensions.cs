namespace Egypt.Net.Core;

/// <summary>
/// Extension methods for strings to work with Egyptian National IDs.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Validates if the string is a valid Egyptian National ID.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateChecksum">
    /// Whether to validate the checksum. Default is FALSE.
    /// Set to TRUE only if you have the verified checksum algorithm.
    /// </param>
    /// <returns>True if the string is a valid Egyptian National ID; otherwise, false.</returns>
    public static bool IsValidEgyptianNationalId(this string value, bool validateChecksum = false)
    {
        return EgyptianNationalId.IsValid(value, validateChecksum);
    }

    /// <summary>
    /// Converts the string to an EgyptianNationalId instance if valid.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <param name="validateChecksum">
    /// Whether to validate the checksum. Default is FALSE.
    /// Set to TRUE only if you have the verified checksum algorithm.
    /// </param>
    /// <returns>An EgyptianNationalId instance if valid; otherwise, null.</returns>
    public static EgyptianNationalId? ToEgyptianNationalId(this string value, bool validateChecksum = false)
    {
        return EgyptianNationalId.TryCreate(value, out var nationalId, validateChecksum)
            ? nationalId
            : null;
    }

    /// <summary>
    /// Tries to parse the string as an Egyptian National ID.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="nationalId">The resulting National ID if parsing succeeds.</param>
    /// <param name="validateChecksum">
    /// Whether to validate the checksum. Default is FALSE.
    /// Set to TRUE only if you have the verified checksum algorithm.
    /// </param>
    /// <returns>True if parsing succeeds; otherwise, false.</returns>
    public static bool TryParseAsNationalId(this string value, out EgyptianNationalId? nationalId, bool validateChecksum = false)
    {
        return EgyptianNationalId.TryCreate(value, out nationalId, validateChecksum);
    }

    /// <summary>
    /// Validates only the format of the National ID without checking domain rules.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the format is valid (14 digits); otherwise, false.</returns>
    public static bool HasValidNationalIdFormat(this string value)
    {
        return EgyptianNationalId.IsValidFormat(value);
    }

    /// <summary>
    /// Validates the checksum of the National ID.
    /// </summary>
    /// <param name="value">The National ID to validate.</param>
    /// <returns>True if the checksum is valid; otherwise, false.</returns>
    public static bool HasValidNationalIdChecksum(this string value)
    {
        return EgyptianNationalId.ValidateChecksum(value);
    }
}
