﻿using System;

namespace DST.Core.Physics
{
    // Represents a DateTime value in Universal Time, for a given client time zone, on the Gregorian calendar.
    public partial class AstronomicalDateTime : IDateTime
    {
        // Gets the underlying date and time value, represented in universal time.
        public DateTime Value { get; }

        // Gets the underlying DateTimeInfo object.
        public DateTimeInfo Info { get; }

        // Gets the number of ticks that represent the date and time of this AstronomicalDateTime instance.
        public long Ticks => Value.Ticks;

        // Gets the date component of the underlying UTC DateTime value.
        public AstronomicalDateTime Date => new(Value.Date, Info);

        // Gets the time of day of the underlying DateTime value, represented in universal time.
        public TimeSpan Time => Value.TimeOfDay;

        // Gets the Coordinated Universal Time (UTC) offset for the client time zone.
        public TimeSpan UtcOffset => Info.ClientTimeZoneInfo.GetUtcOffset(Value);

        // Gets the total number of ticks from the underlying DateTime value (Value) to Info.MinUtcDateTime.
        // This value is negative.
        public long MinTickSpan => DateTimeInfo.MinUtcDateTime.Ticks - Ticks;

        // Gets the total number of ticks from the underlying DateTime value (Value) to Info.MaxUtcDateTime.
        // This value is positive.
        public long MaxTickSpan => DateTimeInfo.MaxUtcDateTime.Ticks - Ticks;

        // Creates a new AstronomicalDateTime with the specified DateTime and DateTimeInfo values.
        // If dateTime.Kind is DateTimeKind.Local or DateTimeKind.Unspecified, then this converts
        // dateTime to universal time using dateTimeInfo.ClientTimeZoneInfo.
        public AstronomicalDateTime(DateTime dateTime, DateTimeInfo dateTimeInfo)
        {
            Info = dateTimeInfo ?? throw new ArgumentNullException(nameof(dateTimeInfo));

            Value = default;
            Info = dateTimeInfo;
            Value = GetAdjustedDateTime(dateTime);
        }

        // Returns a value that indicates whether two specified AstronomicalDateTime values are not equal.
        public static bool operator !=(AstronomicalDateTime left, AstronomicalDateTime right)
        {
            return left.Ticks != right.Ticks;
        }

        // Returns a value that indicates whether two specified AstronomicalDateTime values are equal.
        public static bool operator ==(AstronomicalDateTime left, AstronomicalDateTime right)
        {
            return left.Ticks == right.Ticks;
        }

        // Returns a value that indicates whether a specified AstronomicalDateTime value is less than
        // another specified AstronomicalDateTime value.
        public static bool operator <(AstronomicalDateTime left, AstronomicalDateTime right)
        {
            return left.Ticks < right.Ticks;
        }

        // Returns a value that indicates whether a specified AstronomicalDateTime value is greater than
        // another specified AstronomicalDateTime value.
        public static bool operator >(AstronomicalDateTime left, AstronomicalDateTime right)
        {
            return left.Ticks > right.Ticks;
        }

        // Returns a value that indicates whether a specified AstronomicalDateTime value is less than
        // or equal to another specified AstronomicalDateTime value.
        public static bool operator <=(AstronomicalDateTime left, AstronomicalDateTime right)
        {
            return left.Ticks <= right.Ticks;
        }

        // Returns a value that indicates whether a specified AstronomicalDateTime value is greater than
        // or equal to another specified AstronomicalDateTime value.
        public static bool operator >=(AstronomicalDateTime left, AstronomicalDateTime right)
        {
            return left.Ticks >= right.Ticks;
        }

