using System.Text.RegularExpressions;

namespace FinTracker.Domain.Presentation.Model;

public class InvalidHexColor(string value) : ArgumentException($"Invalid hex color '{value}'", nameof(value));

public readonly partial record struct HexColor
{
    public string Value { get; private init; }

    private HexColor(string value) => Value = value.ToUpperInvariant();

    public static HexColor From(string value)
    {
        if (!HexColorRegex().IsMatch(value)) throw new InvalidHexColor(value);

        return new(value);
    }

    public static HexColor Default => From("#FFF");

    [GeneratedRegex(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")]
    private static partial Regex HexColorRegex();
}
