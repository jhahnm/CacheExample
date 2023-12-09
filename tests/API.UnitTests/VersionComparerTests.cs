using API.Utils;

namespace API.UnitTests;

public class VersionComparerTests
{
    [Theory]
    [InlineData("1.2", "1.1", 1)]
    [InlineData("1.1", "1.2", -1)]
    [InlineData("1.2.3", "1.2.3", 0)]
    [InlineData("2.0", "1.9.9", 1)]
    [InlineData("1.9.9", "2.0", -1)]
    [InlineData("1", "1.0", 0)]
    [InlineData("1.2.3", "1.2.4", -1)]
    [InlineData("1.2.4", "1.2.3", 1)]
    [InlineData("1.2", "1.2.3.4" ,-1)]
    [InlineData("1.2.3.4", "1.2", 1)]
    public void CompareVersions_ValidVersions_ReturnsCorrectResult(string version1, string version2, int expected)
    {
        // Act
        int result = VersionComparer.CompareVersions(version1, version2);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("invalid", "1.1")]
    [InlineData("1.1", "invalid")]
    [InlineData("", "1.1")]
    [InlineData("1.1", null)]
    public void CompareVersions_InvalidVersions_ReturnsZero(string version1, string version2)
    {
        // Act
        int result = VersionComparer.CompareVersions(version1, version2);

        // Assert
        Assert.Equal(0, result);
    }
}