        // Returns a new AstronomicalDateTime, converted from a DateTime value in standard time for
        // a given client time zone as defined by DateTimeIno.ClientTimeZoneInfo.
        // If dateTime.Kind already equals DateTimeKind.Utc, then this will not modify the date or time.
        public static AstronomicalDateTime FromStandardTime(DateTime dateTime, DateTimeInfo dateTimeInfo)
        {
            _ = dateTimeInfo ?? throw new ArgumentNullException(nameof(dateTimeInfo));

            if (dateTime.Kind != DateTimeKind.Utc)
            {
                /* Need to evaluate the Min and Max UTC DateTimes in standard time. */
                /* We just need to check bounds a bit better, using the calendar min/max supported values. */
                // Verify that the local date and time will be in range when converted to universal time.
                // If this fails, then the resultant AstronomicalDateTime will have a value of either
                // DateTimeInfo.MinAstronomicalDateTime or DateTimeInfo.MaxAstronomicalDateTime.
                if (dateTime < DateTimeInfo.MinUtcDateTime) return dateTimeInfo.MinAstronomicalDateTime;
                if (dateTime > DateTimeInfo.MaxUtcDateTime) return dateTimeInfo.MaxAstronomicalDateTime;
                dateTime = DateTime.SpecifyKind(dateTime.Subtract(dateTimeInfo.BaseUtcOffset), DateTimeKind.Utc);
            }

            return new(dateTime, dateTimeInfo);
        }

        /* Might need to revise the bounds checking */
        // Restricts an instance of DateTime onto the Gregorian calendar in universal time, for the underlying client time zone.
        // Converts argument 'dateTime' to UTC if dateTime.Kind equals DateTimeKind.Local or DateTimeKind.Unspecified.
        private DateTime GetAdjustedDateTime(DateTime dateTime)
        {
            /* Apparently, DateTimeKind.Local is for the local timezone. 
             * For timezones that are not UTC or Local, always use DateTimeKind.Unspecified.
             * So, if dateTime.Kind == DateTimeKind.Local, then copy it but with DateTimeKind.Unspecified.
             */
            if (dateTime.Kind == DateTimeKind.Unspecified && kind == UnspecifiedKind.IsLocal ||
                dateTime.Kind == DateTimeKind.Local)
            {
                // The date and time value is being treated as local time, so convert to universal time.
                dateTime = TimeZoneInfo.ConvertTimeToUtc(dateTime, Info.ClientTimeZoneInfo);
            }

            // Get the min and max allowable DateTime values in universal time.
            DateTime max = DateTimeInfo.MaxUtcDateTime;
            DateTime min = DateTimeInfo.MinUtcDateTime;

            // Set the year.
            int year = dateTime.Year;
            if (year < 1) year = 1;
            if (year > max.Year) year = max.Year;

            // Set the month.
            int month = dateTime.Month;
            int monthsInYear = DateTimeInfo.Calendar.GetMonthsInYear(year);
            if (month < 1) month = 1;
            if (month > monthsInYear) month = monthsInYear;

            // Set the day.
            int day = dateTime.Day;
            int daysInMonth = DateTimeInfo.Calendar.GetDaysInMonth(year, month);
            if (day < 1) day = 1;
            if (day > daysInMonth) day = daysInMonth;

            // The adjusted date is finished.
            DateTime date = new(year, month, day);

            // Get the time.
            TimeSpan time = dateTime.TimeOfDay;

            // Verify that the date's time of day is in range.
            if (date == min.Date)
            {
                if (time < min.TimeOfDay)
                {
                    time = min.TimeOfDay;
                }
            }
            else if (date == max.Date)
            {
                if (time > max.TimeOfDay)
                {
                    time = max.TimeOfDay;
                }
            }

            // The final UTC date and time value on the correct calendar.
            DateTime result = new(
                year,
                month,
                day,
                time.Hours,
                time.Minutes,
                time.Seconds,
                time.Milliseconds,
                DateTimeInfo.Calendar,
                DateTimeKind.Utc);

            return result;
        }

        // Returns a value that determines whether this instance is equal to either the minimum or maximum supported
        // AstronomicalDateTime values.
        public bool IsMinOrMaxValue()
        {
            return this == Info.MinAstronomicalDateTime || this == Info.MaxAstronomicalDateTime;
        }

        // Returns the number of ticks between this current AstronomicalDateTime instance and the most recent epoch event.
        // The resulting value may be positive or negative.
        public long GetTicksFromEpoch()
        {
            long value = Value.Ticks;
            long epoch = DateTimeInfo.Epoch.Ticks;
            long bounds = value >= epoch ? DateTimeInfo.MaxUtcDateTime.Ticks : DateTimeInfo.MinUtcDateTime.Ticks;

            // Validate that the difference of the epoch from the adjusted date/time value may be computable.
            // Note that if a given DateTime value, dateTime, lies within MinUtcDateTime and MaxUtcDateTime, then both
            // MinUtcDateTime - dateTime and MaxUtcDateTime - dateTime will be computable and will not throw an exception.
            // 
            // If the value is between the epoch and boundary datetime.
            if (Math.Abs(bounds - epoch) >= Math.Abs(bounds - value))
            {
                return value - epoch;
            }
            else
            {
                // The value is out of supported range.
                return value >= epoch ? DateTimeInfo.MaxEpochTickSpan : DateTimeInfo.MinEpochTickSpan;
            }
        }

