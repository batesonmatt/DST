﻿namespace DST.Core.DateAndTime
{
    // Represents a client's time zone information and defines DateTime rules.
    public class DateTimeInfo : IDateTimeInfo
    {
        // Gets the TimeZoneInfo object for this DateTimeInfo instance.
        public TimeZoneInfo ClientTimeZoneInfo { get; }

        // Gets a value indicating whether the underlying time zone has any daylight saving time rules.
        public bool SupportsDaylightSavingTime => ClientTimeZoneInfo.SupportsDaylightSavingTime;

        // Gets the standardized Coordinated Universal Time (UTC) offset for the underlying time zone.
        public TimeSpan BaseUtcOffset => ClientTimeZoneInfo.BaseUtcOffset;

        // Gets the minimum allowable DateTime in standardized local time for the underlying time zone.
        public DateTime MinStandardDateTime => CalculateMinStandardDateTime();

        // Gets the maximum allowable DateTime in standardized local time for the underlying time zone.
        public DateTime MaxStandardDateTime => CalculateMaxStandardDateTime();

        // Gets the minimum supported value for an IMutableDateTime.
        public IMutableDateTime MinAstronomicalDateTime => DateTimeFactory.CreateMutable(DateTimeConstants.MinUtcDateTime, this);

        // Gets the maximum supported value for an IMutableDateTime.
        public IMutableDateTime MaxAstronomicalDateTime => DateTimeFactory.CreateMutable(DateTimeConstants.MaxUtcDateTime, this);

        // Gets a new IDateTime instance resembling the current UTC date and time.
        public IDateTime Now => DateTimeFactory.CreateDateTime(DateTime.UtcNow, this);

        // Gets a new IDateTime instance resembling the current UTC date with the time value set to midnight (00:00:00).
        public IDateTime Today => DateTimeFactory.CreateDateTime(DateTime.UtcNow.Date, this);

        // Creates a new DateTimeInfo instance given the specified TimeZoneInfo argument.
        public DateTimeInfo(TimeZoneInfo timeZoneInfo)
        {
            ClientTimeZoneInfo = timeZoneInfo ?? throw new ArgumentNullException(nameof(timeZoneInfo));
        }

        // Returns a new IMutableDateTime, converted from a specified DateTime value in standardized
        // local time for the underlying client time zone.
        // If dateTime.Kind already equals DateTimeKind.Utc, then this will not modify the date or time.
        public IMutableDateTime ConvertTimeFromStandard(DateTime dateTime)
        {
            if (dateTime.Kind != DateTimeKind.Utc)
            {
                // Verify that the local date and time will be in range when converted to universal time.
                // If this fails, then the resultant IMutableDateTime will have a value of either
                // MinAstronomicalDateTime or MaxAstronomicalDateTime.
                if (dateTime < MinStandardDateTime) return MinAstronomicalDateTime;
                if (dateTime > MaxStandardDateTime) return MaxAstronomicalDateTime;

                dateTime = DateTime.SpecifyKind(dateTime.Subtract(BaseUtcOffset), DateTimeKind.Utc);
            }

            return DateTimeFactory.CreateMutable(dateTime, this);
        }

        // Calculates the minimum allowable DateTime in standardized local time for the underlying time zone.
        private DateTime CalculateMinStandardDateTime()
        {
            double baseOffsetHours = ClientTimeZoneInfo.BaseUtcOffset.TotalHours;

            return Math.Sign(baseOffsetHours) switch
            {
                >= 0 => DateTime.SpecifyKind(DateTimeConstants.MinUtcDateTime, DateTimeConstants.StandardKind),
                _ => DateTime.SpecifyKind(DateTimeConstants.MinUtcDateTime.AddHours(baseOffsetHours), DateTimeConstants.StandardKind)
            };
        }

        // Calculates the maximum allowable DateTime in standardized local time for the underlying time zone.
        private DateTime CalculateMaxStandardDateTime()
        {
            double baseOffsetHours = ClientTimeZoneInfo.BaseUtcOffset.TotalHours;

            return Math.Sign(baseOffsetHours) switch
            {
                <= 0 => DateTime.SpecifyKind(DateTimeConstants.MaxUtcDateTime, DateTimeConstants.StandardKind),
                _ => DateTime.SpecifyKind(DateTimeConstants.MaxUtcDateTime.AddHours(baseOffsetHours), DateTimeConstants.StandardKind)
            };
        }
    }
}
