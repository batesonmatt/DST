using DST.Core.DateAndTime;

namespace DST.Core.Tests
{
    /* Temporary code to test various time zones and DST observations. */
    internal static class ClientTimeZoneInfoTests
    {
        private static void RunTimeZoneInfoTest(string timezoneId)
        {
            _ = timezoneId ?? throw new ArgumentNullException(nameof(timezoneId));

            // The current UTC datetime generated from the local system timezone info.
            DateTime utcDateTime = new(2023, 5, 31, 2, 30, 0, DateTimeKind.Utc);

            // The client's UTC offset value generated on server-side.
            TimeSpan clientUtcOffset;

            // The client's timezone name id (IANA format) retrieved at client-side.
            string clientTzId_IANA = timezoneId;

            // The client's timezone converted to Windows format on server-side.
            TimeZoneInfo clientTimeZoneInfo;

            // The client's current local datetime generated on server-side.
            DateTime clientDateTime;

            // The client's current datetime expressed in universal time generated on server-side.
            DateTime clientUtcDateTime;

            try
            {
                clientTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(clientTzId_IANA);
                Console.WriteLine("Client time zone: " + clientTimeZoneInfo.DisplayName);

                clientUtcOffset = clientTimeZoneInfo.GetUtcOffset(utcDateTime);
                Console.WriteLine("Client UTC offset: " + clientUtcOffset.ToString());

                clientDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, clientTimeZoneInfo);
                Console.WriteLine("Client local datetime: " + clientDateTime.ToString() + " " + clientDateTime.Kind);

                clientUtcDateTime = TimeZoneInfo.ConvertTimeToUtc(clientDateTime, clientTimeZoneInfo);
                Console.WriteLine("Client UTC datetime: " + clientUtcDateTime.ToString() + " " + clientUtcDateTime.Kind);

                Console.WriteLine("----------------------------------------");

                DateTimeInfo info = new(clientTimeZoneInfo);
                IDateTime d1 = DateTimeFactory.CreateDateTime(clientDateTime, info);
                IMutableDateTime mutable = DateTimeFactory.ConvertToMutable(d1);
                DateTime local = mutable.ToLocalTime();
                DateTime standard = mutable.ToStandardTime();
                IMutableDateTime d2 = info.ConvertTimeFromStandard(standard);

                Console.WriteLine("UTC Now: " + info.Now.ToString());
                Console.WriteLine("UTC Today: " + info.Today.ToString());
                Console.WriteLine("Date: " + d1.Date.ToString());
                Console.WriteLine("UTC: " + mutable.ToString());
                Console.WriteLine("Local: " + local.ToString());
                Console.WriteLine("Standard: " + standard.ToString());
                Console.WriteLine("From Standard: " + d2.ToString());

                Console.WriteLine("----------------------------------------");

                // March 12, 2:00 am
                // November 5, 2:00 am
                local = new(2023, 3, 12, 0, 30, 0, DateTimeKind.Unspecified);
                mutable = DateTimeFactory.CreateMutable(local, info);
                for (int i = 0; i < 4; i++)
                {
                    mutable = mutable.AddMinutes(30);
                    standard = mutable.ToStandardTime();
                    Console.Write(standard.ToString() + " ... ");

                    if (info.ClientTimeZoneInfo.IsInvalidTime(standard))
                    {
                        Console.Write("Invalid");
                    }

                    Console.WriteLine();
                }

                // Test incorrectly converting from standard time to UTC.
                d2 = DateTimeFactory.CreateMutable(standard, info);
                Console.WriteLine(d2.ToString());

                // Test the correct way to convert from standard time to UTC.
                d2 = info.ConvertTimeFromStandard(standard);
                Console.WriteLine(d2.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void RunAmericaNewYorkTest()
        {
            // Client time zone: (UTC - 05:00) Eastern Time(US &Canada)
            // Client UTC offset: -04:00:00
            // Client local datetime: 5 / 30 / 2023 10:30:00 PM Unspecified
            // Client UTC datetime: 5 / 31 / 2023 2:30:00 AM Utc
            // ----------------------------------------
            // UTC Now: 6 / 1 / 2023 2:40:53 AM
            // UTC Today: 6 / 1 / 2023 12:00:00 AM
            // Date: 5 / 31 / 2023 12:00:00 AM
            // UTC: 5 / 31 / 2023 2:30:00 AM
            // Local: 5 / 30 / 2023 10:30:00 PM
            // Standard: 5 / 30 / 2023 9:30:00 PM
            // From Standard: 5 / 31 / 2023 2:30:00 AM
            // ----------------------------------------
            // 3 / 12 / 2023 1:00:00 AM...
            // 3 / 12 / 2023 1:30:00 AM...
            // 3 / 12 / 2023 2:00:00 AM... Invalid
            // 3 / 12 / 2023 2:30:00 AM... Invalid
            // 3 / 12 / 2023 5:00:00 AM
            // 3 / 12 / 2023 7:30:00 AM

            RunTimeZoneInfoTest("America/New_York");
        }

        public static void RunAustraliaSydneyTest()
        {
            RunTimeZoneInfoTest("Australia/Sydney");
        }
    }
}
