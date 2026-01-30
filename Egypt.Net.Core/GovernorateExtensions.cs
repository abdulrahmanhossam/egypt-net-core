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

    /// <summary>
    /// Gets the geographic region for the governorate.
    /// </summary>
    /// <param name="governorate">The governorate enum value.</param>
    /// <returns>The region that contains this governorate.</returns>
    public static Region GetRegion(this Governorate governorate)
    {
        return governorate switch
        {
            // Greater Cairo (القاهرة الكبرى)
            Governorate.Cairo => Region.GreaterCairo,
            Governorate.Giza => Region.GreaterCairo,
            Governorate.Qalyubia => Region.GreaterCairo,

            // Delta (الدلتا)
            Governorate.Alexandria => Region.Delta,
            Governorate.Damietta => Region.Delta,
            Governorate.Dakahlia => Region.Delta,
            Governorate.Sharqia => Region.Delta,
            Governorate.KafrElSheikh => Region.Delta,
            Governorate.Gharbia => Region.Delta,
            Governorate.Monufia => Region.Delta,
            Governorate.Beheira => Region.Delta,

            // Canal (قناة السويس)
            Governorate.PortSaid => Region.Canal,
            Governorate.Suez => Region.Canal,
            Governorate.Ismailia => Region.Canal,

            // Upper Egypt (الصعيد)
            Governorate.BeniSuef => Region.UpperEgypt,
            Governorate.Fayoum => Region.UpperEgypt,
            Governorate.Minya => Region.UpperEgypt,
            Governorate.Asyut => Region.UpperEgypt,
            Governorate.Sohag => Region.UpperEgypt,
            Governorate.Qena => Region.UpperEgypt,
            Governorate.Aswan => Region.UpperEgypt,
            Governorate.Luxor => Region.UpperEgypt,

            // Sinai & Red Sea (سيناء والبحر الأحمر)
            Governorate.RedSea => Region.SinaiAndRedSea,
            Governorate.NorthSinai => Region.SinaiAndRedSea,
            Governorate.SouthSinai => Region.SinaiAndRedSea,

            // Western Desert (الصحراء الغربية)
            Governorate.NewValley => Region.WesternDesert,
            Governorate.Matrouh => Region.WesternDesert,

            // Foreign (خارج الجمهورية)
            Governorate.Foreign => Region.Foreign,

            _ => throw new InvalidOperationException($"Unknown governorate: {governorate}")
        };
    }
}