        // Returns a new AstronomicalDateTime that adds the specified number of seconds to the value of this instance.
        public AstronomicalDateTime AddSeconds(double value)
        {
            if (value == 0.0) return this;
            if (value.IsFiniteRealNumber() == false) return this;
            if (value <= MinTickSpan / Constants.TicksPerSecond) return Info.MinAstronomicalDateTime;
            if (value >= MaxTickSpan / Constants.TicksPerSecond) return Info.MaxAstronomicalDateTime;

            // Preserve DateTime.Kind, which should be DateTimeKind.Utc.
            return new AstronomicalDateTime(Value.AddSeconds(value), Info, UnspecifiedKind.IsUtc);
        }

        // Returns a new AstronomicalDateTime that adds the specified number of minutes to the value of this instance.
        public AstronomicalDateTime AddMinutes(double value)
        {
            if (value == 0.0) return this;
            if (value.IsFiniteRealNumber() == false) return this;
            if (value <= MinTickSpan / Constants.TicksPerMinute) return Info.MinAstronomicalDateTime;
            if (value >= MaxTickSpan / Constants.TicksPerMinute) return Info.MaxAstronomicalDateTime;

            // Preserve DateTime.Kind, which should be DateTimeKind.Utc.
            return new AstronomicalDateTime(Value.AddMinutes(value), Info, UnspecifiedKind.IsUtc);
        }

        // Returns a new AstronomicalDateTime that adds the specified number of hours to the value of this instance.
        public AstronomicalDateTime AddHours(double value)
        {
            if (value == 0.0) return this;
            if (value.IsFiniteRealNumber() == false) return this;
            if (value <= MinTickSpan / Constants.TicksPerHour) return Info.MinAstronomicalDateTime;
            if (value >= MaxTickSpan / Constants.TicksPerHour) return Info.MaxAstronomicalDateTime;

            // Preserve DateTime.Kind, which should be DateTimeKind.Utc.
            return new AstronomicalDateTime(Value.AddHours(value), Info, UnspecifiedKind.IsUtc);
        }

        // Returns a new AstronomicalDateTime that adds the specified number of days to the value of this instance.
        public AstronomicalDateTime AddDays(double value)
        {
            if (value == 0.0) return this;
            if (value.IsFiniteRealNumber() == false) return this;
            if (value <= MinTickSpan / Constants.TicksPerDay) return Info.MinAstronomicalDateTime;
            if (value >= MaxTickSpan / Constants.TicksPerDay) return Info.MaxAstronomicalDateTime;

            // Preserve DateTime.Kind, which should be DateTimeKind.Utc.
            return new AstronomicalDateTime(Value.AddDays(value), Info, UnspecifiedKind.IsUtc);
        }

        // Returns a new AstronomicalDateTime that adds the specified number of weeks to the value of this instance.
        public AstronomicalDateTime AddWeeks(double value)
        {
            if (value == 0.0) return this;
            if (value.IsFiniteRealNumber() == false) return this;
            if (value <= MinTickSpan / Constants.TicksPerWeek) return Info.MinAstronomicalDateTime;
            if (value >= MaxTickSpan / Constants.TicksPerWeek) return Info.MaxAstronomicalDateTime;

            // Preserve DateTime.Kind, which should be DateTimeKind.Utc.
            return new AstronomicalDateTime(Value.AddDays(value * 7.0), Info, UnspecifiedKind.IsUtc);
        }

        // Returns a new AstronomicalDateTime that adds the specified number of months to the value of this instance.
        public AstronomicalDateTime AddMonths(int value)
        {
            if (value == 0) return this;
            if (value <= 1 - Value.Month + (Value.Year - DateTimeInfo.MinUtcDateTime.Year) * -12) return Info.MinAstronomicalDateTime;
            if (value >= 12 - Value.Month + (DateTimeInfo.MaxUtcDateTime.Year - Value.Year) * 12) return Info.MaxAstronomicalDateTime;

            // Preserve DateTime.Kind, which should be DateTimeKind.Utc.
            return new AstronomicalDateTime(Value.AddMonths(value), Info, UnspecifiedKind.IsUtc);
        }

