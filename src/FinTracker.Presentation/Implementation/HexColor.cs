using System.Text.RegularExpressions;
using FinTracker.IDomain;

internal class InvalidHexColor(string value) : Exception($"Invalid hex color '{value}'");

internal readonly partial record struct HexColor
{
    public string Value { get; private init; }

    private HexColor(string value) => Value = value.ToUpperInvariant();

    public static Result<HexColor> TryParse(string value)
    {
        if (!HexColorRegex().IsMatch(value)) return new(new InvalidHexColor(value));

        return new(new HexColor(value));
    }

    public override string ToString() => Value;

    [GeneratedRegex(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")]
    private static partial Regex HexColorRegex();
}
