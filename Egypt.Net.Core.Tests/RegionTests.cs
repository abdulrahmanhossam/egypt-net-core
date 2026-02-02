namespace Egypt.Net.Core.Tests;

public class RegionTests
{
    // Format: C YY MM DD GG SSSS X
    //         3 01 01 01 01 2345 8  = Cairo (01), Born 2001-01-01
    //         3 01 01 01 21 2345 0  = Giza (21), Born 2001-01-01

    [Theory]
    [InlineData("30101010123458", Region.GreaterCairo)]  // Cairo (01)
    [InlineData("30101012112340", Region.GreaterCairo)]  // Giza (21)
    [InlineData("30101011412355", Region.GreaterCairo)]  // Qalyubia (14)
    [InlineData("30101010212351", Region.Delta)]          // Alexandria (02)
    [InlineData("30101011212357", Region.Delta)]          // Dakahlia (12)
    [InlineData("30101010312354", Region.Canal)]          // Port Said (03)
    [InlineData("30101010412357", Region.Canal)]          // Suez (04)
    [InlineData("30101012212353", Region.UpperEgypt)]     // Beni Suef (22)
    [InlineData("30101012512359", Region.UpperEgypt)]     // Asyut (25)
    [InlineData("30101013112356", Region.SinaiAndRedSea)] // Red Sea (31)
    [InlineData("30101013412353", Region.SinaiAndRedSea)] // North Sinai (34)
    [InlineData("30101013212359", Region.WesternDesert)]  // New Valley (32)
    [InlineData("30101018812355", Region.Foreign)]        // Foreign (88)
    public void BirthRegion_ShouldReturnCorrectRegion(string nationalId, Region expectedRegion)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expectedRegion, id.BirthRegion);
    }

    [Theory]
    [InlineData("30101010123458", "القاهرة الكبرى")]  // Cairo
    [InlineData("30101011212357", "الدلتا")]          // Dakahlia
    [InlineData("30101012512359", "الصعيد")]          // Asyut
    [InlineData("30101013112356", "سيناء والبحر الأحمر")] // Red Sea
    public void BirthRegionNameAr_ShouldReturnArabicName(string nationalId, string expectedName)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expectedName, id.BirthRegionNameAr);
    }

    [Theory]
    [InlineData("30101010123458", "GreaterCairo")]
    [InlineData("30101011212357", "Delta")]
    [InlineData("30101012512359", "UpperEgypt")]
    [InlineData("30101013112356", "SinaiAndRedSea")]
    public void BirthRegionNameEn_ShouldReturnEnglishName(string nationalId, string expectedName)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expectedName, id.BirthRegionNameEn);
    }

    [Theory]
    [InlineData("30101012212353", true)]   // Beni Suef (22) - Upper Egypt
    [InlineData("30101012512359", true)]   // Asyut (25) - Upper Egypt
    [InlineData("30101012812355", true)]   // Aswan (28) - Upper Egypt
    [InlineData("30101010123458", false)]  // Cairo (01) - not Upper Egypt
    [InlineData("30101011212357", false)]  // Dakahlia (12) - not Upper Egypt
    public void IsFromUpperEgypt_ShouldReturnCorrectValue(string nationalId, bool expected)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expected, id.IsFromUpperEgypt);
    }

    [Theory]
    [InlineData("30101010123458", true)]   // Cairo (01) - Lower Egypt
    [InlineData("30101012112340", true)]   // Giza (21) - Lower Egypt
    [InlineData("30101011212357", true)]   // Dakahlia (12) - Lower Egypt (Delta)
    [InlineData("30101010212351", true)]   // Alexandria (02) - Lower Egypt (Delta)
    [InlineData("30101012512359", false)]  // Asyut (25) - Upper Egypt
    [InlineData("30101013112356", false)]  // Red Sea (31) - not Lower Egypt
    public void IsFromLowerEgypt_ShouldReturnCorrectValue(string nationalId, bool expected)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expected, id.IsFromLowerEgypt);
    }

    [Theory]
    [InlineData("30101010123458", true)]   // Cairo (01) - Greater Cairo
    [InlineData("30101012112340", true)]   // Giza (21) - Greater Cairo
    [InlineData("30101011212357", false)]  // Dakahlia (12) - not Greater Cairo
    public void IsFromGreaterCairo_ShouldReturnCorrectValue(string nationalId, bool expected)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expected, id.IsFromGreaterCairo);
    }

    [Theory]
    [InlineData("30101011212357", true)]   // Dakahlia (12) - Delta
    [InlineData("30101010212351", true)]   // Alexandria (02) - Delta
    [InlineData("30101010123458", false)]  // Cairo (01) - not Delta
    public void IsFromDelta_ShouldReturnCorrectValue(string nationalId, bool expected)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expected, id.IsFromDelta);
    }

    [Theory]
    [InlineData("30101013112356", true)]   // Red Sea (31) - Sinai
    [InlineData("30101013412353", true)]   // North Sinai (34) - Sinai
    [InlineData("30101010123458", false)]  // Cairo (01) - not Sinai
    public void IsFromSinai_ShouldReturnCorrectValue(string nationalId, bool expected)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expected, id.IsFromSinai);
    }

    [Theory]
    [InlineData("30101018812355", true)]   // Foreign (88) - born abroad
    [InlineData("30101010123458", false)]  // Cairo (01) - not born abroad
    public void IsBornAbroad_ShouldReturnCorrectValue(string nationalId, bool expected)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expected, id.IsBornAbroad);
    }

    [Theory]
    [InlineData("30101010212351", true)]   // Alexandria (02) - coastal (Delta)
    [InlineData("30101010312354", true)]   // Port Said (03) - coastal (Canal)
    [InlineData("30101013112356", true)]   // Red Sea (31) - coastal
    [InlineData("30101013312352", true)]   // Matrouh (33) - coastal
    [InlineData("30101010123458", false)]  // Cairo (01) - not coastal
    [InlineData("30101012512359", false)]  // Asyut (25) - not coastal
    public void IsFromCoastalRegion_ShouldReturnCorrectValue(string nationalId, bool expected)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expected, id.IsFromCoastalRegion);
    }

    [Fact]
    public void RegionExtensions_GetArabicName_ShouldReturnArabicNames()
    {
        // Arrange & Act & Assert
        Assert.Equal("القاهرة الكبرى", Region.GreaterCairo.GetArabicName());
        Assert.Equal("الدلتا", Region.Delta.GetArabicName());
        Assert.Equal("الصعيد", Region.UpperEgypt.GetArabicName());
        Assert.Equal("سيناء والبحر الأحمر", Region.SinaiAndRedSea.GetArabicName());
    }

    [Fact]
    public void RegionExtensions_GetEnglishName_ShouldReturnEnglishNames()
    {
        // Arrange & Act & Assert
        Assert.Equal("GreaterCairo", Region.GreaterCairo.GetEnglishName());
        Assert.Equal("Delta", Region.Delta.GetEnglishName());
        Assert.Equal("UpperEgypt", Region.UpperEgypt.GetEnglishName());
        Assert.Equal("SinaiAndRedSea", Region.SinaiAndRedSea.GetEnglishName());
    }

    [Fact]
    public void RegionExtensions_GetBothNames_ShouldReturnTuple()
    {
        // Arrange & Act
        var (arabic, english) = Region.GreaterCairo.GetBothNames();

        // Assert
        Assert.Equal("القاهرة الكبرى", arabic);
        Assert.Equal("GreaterCairo", english);
    }

    [Fact]
    public void RegionExtensions_IsUpperEgypt_ShouldWorkCorrectly()
    {
        // Arrange & Act & Assert
        Assert.True(Region.UpperEgypt.IsUpperEgypt());
        Assert.False(Region.GreaterCairo.IsUpperEgypt());
        Assert.False(Region.Delta.IsUpperEgypt());
    }

    [Fact]
    public void RegionExtensions_IsLowerEgypt_ShouldWorkCorrectly()
    {
        // Arrange & Act & Assert
        Assert.True(Region.GreaterCairo.IsLowerEgypt());
        Assert.True(Region.Delta.IsLowerEgypt());
        Assert.False(Region.UpperEgypt.IsLowerEgypt());
        Assert.False(Region.Canal.IsLowerEgypt());
    }

    [Fact]
    public void GovernorateExtensions_GetRegion_ShouldMapAllGovernorates()
    {
        // Test Greater Cairo
        Assert.Equal(Region.GreaterCairo, Governorate.Cairo.GetRegion());
        Assert.Equal(Region.GreaterCairo, Governorate.Giza.GetRegion());
        Assert.Equal(Region.GreaterCairo, Governorate.Qalyubia.GetRegion());

        // Test Delta
        Assert.Equal(Region.Delta, Governorate.Alexandria.GetRegion());
        Assert.Equal(Region.Delta, Governorate.Dakahlia.GetRegion());

        // Test Canal
        Assert.Equal(Region.Canal, Governorate.PortSaid.GetRegion());
        Assert.Equal(Region.Canal, Governorate.Suez.GetRegion());

        // Test Upper Egypt
        Assert.Equal(Region.UpperEgypt, Governorate.Asyut.GetRegion());
        Assert.Equal(Region.UpperEgypt, Governorate.Luxor.GetRegion());

        // Test Sinai & Red Sea
        Assert.Equal(Region.SinaiAndRedSea, Governorate.RedSea.GetRegion());
        Assert.Equal(Region.SinaiAndRedSea, Governorate.NorthSinai.GetRegion());

        // Test Western Desert
        Assert.Equal(Region.WesternDesert, Governorate.NewValley.GetRegion());
        Assert.Equal(Region.WesternDesert, Governorate.Matrouh.GetRegion());

        // Test Foreign
        Assert.Equal(Region.Foreign, Governorate.Foreign.GetRegion());
    }
}
