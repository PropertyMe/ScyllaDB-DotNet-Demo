using System.Text.RegularExpressions;

internal static partial class RegexPatterns
{
    public static string[] SplitCamelCase(string source) => IsCamelCase().Split(source);

    [GeneratedRegex(@"(?<!^)(?=[A-Z])")]
    private static partial Regex IsCamelCase();
}