        // Returns a new AstronomicalDateTime that adds the specified number of years to the value of this instance.
        public AstronomicalDateTime AddYears(int value)
        {
            if (value == 0) return this;

            // MinUtcDateTime is the first date at 0h during the calendar year.
            // MaxUtcDateTime is the last date at 11:59:59 during the calendar year.
            if (value <= DateTimeInfo.MinUtcDateTime.Year - Value.Year) return Info.MinAstronomicalDateTime;
            if (value >= DateTimeInfo.MaxUtcDateTime.Year - Value.Year) return Info.MaxAstronomicalDateTime;

            // Preserve DateTime.Kind, which should be DateTimeKind.Utc.
            return new AstronomicalDateTime(Value.AddYears(value), Info, UnspecifiedKind.IsUtc);
        }

        // Returns a new AstronomicalDateTime that adds the specified number of ticks to the value of this instance.
        public AstronomicalDateTime AddTicks(long value)
        {
            if (value == 0) return this;
            if (value <= MinTickSpan) return Info.MinAstronomicalDateTime;
            if (value >= MaxTickSpan) return Info.MaxAstronomicalDateTime;

            // Preserve DateTime.Kind, which should be DateTimeKind.Utc.
            return new AstronomicalDateTime(Value.AddTicks(value), Info, UnspecifiedKind.IsUtc);
        }

        // Returns the localized DateTime value for the underlying client time zone.
        public DateTime ToLocalTime()
        {
            // The underlying DateTime value will never be less than MinUtcDateTime nor greater than MaxUtcDateTime,
            // thus the following should always work.
            // Thi will have a Kind value of DateTimeKind.Unspecified.
            return TimeZoneInfo.ConvertTimeFromUtc(Value, Info.ClientTimeZoneInfo);
        }

        // Returns the localized DateTime value for the underlying standard client time zone, ignoring Daylight Saving Time.
        public DateTime ToStandardTime()
        {
            // Note 1:
            // If the standard time for this AstronomicalDateTime instance is during the switch into DST, then this 
            // resultant DateTime will have an invalid time, as determined by Info.ClientTimeZoneInfo.IsInvalidTime(DateTime).
            //
            // Note 2:
            // Converting back to universal time from this resultant DateTime will produce the incorrect time
            // if DST is in effect for the client time zone.
            // Use AstronomicalDateTime.FromStandardTime(DateTime) to get the corrected UTC time.

            /* Use DateTime.SpecifyKind() */
            // The DateTimeKind needs to be explicitly set to Unspecified.
            // No need to check bounds here, since Min- and MaxUtcDateTime are built on the BaseUtcOffset values.
            DateTime standard = new(Value.AddHours(Info.BaseUtcOffset.TotalHours).Ticks, DateTimeKind.Unspecified);

            return standard;
        }

        // Indicates whether this AstronomicalDateTime instance and the specified object are equal.
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        // Returns the hash code for this AstronomicalDateTime instance.
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        // Returns the string-representation of this AstronomicalDateTime instance.
        public override string ToString()
        {
            return Value.ToString();
        }

        // Returns the Earth Rotation Angle (ERA) in total decimal degrees on the interval [0°, 360°)
        // for this current AstronomicalDateTime instance.
        public Angle GetEarthRotationAngle()
        {
            // The original expression for the ERA is given by:
            //
            //		ERA = 2pi[0.7790572732640 + 1.00273781191135448(D)]
            //
            // Where ERA is in radians, and D is the number of days since the J2000 epoch.
            //
            // The following code resembles the ERA expression rewritten in degrees.

            double era = Constants.ERA0 + (Constants.StellarDayRotation * (GetTicksFromEpoch() / Constants.TicksPerDay));

            // Relate the angle onto the interval [0°, 360°).
            return Angle.Coterminal(era);
        }

