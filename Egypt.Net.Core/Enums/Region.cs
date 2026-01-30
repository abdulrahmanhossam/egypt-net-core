using System.ComponentModel;

namespace Egypt.Net.Core;

/// <summary>
/// Represents the geographic regions of Egypt.
/// Used to classify governorates into broader geographic areas.
/// </summary>
public enum Region
{
    /// <summary>
    /// Greater Cairo region (القاهرة الكبرى)
    /// Includes: Cairo, Giza, Qalyubia
    /// </summary>
    [Description("القاهرة الكبرى")]
    GreaterCairo = 1,

    /// <summary>
    /// Nile Delta region (الدلتا)
    /// Includes: Alexandria, Dakahlia, Sharqia, Gharbia,
    ///           Kafr El-Sheikh, Monufia, Beheira, Damietta
    /// </summary>
    [Description("الدلتا")]
    Delta = 2,

    /// <summary>
    /// Suez Canal region (قناة السويس)
    /// Includes: Port Said, Suez, Ismailia
    /// </summary>
    [Description("قناة السويس")]
    Canal = 3,

    /// <summary>
    /// Upper Egypt region (الصعيد)
    /// Includes: Beni Suef, Fayoum, Minya, Asyut,
    ///           Sohag, Qena, Aswan, Luxor
    /// </summary>
    [Description("الصعيد")]
    UpperEgypt = 4,

    /// <summary>
    /// Sinai and Red Sea region (سيناء والبحر الأحمر)
    /// Includes: North Sinai, South Sinai, Red Sea
    /// </summary>
    [Description("سيناء والبحر الأحمر")]
    SinaiAndRedSea = 5,

    /// <summary>
    /// Western Desert region (الصحراء الغربية)
    /// Includes: New Valley, Matrouh
    /// </summary>
    [Description("الصحراء الغربية")]
    WesternDesert = 6,

    /// <summary>
    /// Foreign (outside Egypt) - خارج الجمهورية
    /// For births that occurred abroad
    /// </summary>
    [Description("خارج الجمهورية")]
    Foreign = 7
}
