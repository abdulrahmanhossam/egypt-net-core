using Egypt.Net.Core;

namespace Egypt.Net.Core.Tests;

/// <summary>
/// Tests for Arabic governorate names and localization
/// </summary>
public class GovernorateArabicNamesTests
{
    [Fact]
    public void GetArabicName_ShouldReturnCorrectArabicName_ForCairo()
    {
        var governorate = Governorate.Cairo;

        var arabicName = governorate.GetArabicName();

        Assert.Equal("القاهرة", arabicName);
    }

    [Fact]
    public void GetArabicName_ShouldReturnCorrectArabicName_ForAlexandria()
    {
        var governorate = Governorate.Alexandria;

        var arabicName = governorate.GetArabicName();

        Assert.Equal("الإسكندرية", arabicName);
    }

    [Fact]
    public void GetArabicName_ShouldReturnCorrectArabicName_ForGiza()
    {
        var governorate = Governorate.Giza;

        var arabicName = governorate.GetArabicName();

        Assert.Equal("الجيزة", arabicName);
    }

    [Fact]
    public void GetEnglishName_ShouldReturnCorrectEnglishName()
    {
        var governorate = Governorate.Cairo;

        var englishName = governorate.GetEnglishName();

        Assert.Equal("Cairo", englishName);
    }

    [Fact]
    public void GetBothNames_ShouldReturnBothArabicAndEnglish()
    {
        var governorate = Governorate.Cairo;

        var (arabic, english) = governorate.GetBothNames();

        Assert.Equal("القاهرة", arabic);
        Assert.Equal("Cairo", english);
    }

    [Fact]
    public void NationalId_GovernorateNameAr_ShouldReturnArabicName()
    {
        var id = new EgyptianNationalId("30101011234567", validateChecksum: false);

        Assert.Equal("القاهرة", id.GovernorateNameAr);
    }

    [Fact]
    public void NationalId_GovernorateNameEn_ShouldReturnEnglishName()
    {
        var id = new EgyptianNationalId("30101011234567", validateChecksum: false);

        Assert.Equal("Cairo", id.GovernorateNameEn);
    }

    [Fact]
    public void NationalId_GenderAr_ShouldReturnArabicGender_Male()
    {
        var id = new EgyptianNationalId("30101011234567", validateChecksum: false); // Male

        Assert.Equal("ذكر", id.GenderAr);
    }

    [Fact]
    public void NationalId_GenderAr_ShouldReturnArabicGender_Female()
    {
        var id = new EgyptianNationalId("30101011234568", validateChecksum: false); // Female

        Assert.Equal("أنثى", id.GenderAr);
    }

    [Theory]
    [InlineData(Governorate.Cairo, "القاهرة")]
    [InlineData(Governorate.Alexandria, "الإسكندرية")]
    [InlineData(Governorate.Giza, "الجيزة")]
    [InlineData(Governorate.Aswan, "أسوان")]
    [InlineData(Governorate.Luxor, "الأقصر")]
    [InlineData(Governorate.Asyut, "أسيوط")]
    [InlineData(Governorate.Dakahlia, "الدقهلية")]
    [InlineData(Governorate.Sharqia, "الشرقية")]
    [InlineData(Governorate.Foreign, "خارج الجمهورية")]
    public void GetArabicName_ShouldReturnCorrectName_ForAllGovernorates(Governorate governorate, string expectedArabicName)
    {
        var arabicName = governorate.GetArabicName();

        Assert.Equal(expectedArabicName, arabicName);
    }
}