        // Returns the Greenwich Mean Sidereal Time (GMST) in total decimal degrees on the interval [0°, 360°)
        // for this current AstronomicalDateTime instance.
        public Angle GetMeanSiderealTime()
        {
            // The original expression for the GMST is given by:
            //
            //		GMST = G + 0.06570982441908(D0) + 1.00273790935(H) + 0.000026(T^2)
            //
            // Where GMST is in hours, 
            // G is the GMST at 0h UT on January 1, 2000, using the constants from the IAU 1982 GMST expression,
            // D0 is the Julian date on the previous midnight,
            // H is the universal time in total decimal hours,
            // D0 is equivalent to D − (H / 24), where D is the total number of decimal days since the J2000 epoch,
            // T is the total number of Julian centuries since the J2000 epoch, equivalent to D / 36525.
            //
            // The following code resembles the GMST expression simplified, rewritten in degrees, recalculated
            // with precise constants, and ignoring the higher order terms.

            double gmst =
                Constants.GMST0 +
                (Constants.SolarToSiderealOffsetDegrees * (GetTicksFromEpoch() / Constants.TicksPerDay)) +
                (Constants.RotationPerHour * (Value.TimeOfDay.Ticks / Constants.TicksPerHour));

            // Relate the angle onto the interval [0°, 360°).
            return Angle.Coterminal(gmst);
        }

        // Returns the Greenwich Apparent Sidereal Time (GAST) in total decimal degrees on the interval [0°, 360°)
        // for this current AstronomicalDateTime instance.
        public Angle GetSiderealTime()
        {
            // The original expression for the GAST is given by:
            //
            //		GAST = GMST + EE
            //      
            // Where EE is the equation of the equinoxes, consisting of nutation, precession, 
            // and any other high precision corrections to the mean sidereal time.
            //
            // The following code resembles the GAST expression written in degrees and simplified
            // to ignore the Earth's eccentricity, the Sun's equation of the center, the Sun's anomaly,
            // and any other high precision effects negligible for the purpose of this application.

            // Get the required angles in decimal degrees.
            Angle gmst = GetMeanSiderealTime();
            Angle eqeq = GetEquationOfEquinoxes();

            // Greenwich Apparent Sidereal Time, expressed in degrees.
            Angle gast = gmst + eqeq;

            // Relate the angle onto the interval [0°, 360°)
            return gast.Coterminal();
        }

        // Returns the ecliptic longitude of the ascending node of the Moon's orbit in total decimal degrees on the interval [0°, 360°)
        // for this current AstronomicalDateTime instance.
        public Angle GetEclipticLongitude()
        {
            // Total days since the J2000.0 epoch, including fractional days.
            double totalDays = GetTicksFromEpoch() / Constants.TicksPerDay;

            // The variables for the polynomial expression, expressed in total Julian centuries.
            double t = totalDays / 36525.0;
            double t2 = t * t;
            double t3 = t2 * t;

            // Ecliptic longitude of the ascending node of the Moon's orbit, in decimal degrees.
            double omega = Constants.Omega0 - 1934.136261 * t + 0.0020708 * t2 + t3 / 450000.0;

            // Relate the angle onto the interval [0°, 360°).
            return Angle.Coterminal(omega);
        }

        // Returns the mean longitude of the Sun in total decimal degrees on the interval [0°, 360°)
        // for this current AstronomicalDateTime instance.
        public Angle GetMeanSolarLongitude()
        {
            // Total days since the J2000.0 epoch, including fractional days.
            double totalDays = GetTicksFromEpoch() / Constants.TicksPerDay;

            // The variables for the polynomial expression, expressed in total Julian centuries.
            double t = totalDays / 36525.0;
            double t2 = t * t;

            // Mean longitude of the Sun, in decimal degrees.
            double lambdaS = Constants.LambdaS0 + 36000.76983 * t + 0.0003032 * t2;

            // Relate the angle onto the interval [0°, 360°).
            return Angle.Coterminal(lambdaS);
        }

