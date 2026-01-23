namespace Egypt.Net.Core.Tests;

public class EgyptianNationalIdTests
{
    [Fact]
    public void Constructor_WithValidNationalId_ShouldCreateObject()
    {
        // Arrange
        var nationalIdValue = "30101011234567";

        // Act
        var nationalId = new EgyptianNationalId(nationalIdValue);

        // Assert
        Assert.NotNull(nationalId);
    }

    [Fact]
    public void BirthDate_ShouldBeParsedCorrectly_FromNationalId()
    {
        // Arrange
        var nationalIdValue = "30101011234567";
        var expectedBirthDate = new DateTime(2001, 01, 01);

        // Act
        var nationalId = new EgyptianNationalId(nationalIdValue);

        // Assert
        Assert.Equal(expectedBirthDate, nationalId.BirthDate);
    }

    [Fact]
    public void Gender_ShouldBeMale_WhenSerialLastDigitIsEven()
    {
        // Arrange
        var nationalIdValue = "30101011234568";

        // Act
        var nationalId = new EgyptianNationalId(nationalIdValue);

        // Assert
        Assert.Equal(Gender.Male, nationalId.Gender);
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenNationalIdIsInvalid()
    {
        // Arrange
        var invalidNationalId = "123";

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            new EgyptianNationalId(invalidNationalId);
        });
    }

    [Fact]
    public void Governorate_ShouldBeCairo_WhenCodeIs01()
    {
        var id = new EgyptianNationalId("30101010123456");

        Assert.Equal(Governorate.Cairo, id.Governorate);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenGovernorateCodeIsInvalid()
    {
        var invalidId = "30101019999999";

        Assert.Throws<InvalidOperationException>(() =>
        {
            new EgyptianNationalId(invalidId);
        });
    }

    [Fact]
    public void Age_ShouldBeCorrect_ForKnownBirthDate()
    {
        var id = new EgyptianNationalId("30001010123456"); // 1/1/2000

        Assert.True(id.Age >= 24);
    }

    [Fact]
    public void IsAdult_ShouldBeTrue_WhenAgeIs18OrMore()
    {
        var id = new EgyptianNationalId("30001010123456");

        Assert.True(id.IsAdult);
    }

}