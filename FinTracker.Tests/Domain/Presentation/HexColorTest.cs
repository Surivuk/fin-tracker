
using FinTracker.Domain.Presentation.Model;

namespace FinTracker.Tests.Domain.Presentation;

public class HexColorTest
{

    [Theory]
    [InlineData("#FF5733")]
    [InlineData("#f57")]
    [InlineData("#000000")]
    [InlineData("#FFF")]
    public void ShouldBeValid(string hexString)
    {
        var color = HexColor.From(hexString);

        Assert.Equal(hexString.ToUpperInvariant(), color.Value);
    }

    [Theory]
    [InlineData("FF5733")]   // missing #
    [InlineData("#XYZ123")]  // invalid characters
    [InlineData("#12345")]   // wrong length
    [InlineData("")]         // empty
    [InlineData("#GGGGGG")]  // out of hex range
    public void ShouldBeInvalid(string hexString)
    {
        Assert.Throws<InvalidHexColor>(() => HexColor.From(hexString));
    }
}