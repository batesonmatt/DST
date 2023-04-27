using DST.Core.Coordinate;
using DST.Core.DateTimeAdder;
using DST.Core.DateTimesBuilder;
using DST.Core.Observer;
using DST.Core.Physics;
using DST.Core.TimeKeeper;
using DST.Core.TimeScalable;

namespace DST.Core.Tests
{
    public class AstronomicalTimeZone
    {
        private TimeZoneInfo _timeZoneInfo;

        public AstronomicalTimeZone(TimeZoneInfo timeZoneInfo)
        {
            _timeZoneInfo = timeZoneInfo;
        }

        public static bool TryFindById(string id, out AstronomicalTimeZone result)
        {
            bool success = true;

            try
            {
                result = new(TimeZoneInfo.FindSystemTimeZoneById(id));
            }
            catch
            {
                result = new(TimeZoneInfo.Utc);
                success = false;
            }

            return success;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // The current UTC datetime generated from the local system timezone info.
            DateTime utcDateTime = DateTime.UtcNow;

            // The client's UTC offset value generated on server-side.
            TimeSpan clientUtcOffset;

            // The client's timezone name id (IANA format) retrieved at client-side.
            string clientTzId_IANA = "America/New_York";

            // The client's timezone converted to Windows format on server-side.
            TimeZoneInfo clientTimeZoneInfo;

            // The client's current local datetime generated on server-side.
            DateTime clientDateTime;

            // The client's current datetime expressed in universal time generated on server-side.
            DateTime clientUtcDateTime;

            try
            {
                /* Base UTC -5 hours, DST UTC -4 hours */
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
                AstronomicalDateTime d1 = new(clientDateTime, info, AstronomicalDateTime.UnspecifiedKind.IsLocal);
                DateTime local = d1.ToLocalTime();
                DateTime standard = d1.ToStandardTime();
                AstronomicalDateTime d2 = AstronomicalDateTime.FromStandardTime(standard, info);

                Console.WriteLine("UTC Now: " + info.Now.ToString());
                Console.WriteLine("UTC Today: " + info.Today.ToString());
                Console.WriteLine("Date: " + d1.Date.ToString());
                Console.WriteLine("UTC: " + d1.ToString());
                Console.WriteLine("Local: " + local.ToString());
                Console.WriteLine("Standard: " + standard.ToString());
                Console.WriteLine("From Standard: " + d2.ToString());

                Console.WriteLine("----------------------------------------");

                // March 12, 2:00 am
                // November 5, 2:00 am
                //local = new(2023, 3, 12, 0, 30, 0, DateTimeKind.Unspecified);
                //d1 = new(local, info);
                //for (int i = 0; i < 4; i++)
                //{
                //    d1 = d1.AddMinutes(30);
                //    standard = d1.ToStandardTime();
                //    Console.Write(standard.ToString() + " ... ");
                    
                //    if (info.ClientTimeZoneInfo.IsInvalidTime(standard))
                //    {
                //        Console.Write("Invalid");
                //    }

                //    Console.WriteLine();
                //}

                //d2 = AstronomicalDateTime.FromStandardTime(standard, info);
                //Console.WriteLine(d2.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}