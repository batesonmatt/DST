using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;

namespace DST.Models.Extensions
{
    public static class CookieExtensions
    {
        public static string GetString(this IRequestCookieCollection cookies, string key)
        {
            return cookies[key] ?? string.Empty;
        }

        public static int? GetInt32(this IRequestCookieCollection cookies, string key)
        {
            string value = cookies.GetString(key);

            return int.TryParse(value, out int i) ? i : null;
        }

        public static T GetObject<T>(this IRequestCookieCollection cookies, string key)
        {
            string value = cookies.GetString(key);

            return value is null ? default : JsonSerializer.Deserialize<T>(value);
        }

        public static void SetString(this IResponseCookies cookies, string key, string value, int days = 30)
        {
            // Delete old value
            cookies.Delete(key);

            if (days == 0)
            {
                // Create session cookie
                cookies.Append(key, value);
            }
            else
            {
                // Create persistent cookie
                CookieOptions options = new()
                {
                    Expires = DateTime.Now.AddDays(days)
                };

                cookies.Append(key, value, options);
            }
        }

        public static void SetInt32(this IResponseCookies cookies, string key, int value, int days = 30)
        {
            cookies.SetString(key, value.ToString(), days);
        }

        public static void SetObject<T>(this IResponseCookies cookies, string key, T value, int days = 30)
        {
            cookies.SetString(key, JsonSerializer.Serialize(value), days);
        }
    }
}