        // Returns the mean longitude of the Earth's moon in total decimal degrees on the interval [0°, 360°)
        // for this current AstronomicalDateTime instance.
        public Angle GetMeanLunarLongitude()
        {
            // Total days since the J2000.0 epoch, including fractional days.
            double totalDays = GetTicksFromEpoch() / Constants.TicksPerDay;

            // The variables for the polynomial expression, expressed in total Julian centuries.
            double t = totalDays / 36525.0;
            double t2 = t * t;
            double t3 = t2 * t;
            double t4 = t3 * t;

            // Mean longitude of the moon, in decimal degrees.
            double lambdaM = Constants.LambdaM0 + 481267.88134236 * t - 0.00163 * t2 + t3 / 5388410.0 - t4 / 65194000.0;

            // Relate the angle onto the interval [0°, 360°).
            return Angle.Coterminal(lambdaM);
        }

        // Returns the mean obliquity of the ecliptic, or Earth's axial tilt, in total decimal degrees on the interval [0°, 360°)
        // for this current AstronomicalDateTime instance.
        public Angle GetMeanObliquity()
        {
            // Total days since the J2000.0 epoch, including fractional days.
            double totalDays = GetTicksFromEpoch() / Constants.TicksPerDay;

            // The variables for the polynomial expression, expressed in total Julian centuries.
            double t = totalDays / 36525.0;
            double t2 = t * t;
            double t3 = t2 * t;

            // Mean obliquity of the ecliptic, or Earth's axial tilt, converted from arc-seconds to degrees.
            double epsilon = (Constants.Epsilon0 - 46.8150 * t - 0.00059 * t2 + 0.001813 * t3) / 3600.0;

            // Relate the angle onto the interval [0°, 360°).
            return Angle.Coterminal(epsilon);
        }

        // Returns the nutation in longitude in total decimal degrees on the interval (-360°, 360°)
        // for this current AstronomicalDateTime instance.
        public Angle GetNutationInLongitude()
        {
            // Convert all required angles to radians.
            double omega = GetEclipticLongitude().TotalRadians;
            double lambdaS = GetMeanSolarLongitude().TotalRadians;
            double lambdaM = GetMeanLunarLongitude().TotalRadians;

            // Nutation in longitude, expressed in arc-seconds, then converted to decimal degrees.
            double deltaPsi = (
                -17.2 * Math.Sin(omega) -
                1.32 * Math.Sin(2.0 * lambdaS) -
                0.23 * Math.Sin(2.0 * lambdaM) +
                0.21 * Math.Sin(2.0 * omega)
                ) / 3600.0;

            return new Angle(deltaPsi);
        }

        // Returns the nutation in obliquity in total decimal degrees on the interval (-360°, 360°)
        // for this current AstronomicalDateTime instance.
        public Angle GetNutationInObliquity()
        {
            // Convert all required angles to radians.
            double omega = GetEclipticLongitude().TotalRadians;
            double lambdaS = GetMeanSolarLongitude().TotalRadians;
            double lambdaM = GetMeanLunarLongitude().TotalRadians;

            // Nutation in obliquity, expressed in arc-seconds, then converted to decimal degrees.
            double deltaEpsilon = (
                9.2 * Math.Cos(omega) +
                0.57 * Math.Cos(2.0 * lambdaS) +
                0.10 * Math.Cos(2.0 * lambdaM) -
                0.09 * Math.Cos(2.0 * omega)
                ) / 3600.0;

            return new Angle(deltaEpsilon);
        }

        // Returns the equation of the equinoxes in total decimal degrees on the interval (-360°, 360°)
        // for this current AstronomicalDateTime instance.
        public Angle GetEquationOfEquinoxes()
        {
            // Convert the obliquity and nutation in obliquity to radians.
            double epsilon = GetMeanObliquity().TotalRadians;
            double deltaEpsilon = GetNutationInObliquity().TotalRadians;

            // Get the nutation in longitude as degrees.
            // The unit of measurement here determines that of the equation of the equinoxes.
            double deltaPsi = GetNutationInLongitude().TotalDegrees;

            // Equation of the equinoxes, using the true obliquity, expressed in decimal degrees.
            double eqeq = deltaPsi * Math.Cos(epsilon + deltaEpsilon);

            return new Angle(eqeq);
        }

        // Returns the equation of the origins in total decimal degrees on the interval (-360°, 360°)
        // for this current AstronomicalDateTime instance.
        public Angle GetEquationOfOrigins()
        {
            // EO = ERA - GAST
            // The resultant angle will lie on the interval (-360°, 360°).
            Angle eo = GetEarthRotationAngle() - GetSiderealTime();

            return eo;
        }
    }
}