using Egypt.Net.Core;

namespace Egypt.Net.Core.Tests;

/// <summary>
/// Tests for IEquatable and IComparable implementations
/// </summary>
public class EgyptianNationalIdEqualityAndComparisonTests
{
    [Fact]
    public void Equals_ShouldReturnTrue_WhenBothNationalIdsAreSame()
    {
        var id1 = new EgyptianNationalId("30101011234567", validateChecksum: false);
        var id2 = new EgyptianNationalId("30101011234567", validateChecksum: false);

        Assert.True(id1.Equals(id2));
        Assert.True(id1 == id2);
        Assert.False(id1 != id2);
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenNationalIdsAreDifferent()
    {
        var id1 = new EgyptianNationalId("30101011234567", validateChecksum: false);
        var id2 = new EgyptianNationalId("30101011234568", validateChecksum: false);

        Assert.False(id1.Equals(id2));
        Assert.False(id1 == id2);
        Assert.True(id1 != id2);
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenComparingWithNull()
    {
        var id = new EgyptianNationalId("30101011234567", validateChecksum: false);

        Assert.False(id.Equals(null));
        Assert.False(id == null);
        Assert.True(id != null);
    }

    [Fact]
    public void GetHashCode_ShouldBeSame_ForEqualNationalIds()
    {
        var id1 = new EgyptianNationalId("30101011234567", validateChecksum: false);
        var id2 = new EgyptianNationalId("30101011234567", validateChecksum: false);

        Assert.Equal(id1.GetHashCode(), id2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ShouldBeDifferent_ForDifferentNationalIds()
    {
        var id1 = new EgyptianNationalId("30101011234567", validateChecksum: false);
        var id2 = new EgyptianNationalId("30101021234567", validateChecksum: false);

        Assert.NotEqual(id1.GetHashCode(), id2.GetHashCode());
    }

    [Fact]
    public void CompareTo_ShouldReturnNegative_WhenThisIsOlder()
    {
        var olderPerson = new EgyptianNationalId("29001011234567", validateChecksum: false); // Born 1990
        var youngerPerson = new EgyptianNationalId("30001011234567", validateChecksum: false); // Born 2000

        Assert.True(olderPerson.CompareTo(youngerPerson) < 0);
        Assert.True(olderPerson < youngerPerson);
        Assert.True(olderPerson <= youngerPerson);
    }

    [Fact]
    public void CompareTo_ShouldReturnPositive_WhenThisIsYounger()
    {
        var youngerPerson = new EgyptianNationalId("30001011234567", validateChecksum: false); // Born 2000
        var olderPerson = new EgyptianNationalId("29001011234567", validateChecksum: false); // Born 1990

        Assert.True(youngerPerson.CompareTo(olderPerson) > 0);
        Assert.True(youngerPerson > olderPerson);
        Assert.True(youngerPerson >= olderPerson);
    }

    [Fact]
    public void CompareTo_ShouldReturnZero_WhenBirthDatesAndSerialsAreSame()
    {
        var id1 = new EgyptianNationalId("30101011234567", validateChecksum: false);
        var id2 = new EgyptianNationalId("30101011234567", validateChecksum: false);

        Assert.Equal(0, id1.CompareTo(id2));
    }

    [Fact]
    public void CompareTo_ShouldCompareBySerial_WhenBirthDatesAreEqual()
    {
        var id1 = new EgyptianNationalId("30101011200001", validateChecksum: false); // Serial 0001
        var id2 = new EgyptianNationalId("30101011200002", validateChecksum: false); // Serial 0002

        Assert.True(id1.CompareTo(id2) < 0);
        Assert.True(id1 < id2);
    }

    [Fact]
    public void CanBeUsedInHashSet()
    {
        var id1 = new EgyptianNationalId("30101011234567", validateChecksum: false);
        var id2 = new EgyptianNationalId("30101011234567", validateChecksum: false);
        var id3 = new EgyptianNationalId("30101021234567", validateChecksum: false);

        var hashSet = new HashSet<EgyptianNationalId> { id1, id2, id3 };

        // id1 and id2 are equal, so only 2 items should be in the set
        Assert.Equal(2, hashSet.Count);
        Assert.Contains(id1, hashSet);
        Assert.Contains(id3, hashSet);
    }

    [Fact]
    public void CanBeSorted()
    {
        var id1 = new EgyptianNationalId("30001011234567", validateChecksum: false); // Born 2000
        var id2 = new EgyptianNationalId("29001011234567", validateChecksum: false); // Born 1990
        var id3 = new EgyptianNationalId("31001011234567", validateChecksum: false); // Born 2010

        var list = new List<EgyptianNationalId> { id1, id2, id3 };
        list.Sort();

        // Should be sorted by birth date (oldest first)
        Assert.Equal(id2, list[0]); // 1990
        Assert.Equal(id1, list[1]); // 2000
        Assert.Equal(id3, list[2]); // 2010
    }

    [Fact]
    public void ToString_ShouldReturnNationalIdValue()
    {
        var id = new EgyptianNationalId("30101011234567", validateChecksum: false);

        Assert.Equal("30101011234567", id.ToString());
    }
}
