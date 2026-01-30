namespace Egypt.Net.Core.Tests;

public class RegionTests
{
    [Theory]
    [InlineData("30101010123458", Region.GreaterCairo)]  // Cairo
    [InlineData("30221010123456", Region.GreaterCairo)]  // Giza
    [InlineData("30214010123451", Region.GreaterCairo)]  // Qalyubia
    [InlineData("30202010123459", Region.Delta)]          // Alexandria
    [InlineData("30212010123453", Region.Delta)]          // Dakahlia
    [InlineData("30303010123452", Region.Canal)]          // Port Said
    [InlineData("30304010123454", Region.Canal)]          // Suez
    [InlineData("30222010123458", Region.UpperEgypt)]     // Beni Suef
    [InlineData("30225010123454", Region.UpperEgypt)]     // Asyut
    [InlineData("30331010123451", Region.SinaiAndRedSea)] // Red Sea
    [InlineData("30334010123458", Region.SinaiAndRedSea)] // North Sinai
    [InlineData("30332010123453", Region.WesternDesert)]  // New Valley
    [InlineData("30888010123450", Region.Foreign)]        // Foreign
    public void BirthRegion_ShouldReturnCorrectRegion(string nationalId, Region expectedRegion)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expectedRegion, id.BirthRegion);
    }

    [Theory]
    [InlineData("30101010123458", "القاهرة الكبرى")]  // Cairo
    [InlineData("30212010123453", "الدلتا")]          // Dakahlia
    [InlineData("30225010123454", "الصعيد")]          // Asyut
    [InlineData("30331010123451", "سيناء والبحر الأحمر")] // Red Sea
    public void BirthRegionNameAr_ShouldReturnArabicName(string nationalId, string expectedName)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expectedName, id.BirthRegionNameAr);
    }

    [Theory]
    [InlineData("30101010123458", "GreaterCairo")]
    [InlineData("30212010123453", "Delta")]
    [InlineData("30225010123454", "UpperEgypt")]
    [InlineData("30331010123451", "SinaiAndRedSea")]
    public void BirthRegionNameEn_ShouldReturnEnglishName(string nationalId, string expectedName)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expectedName, id.BirthRegionNameEn);
    }

    [Theory]
    [InlineData("30222010123458", true)]   // Beni Suef - Upper Egypt
    [InlineData("30225010123454", true)]   // Asyut - Upper Egypt
    [InlineData("30228010123450", true)]   // Aswan - Upper Egypt
    [InlineData("30101010123458", false)]  // Cairo - not Upper Egypt
    [InlineData("30212010123453", false)]  // Dakahlia - not Upper Egypt
    public void IsFromUpperEgypt_ShouldReturnCorrectValue(string nationalId, bool expected)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expected, id.IsFromUpperEgypt);
    }

    [Theory]
    [InlineData("30101010123458", true)]   // Cairo - Lower Egypt
    [InlineData("30221010123456", true)]   // Giza - Lower Egypt
    [InlineData("30212010123453", true)]   // Dakahlia - Lower Egypt (Delta)
    [InlineData("30202010123459", true)]   // Alexandria - Lower Egypt (Delta)
    [InlineData("30225010123454", false)]  // Asyut - Upper Egypt
    [InlineData("30331010123451", false)]  // Red Sea - not Lower Egypt
    public void IsFromLowerEgypt_ShouldReturnCorrectValue(string nationalId, bool expected)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expected, id.IsFromLowerEgypt);
    }

    [Theory]
    [InlineData("30101010123458", true)]   // Cairo - Greater Cairo
    [InlineData("30221010123456", true)]   // Giza - Greater Cairo
    [InlineData("30212010123453", false)]  // Dakahlia - not Greater Cairo
    public void IsFromGreaterCairo_ShouldReturnCorrectValue(string nationalId, bool expected)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expected, id.IsFromGreaterCairo);
    }

    [Theory]
    [InlineData("30212010123453", true)]   // Dakahlia - Delta
    [InlineData("30202010123459", true)]   // Alexandria - Delta
    [InlineData("30101010123458", false)]  // Cairo - not Delta
    public void IsFromDelta_ShouldReturnCorrectValue(string nationalId, bool expected)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expected, id.IsFromDelta);
    }

    [Theory]
    [InlineData("30331010123451", true)]   // Red Sea - Sinai
    [InlineData("30334010123458", true)]   // North Sinai - Sinai
    [InlineData("30101010123458", false)]  // Cairo - not Sinai
    public void IsFromSinai_ShouldReturnCorrectValue(string nationalId, bool expected)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expected, id.IsFromSinai);
    }

    [Theory]
    [InlineData("30888010123450", true)]   // Foreign - born abroad
    [InlineData("30101010123458", false)]  // Cairo - not born abroad
    public void IsBornAbroad_ShouldReturnCorrectValue(string nationalId, bool expected)
    {
        // Arrange & Act
        var id = new EgyptianNationalId(nationalId);

        // Assert
        Assert.Equal(expected, id.IsBornAbroad);
    }

    [Theory]
    [InlineData("30202010123459", true)]   // Alexandria - coastal (Delta)
    [InlineData("30303010123452", true)]   // Port Said - coastal (Canal)
    [InlineData("30331010123451", true)]   // Red Sea - coastal
    [InlineData("30333010123457", true)]   // Matrouh - coastal
    [InlineData("30101010123458", false)]  // Cairo - not coastal
    [InlineData("30225010123454", false)]  // Asyut - not coastal
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
