using DST.Core.Vector;
using DST.Models.DataLayer.Query;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DST.Models.Extensions
{
    public static partial class SeoExtensions
    {
        public static string Capitalize(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            char[] array = value.ToCharArray();

            array[0] = char.ToUpper(array[0], CultureInfo.CurrentCulture);

            return new string(array);
        }

        public static string ToTitleCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower(CultureInfo.CurrentCulture));
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
            return value.ToLowerInvariant();
        }

        public static bool EqualsExact(this string a, string b)
        {
            return a == b;
        }

        public static bool EqualsIgnoreCase(this string a, string b)
        {
            return a?.ToLowerInvariant() == b?.ToLowerInvariant();
        }

        public static bool EqualsSeo(this string a, string b)
        {
            return a.ToKebabCase().EqualsExact(b.ToKebabCase());
        }

        public static string Active(this string a, string b)
        {
            return a.EqualsSeo(b) ? "active" : string.Empty;
        }

        public static string Active(this int a, int b)
        {
            return a == b ? "active" : string.Empty;
        }

        public static string Visibility(this IVector vector)
        {
            return vector.Coordinate.Components.Inclination > 0.0 ? "bi-eye" : "bi-eye-slash";
        }

        public static string Show(this string value)
        {
            return value.IsFilterOff() ? "show" : string.Empty;
        }

        public static string OtherNames(this int value)
        {
            return value switch
            {
                > 1 => $"{value} other names",
                1 => $"{value} other name",
                _ => string.Empty
            };
        }

        public static bool IsFilterAll(this string value)
        {
            return value.EqualsSeo(Filter.All);
        }

        public static bool IsFilterAny(this string value)
        {
            return value.EqualsSeo(Filter.Any);
        }

        public static bool IsFilterOff(this string value)
        {
            return value.EqualsSeo(Filter.Off);
        }

        public static bool IsFilterOn(this string value)
        {
            return value.EqualsSeo(Filter.On);
        }

        public static int ToInt(this string value)
        {
            return int.TryParse(value, out int result) ? result : default;
        }

        public static double ToDouble(this string value)
        {
            return double.TryParse(value, out double result) ? result : default;
        }

        [GeneratedRegex("[^0-9a-zA-Z\\p{L}]", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
        private static partial Regex NonAlpha();

        [GeneratedRegex("[-]{2,}", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
        private static partial Regex RepeatDash();
    }
}
