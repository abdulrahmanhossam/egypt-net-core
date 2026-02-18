using System.ComponentModel;

namespace Egypt.Net.Core.Enums;

/// <summary>
/// Represents the generational cohorts based on birth year.
/// These are Western generational definitions adapted for Egyptian context.
/// </summary>
public enum Generation
{
    /// <summary>
    /// Silent Generation (1928-1945) - الجيل الصامت
    /// Born during the Great Depression and World War II.
    /// Known for traditional values and strong work ethic.
    /// </summary>
    [Description("الجيل الصامت")]
    SilentGeneration = 1,

    /// <summary>
    /// Baby Boomers (1946-1964) - جيل الطفرة
    /// Post-WWII birth surge generation.
    /// Witnessed economic growth and social change.
    /// </summary>
    [Description("جيل الطفرة")]
    BabyBoomers = 2,

    /// <summary>
    /// Generation X (1965-1980) - الجيل إكس
    /// Bridge between analog and digital worlds.
    /// Independent and adaptable.
    /// </summary>
    [Description("الجيل إكس")]
    GenerationX = 3,

    /// <summary>
    /// Millennials (1981-1996) - جيل الألفية
    /// First digital natives, came of age with internet.
    /// Tech-savvy and socially conscious.
    /// </summary>
    [Description("جيل الألفية")]
    Millennials = 4,

    /// <summary>
    /// Generation Z (1997-2012) - جيل زد
    /// True digital natives, grew up with smartphones.
    /// Diverse, entrepreneurial, and socially aware.
    /// </summary>
    [Description("جيل زد")]
    GenerationZ = 5,

    /// <summary>
    /// Generation Alpha (2013-present) - جيل ألفا
    /// Born entirely in the 21st century.
    /// Growing up with AI, tablets, and social media.
    /// </summary>
    [Description("جيل ألفا")]
    GenerationAlpha = 6
}