using Egypt.Net.Core;

namespace Egypt.Net.Core.Tests;

/// <summary>
/// Tests for National ID formatting methods
/// </summary>
public class EgyptianNationalIdFormatterTests
{
    private readonly EgyptianNationalId _testId;

    public EgyptianNationalIdFormatterTests()
    {
        _testId = new EgyptianNationalId("30101011234567", validateChecksum: false);
    }

    [Fact]
    public void FormatWithDashes_ShouldReturnCorrectFormat()
    {
        var formatted = _testId.FormatWithDashes();

        Assert.Equal("3-010101-01-23456", formatted);
    }

    [Fact]
    public void FormatWithSpaces_ShouldReturnCorrectFormat()
    {
        var formatted = _testId.FormatWithSpaces();

        Assert.Equal("3 010101 01 23456", formatted);
    }

    [Fact]
    public void FormatWithBrackets_ShouldReturnCorrectFormat()
    {
        var formatted = _testId.FormatWithBrackets();

        Assert.Equal("[3][010101][01][23456]", formatted);
    }

    [Fact]
    public void FormatMasked_ShouldMaskMiddleDigits()
    {
        var formatted = _testId.FormatMasked();

        Assert.Equal("301********67", formatted);
        Assert.Contains("301", formatted);
        Assert.Contains("67", formatted);
        Assert.Contains("********", formatted);
    }

    [Fact]
    public void FormatDetailed_ShouldIncludeAllInformation()
    {
        var formatted = _testId.FormatDetailed();

        Assert.Contains("Century: 3", formatted);
        Assert.Contains("2000s", formatted);
        Assert.Contains("Birth Date:", formatted);
        Assert.Contains("01/01/2001", formatted);
        Assert.Contains("Governorate: 01", formatted);
        Assert.Contains("Cairo", formatted);
        Assert.Contains("Serial: 2345", formatted);
        Assert.Contains("Gender: Male", formatted);
    }

    [Fact]
    public void FormatDetailed_ShouldHandle1900sCentury()
    {
        var id1900s = new EgyptianNationalId("29501011234567", validateChecksum: false);
        var formatted = id1900s.FormatDetailed();

        Assert.Contains("Century: 2", formatted);
        Assert.Contains("1900s", formatted);
    }

    [Fact]
    public void AllFormattingMethods_ShouldWorkWithDifferentIds()
    {
        var id1 = new EgyptianNationalId("28012152200001", validateChecksum: false);
        var id2 = new EgyptianNationalId("31506283500099", validateChecksum: false);

        // Just ensure they don't throw exceptions
        Assert.NotNull(id1.FormatWithDashes());
        Assert.NotNull(id1.FormatWithSpaces());
        Assert.NotNull(id1.FormatWithBrackets());
        Assert.NotNull(id1.FormatMasked());
        Assert.NotNull(id1.FormatDetailed());

        Assert.NotNull(id2.FormatWithDashes());
        Assert.NotNull(id2.FormatWithSpaces());
        Assert.NotNull(id2.FormatWithBrackets());
        Assert.NotNull(id2.FormatMasked());
        Assert.NotNull(id2.FormatDetailed());
    }
}
