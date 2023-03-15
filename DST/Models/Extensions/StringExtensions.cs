using DST.Models.DataLayer.Query;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DST.Models.Extensions
{
    public static partial class StringExtensions
    {
        public static string Capitalize(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            char[] array = value.ToCharArray();

            array[0] = char.ToUpper(array[0]);

            return new string(array);
        }

        public static string ToTitleCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }

        public static string ToKebabCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            // Replace all non-alphanumeric characters with a dash
            value = NonAlpha().Replace(value, "-");

            // Replace all subsequent dashes with a single dash
            value = RepeatDash().Replace(value, "-");

            // Remove any leading and trailing dashes
            value = value.Trim('-');

            // Lowercase and return
            return value.ToLower();
        }

        public static bool EqualsExact(this string a, string b)
            => a == b;

        public static bool EqualsIgnoreCase(this string a, string b)
            => a?.ToLower() == b?.ToLower();

        public static int ToInt(this string value)
        {
            return int.TryParse(value, out int result) ? result : default;
        }

        [GeneratedRegex("[^0-9a-zA-Z]")]
        private static partial Regex NonAlpha();

        [GeneratedRegex("[-]{2,}")]
        private static partial Regex RepeatDash();
    }
}
