using System.ComponentModel;
using System.Reflection;

namespace Egypt.Net.Core;

/// <summary>
/// Extension methods for the Governorate enum.
/// </summary>
public static class GovernorateExtensions
{
    /// <summary>
    /// Gets the Arabic name of the governorate.
    /// </summary>
    /// <param name="governorate">The governorate enum value.</param>
    /// <returns>The Arabic name of the governorate.</returns>
    public static string GetArabicName(this Governorate governorate)
    {
        var field = governorate.GetType().GetField(governorate.ToString());

        if (field == null)
            return governorate.ToString();

        var attribute = field.GetCustomAttribute<DescriptionAttribute>();

        return attribute?.Description ?? governorate.ToString();
    }

    /// <summary>
    /// Gets the English name of the governorate.
    /// </summary>
    /// <param name="governorate">The governorate enum value.</param>
    /// <returns>The English name of the governorate.</returns>
    public static string GetEnglishName(this Governorate governorate)
    {
        return governorate.ToString();
    }

    /// <summary>
    /// Gets both Arabic and English names of the governorate.
    /// </summary>
    /// <param name="governorate">The governorate enum value.</param>
    /// <returns>A tuple containing (ArabicName, EnglishName).</returns>
    public static (string Arabic, string English) GetBothNames(this Governorate governorate)
    {
        return (governorate.GetArabicName(), governorate.GetEnglishName());
    }
}
