using Egypt.Net.Core.Exceptions;

namespace Egypt.Net.Core.Tests;

/// <summary>
/// Additional tests for checksum validation feature
/// </summary>
public class EgyptianNationalIdChecksumTests
{
    [Fact]
    public void ValidateChecksum_ShouldReturnTrue_WhenChecksumIsValid()
    {
        // This is a valid National ID with correct checksum
        var validId = "30101011234565";

        var isValid = EgyptianNationalId.ValidateChecksum(validId);

        Assert.True(isValid);
    }

    [Fact]
    public void ValidateChecksum_ShouldReturnFalse_WhenChecksumIsInvalid()
    {
        // Same ID but with wrong checksum (changed last digit)
        var invalidId = "30101011234568";

        var isValid = EgyptianNationalId.ValidateChecksum(invalidId);

        Assert.False(isValid);
    }

    [Fact]
    public void Constructor_WithInvalidChecksum_ShouldThrow()
    {
        // Explicit checksum validation
        Assert.Throws<InvalidChecksumException>(() =>
            new EgyptianNationalId("30101010123459", validateChecksum: true));
    }

    [Fact]
    public void Constructor_WithValidChecksum_ShouldNotThrow()
    {
        // Explicit checksum validation
        var exception = Record.Exception(() =>
            new EgyptianNationalId("30101010123458", validateChecksum: true));

        Assert.Null(exception);
    }

    [Fact]
    public void IsValid_WithValidateChecksumTrue_ShouldReturnFalse_WhenChecksumIsWrong()
    {
        var idWithWrongChecksum = "30101011234568";

        var isValid = EgyptianNationalId.IsValid(idWithWrongChecksum, validateChecksum: true);

        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_WithValidateChecksumFalse_ShouldReturnTrue_EvenWithWrongChecksum()
    {
        var idWithWrongChecksum = "30101011234568";

        var isValid = EgyptianNationalId.IsValid(idWithWrongChecksum, validateChecksum: false);

        Assert.True(isValid);
    }

    [Fact]
    public void TryCreate_WithValidateChecksumTrue_ShouldReturnFalse_WhenChecksumIsWrong()
    {
        var idWithWrongChecksum = "30101011234568";

        var result = EgyptianNationalId.TryCreate(idWithWrongChecksum, out var nationalId, validateChecksum: true);

        Assert.False(result);
        Assert.Null(nationalId);
    }

    [Fact]
    public void TryCreate_WithValidateChecksumFalse_ShouldReturnTrue_EvenWithWrongChecksum()
    {
        var idWithWrongChecksum = "30101011234568";

        var result = EgyptianNationalId.TryCreate(idWithWrongChecksum, out var nationalId, validateChecksum: false);

        Assert.True(result);
        Assert.NotNull(nationalId);
    }

    [Fact]
    public void IsValid_ByDefault_ShouldNotValidateChecksum()
    {
        // Invalid checksum should be accepted by default
        bool isValid = EgyptianNationalId.IsValid("30101010123459");
        Assert.True(isValid);
    }

    [Fact]
    public void IsValid_WithChecksumEnabled_ShouldValidate()
    {
        // Invalid checksum should be rejected when validation enabled
        bool isValid = EgyptianNationalId.IsValid("30101010123459", validateChecksum: true);
        Assert.False(isValid);
    }
}
