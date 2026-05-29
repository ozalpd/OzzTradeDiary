using System.Text;
using System.Text.RegularExpressions;

namespace TD.Tools;

public static partial class Text
{
    public static bool IsNumeric(this string s)
    {
        return Regex.IsMatch(s, @"^\d+$");
    }

    public static string PascalCaseToTitleCase(this string s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;
        return Regex.Replace(s, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToUpperInvariant(m.Value[1]));
    }

    public static string PascalCaseToSentenceCase(this string s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;
        return Regex.Replace(s, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLowerInvariant(m.Value[1]));
    }

    public static string GetUpperNamespace(this string s)
    {
        string[] parts = s.Split('.');
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < parts.Length - 1; i++)
        {
            if (sb.Length > 0)
            {
                sb.Append('.');
            }
            sb.Append(parts[i]);
        }

        return sb.ToString();
    }

    public static string Pluralize(this string s)
    {
        if (s.EndsWith("y") &&
            !s.EndsWith("ay") && !s.EndsWith("ey") &&
            !s.EndsWith("uy") && !s.EndsWith("iy") && !s.EndsWith("oy"))
        {
            return s.Substring(0, s.Length - 1) + "ies";
        }
        else if (s.EndsWith("s") || s.EndsWith("h") || s.EndsWith("x"))
        {
            return s + "es";
        }
        else
        {
            return s + "s";
        }
    }

    /// <summary>
    /// Remove HTML tags from string using char array.
    /// </summary>
    public static string RemoveHtmlTags(this string source)
    {
        char[] array = new char[source.Length];
        int arrayIndex = 0;
        bool inside = false;

        for (int i = 0; i < source.Length; i++)
        {
            char let = source[i];
            if (let == '<')
            {
                inside = true;
                continue;
            }
            if (let == '>')
            {
                inside = false;
                continue;
            }
            if (!inside)
            {
                array[arrayIndex] = let;
                arrayIndex++;
            }
        }
        return new string(array, 0, arrayIndex);
    }


    private static List<char[]> _replaceTrEnList = new List<char[]>()
    {
        new char[] { 'ş', 's' },
        new char[] { 'ğ', 'g' },
        new char[] { 'ü', 'u' },
        new char[] { 'ö', 'o' },
        new char[] { 'ç', 'c' },
        new char[] { 'ı', 'i' },
        new char[] { 'İ', 'I' },
        new char[] { 'Ş', 'S' },
        new char[] { 'Ç', 'C' },
        new char[] { 'Ö', 'O' },
        new char[] { 'Ğ', 'G' },
        new char[] { 'Ü', 'U' }
    };

    public static string RemoveTurkishChars(this string source)
    {
        StringBuilder sb = new StringBuilder(source);

        foreach (char[] c in _replaceTrEnList)
        {
            sb.Replace(c[0], c[1]);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Returns new string with the first letter changed to lowercase
    /// </summary>
    public static string ToCamelCase(this string s)
    {
        if (String.IsNullOrEmpty(s))
            return s;

        if (s.Length == 1)
            return s[0].ToString().ToLowerInvariant();

        return s[0].ToString().ToLowerInvariant() + s.ToPascalCase().Substring(1);
    }

    /// <summary>
    /// Returns new string which spaces are removed and each word begins with a capital letter
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ToPascalCase(this string s)
    {

        if (string.IsNullOrEmpty(s))
            return string.Empty;

        if (s.Length == 1)
            return s[0].ToString().ToUpperInvariant();

        string[] words = s.Split(' ');
        if (words.Length == 1)
            return s.ToTitleCase()[0].ToString().ToUpperInvariant() + s.Substring(1);

        StringBuilder sb = new StringBuilder();
        foreach (var word in words)
        {
            if (!string.IsNullOrEmpty(word))
            {
                sb.Append(word[0].ToString().ToUpperInvariant());
                sb.Append(word.Substring(1).ToLowerInvariant());
            }
        }
        return sb.ToString();
    }

    public static string ToSentenceCase(this string s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;

        var lowerCase = s.ToLower();
        var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
        return r.Replace(lowerCase, l => l.Value.ToUpper());
    }

    public static string ToTitleCase(this string s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;

        var textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(s);
    }


    public static string StringArrayToString(string[] array)
    {
        StringBuilder sb = new StringBuilder();
        foreach (string s in array)
        {
            sb.Append(s);
            sb.Append(' ');
        }

        return sb.ToString().Trim();
    }

    public static string Singularize(this string s)
    {
        if (string.IsNullOrEmpty(s) || s.Length < 2) return s;

        if (s.EndsWith("ies"))
        {
            return s.Substring(0, s.Length - 3) + "y";
        }
        else
        {
            return s.Substring(0, s.Length - 1);
        }
    }
}

