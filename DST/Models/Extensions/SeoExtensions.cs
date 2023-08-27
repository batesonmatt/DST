using DST.Core.Physics;
using DST.Models.DataLayer.Query;
using System;
using System.Globalization;
using System.Linq;
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
            if (a.EqualsSeo(b))
            {
                return "active";
            }
            else
            {
                return string.Empty;
            }
        }

        public static int ToInt(this string value)
        {
            return int.TryParse(value, out int result) ? result : default;
        }

        public static double ToDouble(this string value)
        {
            return double.TryParse(value, out double result) ? result : default;
        }

        [Obsolete("Not adding geolocation information to routing service at this time.")]
        public static bool IsLatitudeSeo(this string value)
        {
            return LatitudeSeo().IsMatch(value);
        }

        [Obsolete("Not adding geolocation information to routing service at this time.")]
        public static bool IsLongitudeSeo(this string value)
        {
            return LongitudeSeo().IsMatch(value);
        }

        [Obsolete("Not adding geolocation information to routing service at this time.")]
        public static double ToLatitude(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return default;
            }

            if (value.IsLatitudeSeo() == false)
            {
                return default;
            }

            int sign = value.ToUpperInvariant().First().ToString() == GeoIndicator.North ? 1 : -1;

            string number = value.Substring(1).Replace('-', '.');

            return sign * number.ToDouble();
        }

        [Obsolete("Not adding geolocation information to routing service at this time.")]
        public static double ToLongitude(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return default;
            }

            if (value.IsLongitudeSeo() == false)
            {
                return default;
            }

            int sign = value.ToUpperInvariant().First().ToString() == GeoIndicator.East ? 1 : -1;

            string number = value.Substring(1).Replace('-', '.');

            return sign * number.ToDouble();
        }

        [Obsolete("Not adding geolocation information to routing service at this time.")]
        public static string ToLatitudeSeo(this double value)
        {
            if (value.IsFiniteRealNumber() == false)
            {
                return GeoIndicator.DefaultLatitude;
            }

            string result = (value < 0 ? GeoIndicator.South : GeoIndicator.North)
                + Math.Abs(value).ToString(CultureInfo.InvariantCulture).ToKebabCase();

            return result;
        }

        [Obsolete("Not adding geolocation information to routing service at this time.")]
        public static string ToLongitudeSeo(this double value)
        {
            if (value.IsFiniteRealNumber() == false)
            {
                return GeoIndicator.DefaultLongitude;
            }

            string result = (value < 0 ? GeoIndicator.West : GeoIndicator.East)
                + Math.Abs(value).ToString(CultureInfo.InvariantCulture).ToKebabCase();

            return result;
        }

        [GeneratedRegex("[^0-9a-zA-Z\\p{L}]", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
        private static partial Regex NonAlpha();

        [GeneratedRegex("[-]{2,}", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
        private static partial Regex RepeatDash();

        [Obsolete("Not adding geolocation information to routing service at this time.")]
        [GeneratedRegex("^[N|S][0-9]+([-][0-9]+)?$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase)]
        private static partial Regex LatitudeSeo();

        [Obsolete("Not adding geolocation information to routing service at this time.")]
        [GeneratedRegex("^[E|W][0-9]+([-][0-9]+)?$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase)]
        private static partial Regex LongitudeSeo();
    }
}
