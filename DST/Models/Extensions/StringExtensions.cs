using System.Globalization;
using System.Text;

namespace DST.Models.Extensions
{
    public static class StringExtensions
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

        public static string Slug(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            StringBuilder builder = new();

            foreach (char c in value)
            {
                if (char.IsPunctuation(c) == false || c == '-')
                {
                    builder.Append(c);
                }
            }

            return builder.ToString().Replace(' ', '-').ToLower();
        }

        public static bool EqualsIgnoreCase(this string a, string b)
            => a?.ToLower() == b?.ToLower();

        public static int ToInt(this string value)
        {
            int.TryParse(value, out int result);

            return result;
        }
    }
}
