using System.Globalization;

namespace DST.Core.Physics
{
    // Defines shared information relating to important DateTime values and supported ranges.
    public static class DateTimeConstants
    {
        // Gets an instance of the GregorianCalendar.
        public static Calendar Calendar { get; } = new GregorianCalendar(GregorianCalendarTypes.Localized);

        // Gets the most recent epoch date and time, represented as the Gregorian date in universal time.
        // This resembles the Julian date in Terrestrial Time (TT), which may be proven by Epoch.ToOADate() + 2415018.5 = 2451545.0.
        // A DateTimeKind of Utc is necessary for the +0h offset.
        public static DateTime Epoch { get; } = new(2000, 1, 1, 12, 0, 0, 0, Calendar, DateTimeKind.Utc);

        // Gets the minimum allowable UTC DateTime for the underlying time zone.
        // This is 1 day (24 hours) ahead of the MinSupportedDateTime, and it should accommodate
        // converting to local time for any timezone, during any time of the year.
        public static DateTime MinUtcDateTime { get; }
            = DateTime.SpecifyKind(Calendar.MinSupportedDateTime.AddDays(1), DateTimeKind.Utc);

        // Gets the maximum allowable UTC DateTime for the underlying time zone.
        // This is 1 day (24 hours) behind the MaxSupportedDateTime, and it should accommodate
        // converting to local time for any timezone, during any time of the year.
        public static DateTime MaxUtcDateTime { get; }
            = DateTime.SpecifyKind(Calendar.MaxSupportedDateTime.AddDays(-1), DateTimeKind.Utc);

        // Gets the total number of ticks from the epoch to MinUtcDateTime.
        // This value is negative.
        public static long MinEpochTickSpan { get; } = MinUtcDateTime.Ticks - Epoch.Ticks;

        // Gets the total number of ticks from the epoch to MaxUtcDateTime.
        // This value is positive.
        public static long MaxEpochTickSpan { get; } = MaxUtcDateTime.Ticks - Epoch.Ticks;

        // Gets the DateTimeKind value intended for use with standardized local DateTime values.
        public static DateTimeKind StandardKind { get; } = DateTimeKind.Unspecified;
    }
}
