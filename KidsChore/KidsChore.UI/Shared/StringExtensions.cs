using System.Globalization;

namespace VibeHome.UI.Shared
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string? str)
        {
            if (string.IsNullOrWhiteSpace(str)) return string.Empty;
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
    }
} 