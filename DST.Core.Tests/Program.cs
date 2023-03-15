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
            // The current UTC datetime generated on server-side
            DateTime utcDateTime = DateTime.UtcNow;

            // The client's UTC offset value generated on server-side.
            TimeSpan clientUtcOffset;

            // The client's timezone name id (IANA format) retrieved at client-side.
            string clientTzId_IANA = "America/New_York";

            // The client's timezone converted to Windows format on server-side.
            TimeZoneInfo clientTimeZoneInfo;

            // The client's current datetime generated on server-side.
            DateTime clientDateTime;

            try
            {
                clientTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(clientTzId_IANA);
                Console.WriteLine("Client time zone: " + clientTimeZoneInfo.DisplayName);

                clientUtcOffset = clientTimeZoneInfo.GetUtcOffset(utcDateTime);
                Console.WriteLine("Client UTC offset: " + clientUtcOffset.ToString());

                clientDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, clientTimeZoneInfo);
                Console.WriteLine("Client local datetime: " + clientDateTime.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}