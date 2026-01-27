namespace Egypt.Net.Core;

/// <summary>
/// Provides formatting options for Egyptian National IDs.
/// </summary>
public static class EgyptianNationalIdFormatter
{
    /// <summary>
    /// Formats the National ID with dashes for readability.
    /// Format: C-YYMMDD-GG-SSSS
    /// Example: 3-010101-01-2345
    /// Where:
    /// - C: Century digit
    /// - YY: Year
    /// - MM: Month
    /// - DD: Day
    /// - GG: Governorate code
    /// - SSSS: Serial number (including gender digit)
    /// </summary>
    public static string FormatWithDashes(this EgyptianNationalId nationalId)
    {
        var value = nationalId.Value;
        return $"{value[0]}-{value.Substring(1, 6)}-{value.Substring(7, 2)}-{value.Substring(9, 5)}";
    }

    /// <summary>
    /// Formats the National ID with spaces for readability.
    /// Format: C YYMMDD GG SSSS
    /// Example: 3 010101 01 2345
    /// </summary>
    public static string FormatWithSpaces(this EgyptianNationalId nationalId)
    {
        var value = nationalId.Value;
        return $"{value[0]} {value.Substring(1, 6)} {value.Substring(7, 2)} {value.Substring(9, 5)}";
    }

    /// <summary>
    /// Formats the National ID in a structured, human-readable format.
    /// Example output:
    /// Century: 3 (2000s)
    /// Birth Date: 01/01/2001
    /// Governorate: 01 (Cairo)
    /// Serial: 2345
    /// </summary>
    public static string FormatDetailed(this EgyptianNationalId nationalId)
    {
        var centuryText = nationalId.Value[0] == '2' ? "1900s" : "2000s";

        return $"Century: {nationalId.Value[0]} ({centuryText})\n" +
               $"Birth Date: {nationalId.BirthDate:dd/MM/yyyy}\n" +
               $"Governorate: {nationalId.GovernorateCode:D2} ({nationalId.Governorate})\n" +
               $"Serial: {nationalId.SerialNumber:D4}\n" +
               $"Gender: {nationalId.Gender}";
    }

    /// <summary>
    /// Masks the National ID for privacy, showing only the first 3 and last 2 digits.
    /// Example: 301********67
    /// </summary>
    public static string FormatMasked(this EgyptianNationalId nationalId)
    {
        var value = nationalId.Value;
        return $"{value.Substring(0, 3)}********{value.Substring(12, 2)}";
    }

    /// <summary>
    /// Formats the National ID with grouping by logical segments.
    /// Format: [C][YYMMDD][GG][SSSS]
    /// Example: [3][010101][01][2345]
    /// </summary>
    public static string FormatWithBrackets(this EgyptianNationalId nationalId)
    {
        var value = nationalId.Value;
        return $"[{value[0]}][{value.Substring(1, 6)}][{value.Substring(7, 2)}][{value.Substring(9, 5)}]";
    }
}
