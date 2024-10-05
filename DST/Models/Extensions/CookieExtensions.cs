using Microsoft.AspNetCore.CookiePolicy;
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

            return string.IsNullOrWhiteSpace(value) ? default : JsonSerializer.Deserialize<T>(value);
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

        // Set the property for Cookies Having Independent Partitioned State (CHIPS).
        public static void SetPartitionedCookie(this AppendCookieContext cookieContext)
        {
            if (cookieContext.Context is null || cookieContext.CookieOptions is null)
            {
                return;
            }

            if (cookieContext.CookieOptions.SameSite == SameSiteMode.None)
            {
                if (cookieContext.Context.Request.IsHttps)
                {
                    // Support for setting CHIPS directly is expected in .NET 9.
                    // Until then, add the Partitioned attribute to the cookie path.
                    cookieContext.CookieOptions.Path = "/; samesite=None; Partitioned";
                }
            }
        }
    }
}
