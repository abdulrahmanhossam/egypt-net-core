using Egypt.Net.Core.Enums;

namespace Egypt.Net.Core.Tests;

public class GenerationTests
{
    [Theory]
    [InlineData("24001010123456", Generation.SilentGeneration)]  // Born 1940
    [InlineData("25001010123451", Generation.BabyBoomers)]       // Born 1950
    [InlineData("27001010123459", Generation.GenerationX)]       // Born 1970
    [InlineData("29001010123452", Generation.Millennials)]       // Born 1990
    [InlineData("30001010123452", Generation.GenerationZ)]       // Born 2000
    [InlineData("31501010123459", Generation.GenerationAlpha)]   // Born 2015
    public void Generation_ShouldReturnCorrectGeneration(string nationalId, Generation expectedGeneration)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expectedGeneration, id.Generation);
    }

    [Theory]
    [InlineData("24001010123456", "الجيل الصامت")]  // Silent Generation
    [InlineData("25001010123451", "جيل الطفرة")]     // Baby Boomers
    [InlineData("27001010123459", "الجيل إكس")]      // Gen X
    [InlineData("29001010123452", "جيل الألفية")]    // Millennials
    [InlineData("30001010123452", "جيل زد")]         // Gen Z
    [InlineData("31501010123459", "جيل ألفا")]       // Gen Alpha
    public void GenerationAr_ShouldReturnArabicName(string nationalId, string expectedName)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expectedName, id.GenerationAr);
    }

    [Theory]
    [InlineData("24001010123456", "SilentGeneration")]
    [InlineData("25001010123451", "BabyBoomers")]
    [InlineData("27001010123459", "GenerationX")]
    [InlineData("29001010123452", "Millennials")]
    [InlineData("30001010123452", "GenerationZ")]
    [InlineData("31501010123459", "GenerationAlpha")]
    public void GenerationEn_ShouldReturnEnglishName(string nationalId, string expectedName)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expectedName, id.GenerationEn);
    }

    [Theory]
    [InlineData("24001010123456", false)]  // Silent Generation - not digital native
    [InlineData("25001010123451", false)]  // Baby Boomers - not digital native
    [InlineData("27001010123459", false)]  // Gen X - not digital native
    [InlineData("29001010123452", true)]   // Millennials - digital native
    [InlineData("30001010123452", true)]   // Gen Z - digital native
    [InlineData("31501010123459", true)]   // Gen Alpha - digital native
    public void IsDigitalNative_ShouldReturnCorrectValue(string nationalId, bool expected)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expected, id.IsDigitalNative);
    }

    [Fact]
    public void GenerationExtensions_GetArabicName_ShouldReturnCorrectNames()
    {
        // Arrange & Act & Assert
        Assert.Equal("الجيل الصامت", Generation.SilentGeneration.GetArabicName());
        Assert.Equal("جيل الطفرة", Generation.BabyBoomers.GetArabicName());
        Assert.Equal("الجيل إكس", Generation.GenerationX.GetArabicName());
        Assert.Equal("جيل الألفية", Generation.Millennials.GetArabicName());
        Assert.Equal("جيل زد", Generation.GenerationZ.GetArabicName());
        Assert.Equal("جيل ألفا", Generation.GenerationAlpha.GetArabicName());
    }

    [Fact]
    public void GenerationExtensions_GetEnglishName_ShouldReturnCorrectNames()
    {
        // Arrange & Act & Assert
        Assert.Equal("SilentGeneration", Generation.SilentGeneration.GetEnglishName());
        Assert.Equal("BabyBoomers", Generation.BabyBoomers.GetEnglishName());
        Assert.Equal("GenerationX", Generation.GenerationX.GetEnglishName());
        Assert.Equal("Millennials", Generation.Millennials.GetEnglishName());
        Assert.Equal("GenerationZ", Generation.GenerationZ.GetEnglishName());
        Assert.Equal("GenerationAlpha", Generation.GenerationAlpha.GetEnglishName());
    }

    [Fact]
    public void GenerationExtensions_GetBothNames_ShouldReturnTuple()
    {
        // Arrange & Act
        var (arabic, english) = Generation.Millennials.GetBothNames();

        // Assert
        Assert.Equal("جيل الألفية", arabic);
        Assert.Equal("Millennials", english);
    }

    [Fact]
    public void GenerationExtensions_GetYearRange_ShouldReturnCorrectRanges()
    {
        // Arrange & Act & Assert
        Assert.Equal((1928, 1945), Generation.SilentGeneration.GetYearRange());
        Assert.Equal((1946, 1964), Generation.BabyBoomers.GetYearRange());
        Assert.Equal((1965, 1980), Generation.GenerationX.GetYearRange());
        Assert.Equal((1981, 1996), Generation.Millennials.GetYearRange());
        Assert.Equal((1997, 2012), Generation.GenerationZ.GetYearRange());
        // Gen Alpha ends at current year
        var (alphaStart, alphaEnd) = Generation.GenerationAlpha.GetYearRange();
        Assert.Equal(2013, alphaStart);
        Assert.Equal(DateTime.Today.Year, alphaEnd);
    }

    [Theory]
    [InlineData(1920, Generation.SilentGeneration)]  // Before range
    [InlineData(1928, Generation.SilentGeneration)]  // Start of Silent
    [InlineData(1945, Generation.SilentGeneration)]  // End of Silent
    [InlineData(1946, Generation.BabyBoomers)]       // Start of Boomers
    [InlineData(1964, Generation.BabyBoomers)]       // End of Boomers
    [InlineData(1965, Generation.GenerationX)]       // Start of Gen X
    [InlineData(1980, Generation.GenerationX)]       // End of Gen X
    [InlineData(1981, Generation.Millennials)]       // Start of Millennials
    [InlineData(1996, Generation.Millennials)]       // End of Millennials
    [InlineData(1997, Generation.GenerationZ)]       // Start of Gen Z
    [InlineData(2012, Generation.GenerationZ)]       // End of Gen Z
    [InlineData(2013, Generation.GenerationAlpha)]   // Start of Gen Alpha
    [InlineData(2020, Generation.GenerationAlpha)]   // Recent Gen Alpha
    public void GetGenerationFromYear_ShouldReturnCorrectGeneration(int year, Generation expected)
    {
        // Arrange & Act
        var generation = GenerationExtensions.GetGenerationFromYear(year);

        // Assert
        Assert.Equal(expected, generation);
    }

    [Fact]
    public void IsDigitalNative_Extension_ShouldWorkCorrectly()
    {
        // Arrange & Act & Assert
        Assert.False(Generation.SilentGeneration.IsDigitalNative());
        Assert.False(Generation.BabyBoomers.IsDigitalNative());
        Assert.False(Generation.GenerationX.IsDigitalNative());
        Assert.True(Generation.Millennials.IsDigitalNative());
        Assert.True(Generation.GenerationZ.IsDigitalNative());
        Assert.True(Generation.GenerationAlpha.IsDigitalNative());
    }

    [Fact]
    public void Generation_ShouldBe_ConsistentWithBirthYear()
    {
        // Arrange
        var id = new EgyptianNationalId("29001010123452"); // Born 1990

        // Act
        var generation = id.Generation;
        var birthYear = id.BirthYear;

        // Assert
        Assert.Equal(Generation.Millennials, generation);
        Assert.Equal(1990, birthYear);
        Assert.True(birthYear >= 1981 && birthYear <= 1996); // Millennials range
    }

    [Theory]
    [InlineData("23001010123454")] // Born 1930 - Silent Generation
    [InlineData("26001010123456")] // Born 1960 - Baby Boomers
    [InlineData("28001010123452")] // Born 1980 - Gen X
    [InlineData("29501010123457")] // Born 1995 - Millennials
    [InlineData("30501010123459")] // Born 2005 - Gen Z
    [InlineData("31801010123450")] // Born 2018 - Gen Alpha
    public void Generation_Properties_ShouldAllBeConsistent(string nationalId)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        var expectedGeneration = GenerationExtensions.GetGenerationFromYear(id.BirthYear);
        Assert.Equal(expectedGeneration, id.Generation);
        Assert.Equal(expectedGeneration.GetArabicName(), id.GenerationAr);
        Assert.Equal(expectedGeneration.GetEnglishName(), id.GenerationEn);
        Assert.Equal(expectedGeneration.IsDigitalNative(), id.IsDigitalNative);
    }
}