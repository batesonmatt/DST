namespace DST.Core.Physics
{
    public class DateTimeInfoFactory
    {
        // Returns a new IDateTimeInfo object given the specified TimeZoneInfo.
        public static IDateTimeInfo Create(TimeZoneInfo timeZoneInfo)
        {
            _ = timeZoneInfo ?? throw new ArgumentNullException(nameof(timeZoneInfo));

            return new DateTimeInfo(timeZoneInfo);
        }

        // Returns a new IDateTimeInfo object by finding a new TimeZoneInfo object by Id.
        // If the id is not a valid Windows or IANA time zone name, then this returns DateTimeInfo.Default.
        public static IDateTimeInfo CreateFromTimeZoneId(string id)
        {
            _ = id ?? throw new ArgumentNullException(nameof(id));

            IDateTimeInfo result;

            try
            {
                result = Create(TimeZoneInfo.FindSystemTimeZoneById(id));
            }
            catch
            {
                result = DateTimeInfo.Default;
            }

            return result;
        }
    }
}
