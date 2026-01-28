using Egypt.Net.Core;

namespace Egypt.Net.Core.Tests;

/// <summary>
/// Tests for string extension methods
/// </summary>
public class StringExtensionsTests
{
    private const string ValidId = "30101010123458";
    private const string InvalidFormatId = "123";
    private const string InvalidChecksumId = "30101011234568";

    [Fact]
    public void IsValidEgyptianNationalId_ShouldReturnTrue_WhenValid()
    {
        // Default behavior - no checksum
        var result = "30101010123458".IsValidEgyptianNationalId();
        Assert.True(result);
    }

    [Fact]
    public void IsValidEgyptianNationalId_ShouldReturnFalse_ForInvalidFormat()
    {
        var result = InvalidFormatId.IsValidEgyptianNationalId();

        Assert.False(result);
    }

    [Fact]
    public void ToEgyptianNationalId_ShouldReturnNationalId_ForValidId()
    {
        var nationalId = ValidId.ToEgyptianNationalId();

        Assert.NotNull(nationalId);
        Assert.Equal(ValidId, nationalId!.Value);
    }

    [Fact]
    public void ToEgyptianNationalId_ShouldReturnNull_ForInvalidId()
    {
        var nationalId = InvalidFormatId.ToEgyptianNationalId();

        Assert.Null(nationalId);
    }

    [Fact]
    public void TryParseAsNationalId_ShouldReturnTrue_ForValidId()
    {
        var result = ValidId.TryParseAsNationalId(out var nationalId);

        Assert.True(result);
        Assert.NotNull(nationalId);
        Assert.Equal(ValidId, nationalId!.Value);
    }

    [Fact]
    public void TryParseAsNationalId_ShouldReturnFalse_ForInvalidId()
    {
        var result = InvalidFormatId.TryParseAsNationalId(out var nationalId);

        Assert.False(result);
        Assert.Null(nationalId);
    }

    [Fact]
    public void HasValidNationalIdFormat_ShouldReturnTrue_ForCorrectFormat()
    {
        var result = ValidId.HasValidNationalIdFormat();

        Assert.True(result);
    }

    [Fact]
    public void HasValidNationalIdFormat_ShouldReturnTrue_EvenWithWrongChecksum()
    {
        var result = InvalidChecksumId.HasValidNationalIdFormat();

        Assert.True(result); // Format is valid even if checksum is wrong
    }

    [Fact]
    public void HasValidNationalIdFormat_ShouldReturnFalse_ForInvalidFormat()
    {
        var result = InvalidFormatId.HasValidNationalIdFormat();

        Assert.False(result);
    }

    [Fact]
    public void HasValidNationalIdChecksum_ShouldReturnTrue_ForValidChecksum()
    {
        // ValidId has correct checksum
        var result = ValidId.HasValidNationalIdChecksum();

        Assert.True(result);
    }

    [Fact]
    public void HasValidNationalIdChecksum_ShouldReturnFalse_ForInvalidChecksum()
    {
        var result = InvalidChecksumId.HasValidNationalIdChecksum();

        Assert.False(result);
    }

    [Fact]
    public void ChainedExtensionMethods_ShouldWork()
    {
        // Test chaining: validate format, then convert
        var id = "30101010123458";

        if (id.HasValidNationalIdFormat())
        {
            var nationalId = id.ToEgyptianNationalId();
            Assert.NotNull(nationalId);
            Assert.Equal(Governorate.Cairo, nationalId!.Governorate);
        }
    }

    [Fact]
    public void ExtensionMethods_ShouldHandleNullAndEmpty()
    {
        string? nullString = null;
        string emptyString = "";

        Assert.False(nullString.IsValidEgyptianNationalId());
        Assert.False(emptyString.IsValidEgyptianNationalId());
        Assert.Null(nullString.ToEgyptianNationalId());
        Assert.Null(emptyString.ToEgyptianNationalId());
    }

    [Fact]
    public void IsValidEgyptianNationalId_ByDefault_ShouldNotValidateChecksum()
    {
        // Invalid checksum should be accepted by default
        var result = "30101010123459".IsValidEgyptianNationalId();
        Assert.True(result);
    }

    [Fact]
    public void IsValidEgyptianNationalId_WithChecksumEnabled_ShouldValidate()
    {
        // Invalid checksum should be rejected when enabled
        var result = "30101010123459".IsValidEgyptianNationalId(validateChecksum: true);
        Assert.False(result);
    }
}
