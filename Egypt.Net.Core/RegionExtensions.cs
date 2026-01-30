using System.ComponentModel;
using System.Reflection;

namespace Egypt.Net.Core;

/// <summary>
/// Extension methods for the Region enum.
/// Provides helper methods for working with geographic regions.
/// </summary>
public static class RegionExtensions
{
    /// <summary>
    /// Gets the Arabic name of the region.
    /// </summary>
    /// <param name="region">The region enum value.</param>
    /// <returns>The Arabic name of the region.</returns>
    public static string GetArabicName(this Region region)
    {
        var field = region.GetType().GetField(region.ToString());

        if (field == null)
            return region.ToString();

        var attribute = field.GetCustomAttribute<DescriptionAttribute>();

        return attribute?.Description ?? region.ToString();
    }

    /// <summary>
    /// Gets the English name of the region.
    /// </summary>
    /// <param name="region">The region enum value.</param>
    /// <returns>The English name of the region.</returns>
    public static string GetEnglishName(this Region region)
    {
        return region.ToString();
    }

    /// <summary>
    /// Gets both Arabic and English names of the region.
    /// </summary>
    /// <param name="region">The region enum value.</param>
    /// <returns>A tuple containing (ArabicName, EnglishName).</returns>
    public static (string Arabic, string English) GetBothNames(this Region region)
    {
        return (region.GetArabicName(), region.GetEnglishName());
    }

    /// <summary>
    /// Determines whether the region is in Upper Egypt (الصعيد).
    /// </summary>
    /// <param name="region">The region to check.</param>
    /// <returns>True if the region is Upper Egypt; otherwise, false.</returns>
    public static bool IsUpperEgypt(this Region region)
    {
        return region == Region.UpperEgypt;
    }

    /// <summary>
    /// Determines whether the region is in Lower Egypt (الدلتا والقاهرة الكبرى).
    /// Lower Egypt traditionally includes the Delta and Greater Cairo.
    /// </summary>
    /// <param name="region">The region to check.</param>
    /// <returns>True if the region is in Lower Egypt; otherwise, false.</returns>
    public static bool IsLowerEgypt(this Region region)
    {
        return region == Region.GreaterCairo || region == Region.Delta;
    }

    /// <summary>
    /// Determines whether the region is coastal (has Mediterranean or Red Sea coast).
    /// </summary>
    /// <param name="region">The region to check.</param>
    /// <returns>True if the region is coastal; otherwise, false.</returns>
    public static bool IsCoastal(this Region region)
    {
        return region == Region.Delta ||           // Mediterranean coast
               region == Region.Canal ||           // Suez Canal & Red Sea
               region == Region.SinaiAndRedSea ||  // Red Sea coast
               region == Region.WesternDesert;     // Mediterranean coast (Matrouh)
    }
}
