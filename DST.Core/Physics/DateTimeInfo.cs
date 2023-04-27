using System.Globalization;

namespace DST.Core.Physics
{
    // Represents a client's time zone information and defines DateTime rules.
    // Uses the Gregorian calendar and the J2000.0 epoch event.
    public class DateTimeInfo : IDateTimeInfo
    {
        // Gets an instance of the GregorianCalendar.
        public static Calendar Calendar { get; } = new GregorianCalendar(GregorianCalendarTypes.Localized);

        // Gets the most recent epoch date and time, represented as the Gregorian date in universal time.
        // This resembles the Julian date in Terrestrial Time (TT), which may be proved by Epoch.ToOADate() + 2415018.5 = 2451545.0.
        // A DateTimeKind of Utc is necessary for the +0h offset.
        public static DateTime Epoch { get; } = new(2000, 1, 1, 12, 0, 0, 0, Calendar, DateTimeKind.Utc);

        // Gets the minimum allowable UTC DateTime for the underlying time zone.
        public static DateTime MinUtcDateTime
            => DateTime.SpecifyKind(Calendar.MinSupportedDateTime.AddDays(1), DateTimeKind.Utc);

        // Gets the maximum allowable UTC DateTime for the underlying time zone.
        public static DateTime MaxUtcDateTime
            => DateTime.SpecifyKind(Calendar.MaxSupportedDateTime.AddDays(-1), DateTimeKind.Utc);

        // Gets the total number of ticks from the epoch to MinUtcDateTime.
        // This value is negative.
        public static long MinEpochTickSpan => MinUtcDateTime.Ticks - Epoch.Ticks;

        // Gets the total number of ticks from the epoch to MaxUtcDateTime.
        // This value is positive.
        public static long MaxEpochTickSpan => MaxUtcDateTime.Ticks - Epoch.Ticks;

        public static DateTimeInfo Default { get; } = new(TimeZoneInfo.Utc);

        public TimeZoneInfo ClientTimeZoneInfo { get; }

        // Gets a value indicating whether the underlying time zone has any daylight saving time rules.
        public bool SupportsDaylightSavingTime => ClientTimeZoneInfo.SupportsDaylightSavingTime;

        // Gets the Coordinated Universal Time (UTC) offset for the underlying time zone.
        public TimeSpan BaseUtcOffset => ClientTimeZoneInfo.BaseUtcOffset;

        // Gets the minimum supported value for an AstronomicalDateTime.
        public AstronomicalDateTime MinAstronomicalDateTime => new(MinUtcDateTime, this);

        // Gets the maximum supported value for an AstronomicalDateTime.
        public AstronomicalDateTime MaxAstronomicalDateTime => new(MaxUtcDateTime, this);

        // Gets a new AstronomicalDateTime instance resembling the current UTC date and time.
        public AstronomicalDateTime Now => new(DateTime.UtcNow, this);

        // Gets a new AstronomicalDateTime instance resembling the current UTC date with the time value set to midnight (00:00:00).
        public AstronomicalDateTime Today => new(DateTime.UtcNow.Date, this);

        public DateTimeInfo(TimeZoneInfo timeZoneInfo)
        {
            ClientTimeZoneInfo = timeZoneInfo ?? throw new ArgumentNullException(nameof(timeZoneInfo));
        }
    }
}
