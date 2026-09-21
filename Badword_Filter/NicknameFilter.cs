using System.Text.Json;
using System.Text.RegularExpressions;

/// <summary>
/// Prompt
/// Ich baue eine App, bei der sich Nutzer mit einem selbst gewählten Nickname registrieren. Schreibe mir in C# eine Funktion IsNicknameAllowed(string nickname), die false zurückgibt, wenn der Nickname eine beleidigende, diskriminierende oder anzügliche Formulierung enthält, sonst true. Nutze dafür eine austauschbare, extern geladene Liste unzulässiger Begriffe (z. B. aus einer JSON-Datei) statt die Begriffe hart im Code zu hinterlegen.
/// </summary>
public sealed class NicknameFilter
{
    private readonly HashSet<string> _blockedTerms;

    public NicknameFilter(string jsonPath)
    {
        var json = File.ReadAllText(jsonPath);

        var terms = JsonSerializer.Deserialize<List<string>>(json)
                    ?? new List<string>();

        _blockedTerms = new HashSet<string>(
            terms
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(Normalize),
            StringComparer.OrdinalIgnoreCase);
    }

    public bool IsNicknameAllowed(string nickname)
    {
        if (string.IsNullOrWhiteSpace(nickname))
            return false;

        var normalized = Normalize(nickname);

        return !_blockedTerms.Any(term =>
            Regex.IsMatch(
                normalized,
                $@"(?<!\w){Regex.Escape(term)}(?!\w)",
                RegexOptions.CultureInvariant));
    }

    private static string Normalize(string value)
    {
        value = value.ToLowerInvariant();

        // Sonderzeichen/Leerzeichen vereinheitlichen
        value = Regex.Replace(value, @"[^\p{L}\p{N}]+", " ");

        return value.Trim();
    }
}