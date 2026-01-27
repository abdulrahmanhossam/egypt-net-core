using System.ComponentModel;

namespace Egypt.Net.Core;

/// <summary>
/// Represents Egyptian governorates with their official codes.
/// </summary>
public enum Governorate
{
    [Description("القاهرة")]
    Cairo = 1,

    [Description("الإسكندرية")]
    Alexandria = 2,

    [Description("بورسعيد")]
    PortSaid = 3,

    [Description("السويس")]
    Suez = 4,

    [Description("دمياط")]
    Damietta = 11,

    [Description("الدقهلية")]
    Dakahlia = 12,

    [Description("الشرقية")]
    Sharqia = 13,

    [Description("القليوبية")]
    Qalyubia = 14,

    [Description("كفر الشيخ")]
    KafrElSheikh = 15,

    [Description("الغربية")]
    Gharbia = 16,

    [Description("المنوفية")]
    Monufia = 17,

    [Description("البحيرة")]
    Beheira = 18,

    [Description("الإسماعيلية")]
    Ismailia = 19,

    [Description("الجيزة")]
    Giza = 21,

    [Description("بني سويف")]
    BeniSuef = 22,

    [Description("الفيوم")]
    Fayoum = 23,

    [Description("المنيا")]
    Minya = 24,

    [Description("أسيوط")]
    Asyut = 25,

    [Description("سوهاج")]
    Sohag = 26,

    [Description("قنا")]
    Qena = 27,

    [Description("أسوان")]
    Aswan = 28,

    [Description("الأقصر")]
    Luxor = 29,

    [Description("البحر الأحمر")]
    RedSea = 31,

    [Description("الوادي الجديد")]
    NewValley = 32,

    [Description("مطروح")]
    Matrouh = 33,

    [Description("شمال سيناء")]
    NorthSinai = 34,

    [Description("جنوب سيناء")]
    SouthSinai = 35,

    [Description("خارج الجمهورية")]
    Foreign = 88
}
