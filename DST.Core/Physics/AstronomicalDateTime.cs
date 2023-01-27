using System.Globalization;

namespace DST.Core.Physics
{
    // Represents a DateTime value in Universal Time, for the current system time zone, on the Gregorian calendar.
    public readonly partial struct AstronomicalDateTime
    {
        // Gets the calendar on which the underlying DateTime value lies.
        public static Calendar Calendar { get; } = new GregorianCalendar(GregorianCalendarTypes.Localized);

        // Gets the most recent epoch date and time, represented as the Gregorian date in universal time.
        // This resembles the Julian date in Terrestrial Time (TT), which may be proved by Epoch.ToOADate() + 2415018.5 = 2451545.0.
        // A DateTimeKind of Utc is necessary for the +0h offset.
        public static DateTime Epoch { get; } = new(2000, 1, 1, 12, 0, 0, 0, Calendar, DateTimeKind.Utc);

        // Gets a value indicating whether the time zone has any daylight saving time rules.
        // This returns true if the time zone supports daylight saving time; otherwise, false.
        public static bool SupportsDaylightSavingTime { get; } = TimeZoneInfo.Local.SupportsDaylightSavingTime;

        // Gets the Coordinated Universal Time (UTC) offset for the standard local time zone.
        public static TimeSpan BaseUtcOffset { get; } = TimeZoneInfo.Local.BaseUtcOffset;

        // Gets the minimum allowable UTC DateTime for the current local time zone.
        public static DateTime MinUtcDateTime { get; }
            = BaseUtcOffset < TimeSpan.Zero ? Calendar.MinSupportedDateTime.Subtract(BaseUtcOffset) : Calendar.MinSupportedDateTime;

        // Gets the maximum allowable UTC DateTime for the current local time zone.
        public static DateTime MaxUtcDateTime { get; }
            = BaseUtcOffset > TimeSpan.Zero ? Calendar.MaxSupportedDateTime.Subtract(BaseUtcOffset) : Calendar.MaxSupportedDateTime;

        // Gets the minimum supported value for an AstronomicalDateTime.
        public static AstronomicalDateTime MinValue { get; } = new(MinUtcDateTime);

        // Gets the maximum supported value for an AstronomicalDateTime.
        public static AstronomicalDateTime MaxValue { get; } = new(MaxUtcDateTime);

        // Gets the total number of ticks from the epoch to MinUtcDateTime.
        // This value is negative.
        public static long MinEpochTickSpan { get; } = MinUtcDateTime.Ticks - Epoch.Ticks;

        // Gets the total number of ticks from the epoch to MaxUtcDateTime.
        // This value is positive.
        public static long MaxEpochTickSpan { get; } = MaxUtcDateTime.Ticks - Epoch.Ticks;

        // Gets a new AstronomicalDateTime instance resembling the current UTC date and time.
        public static AstronomicalDateTime Now { get; } = new(DateTime.UtcNow);

        // Gets a new AstronomicalDateTime instance resembling the current UTC date with the time value set to midnight (00:00:00).
        public static AstronomicalDateTime Today { get; } = new(DateTime.UtcNow.Date);

        // Gets the underlying date and time value, represented in universal time.
        public DateTime Value { get; }

        // Gets the number of ticks that represent the date and time of this AstronomicalDateTime instance.
        public long Ticks => Value.Ticks;

        // Gets the date component of the underlying UTC DateTime value.
        public AstronomicalDateTime Date => new(Value.Date);

        // Gets the time of day of the underlying DateTime value, represented in universal time.
        public TimeSpan Time => Value.TimeOfDay;

        // Gets the Coordinated Universal Time (UTC) offset for the current local time zone.
        public TimeSpan UtcOffset => TimeZoneInfo.Local.GetUtcOffset(Value);

        // Gets the total number of ticks from the underlying DateTime value (Value) to MinUtcDateTime.
        // This value is negative.
        public long MinTickSpan => MinUtcDateTime.Ticks - Ticks;

        // Gets the total number of ticks from the underlying DateTime value (Value) to MaxUtcDateTime.
        // This value is positive.
        public long MaxTickSpan => MaxUtcDateTime.Ticks - Ticks;

        // Creates a new AstronomicalDateTime with the specified DateTime value.
        // If dateTime.Kind equals DateTimeKind.Unspecified, then this will assume local time.
        public AstronomicalDateTime(DateTime dateTime)
            : this(dateTime, UnspecifiedKind.IsLocal)
        { }

        // Creates a new AstronomicalDateTime with the specified DateTime value.
        // If dateTime.Kind equals DateTimeKind.Unspecified, then this will assume the value of 'kind'.
        public AstronomicalDateTime(DateTime dateTime, UnspecifiedKind kind)
        {
            Value = default;
            Value = GetAdjustedDateTime(dateTime, kind);
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

        // Returns a new AstronomicalDateTime, converted from a local date and time value in the standard time zone.
        // If dateTime.Kind already equals DateTimeKind.Utc, then this will not modify the date or time.
        public static AstronomicalDateTime FromStandardTime(DateTime dateTime)
        {
            if (dateTime.Kind != DateTimeKind.Utc)
            {
                // If DST is in effect for the current local timezone, use the base UTC offset to get the universal time.
                if (SupportsDaylightSavingTime && dateTime.IsDaylightSavingTime())
                {
                    // Verify that the local date and time will be in range when converted to universal time.
                    // If this fails, then the resultant AstronomicalDateTime will have a value of either this.MinValue or this.MaxValue.
                    if (dateTime >= MinUtcDateTime.ToLocalTime() && dateTime <= MaxUtcDateTime.ToLocalTime())
                    {
                        dateTime = dateTime.Subtract(BaseUtcOffset);
                    }
                }
            }

            return new(dateTime, UnspecifiedKind.IsUtc);
        }

        // Restricts an instance of DateTime onto the Gregorian calendar in universal time.
        // Converts argument 'dateTime' to UTC if dateTime.Kind equals DateTimeKind.Local, or if dateTime.Kind
        // equals DateTimeKind.Unspecified and argument 'kind' equals UnspecifiedKind.IsLocal.
        private static DateTime GetAdjustedDateTime(DateTime dateTime, UnspecifiedKind kind)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified && kind == UnspecifiedKind.IsLocal ||
                dateTime.Kind == DateTimeKind.Local)
            {
                // The date and time value is being treated as local time, so convert to universal time.
                dateTime = dateTime.ToUniversalTime();
            }

            // Set the year.
            int year = dateTime.Year;
            if (year < 1) year = 1;
            if (year > MaxUtcDateTime.Year) year = MaxUtcDateTime.Year;

            // Set the month.
            int month = dateTime.Month;
            int monthsInYear = Calendar.GetMonthsInYear(year);
            if (month < 1) month = 1;
            if (month > monthsInYear) month = monthsInYear;

            // Set the day.
            int day = dateTime.Day;
            int daysInMonth = Calendar.GetDaysInMonth(year, month);
            if (day < 1) day = 1;
            if (day > daysInMonth) day = daysInMonth;

            // The adjusted date is finished.
            DateTime date = new(year, month, day);

            // Get the time.
            TimeSpan time = dateTime.TimeOfDay;

            // Verify that the date's time of day is in range.
            if (date == MinUtcDateTime.Date)
            {
                if (time < MinUtcDateTime.TimeOfDay)
                {
                    time = MinUtcDateTime.TimeOfDay;
                }
            }
            else if (date == MaxUtcDateTime.Date)
            {
                if (time > MaxUtcDateTime.TimeOfDay)
                {
                    time = MaxUtcDateTime.TimeOfDay;
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
                Calendar,
                DateTimeKind.Utc);

            return result;
        }

        // Returns a value that determines whether this instance is equal to either the minimum or maximum supported
        // AstronomicalDateTime values.
        public bool IsMinOrMaxValue()
        {
            return this == MinValue || this == MaxValue;
        }

        // Returns the number of ticks between this current AstronomicalDateTime instance and the most recent epoch event.
        // The resulting value may be positive or negative.
        public long GetTicksFromEpoch()
        {
            long value = Value.Ticks;
            long epoch = Epoch.Ticks;
            long bounds = value >= epoch ? MaxUtcDateTime.Ticks : MinUtcDateTime.Ticks;

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
                return value >= epoch ? MaxEpochTickSpan : MinEpochTickSpan;
            }
        }

        // Returns a new AstronomicalDateTime that adds the specified number of seconds to the value of this instance.
        public AstronomicalDateTime AddSeconds(double value)
        {
            if (value == 0.0) return this;
            if (value.IsFiniteRealNumber() == false) return this;
            if (value <= MinTickSpan / Constants.TicksPerSecond) return MinValue;
            if (value >= MaxTickSpan / Constants.TicksPerSecond) return MaxValue;

            // Preserve DateTime.Kind, which should be DateTimeKind.Utc.
            return new AstronomicalDateTime(Value.AddSeconds(value), UnspecifiedKind.IsUtc);
        }

        // Returns a new AstronomicalDateTime that adds the specified number of minutes to the value of this instance.
        public AstronomicalDateTime AddMinutes(double value)
        {
            if (value == 0.0) return this;
            if (value.IsFiniteRealNumber() == false) return this;
            if (value <= MinTickSpan / Constants.TicksPerMinute) return MinValue;
            if (value >= MaxTickSpan / Constants.TicksPerMinute) return MaxValue;

            // Preserve DateTime.Kind, which should be DateTimeKind.Utc.
            return new AstronomicalDateTime(Value.AddMinutes(value), UnspecifiedKind.IsUtc);
        }

        // Returns a new AstronomicalDateTime that adds the specified number of hours to the value of this instance.
        public AstronomicalDateTime AddHours(double value)
        {
            if (value == 0.0) return this;
            if (value.IsFiniteRealNumber() == false) return this;
            if (value <= MinTickSpan / Constants.TicksPerHour) return MinValue;
            if (value >= MaxTickSpan / Constants.TicksPerHour) return MaxValue;

            // Preserve DateTime.Kind, which should be DateTimeKind.Utc.
            return new AstronomicalDateTime(Value.AddHours(value), UnspecifiedKind.IsUtc);
        }

        // Returns a new AstronomicalDateTime that adds the specified number of days to the value of this instance.
        public AstronomicalDateTime AddDays(double value)
        {
            if (value == 0.0) return this;
            if (value.IsFiniteRealNumber() == false) return this;
            if (value <= MinTickSpan / Constants.TicksPerDay) return MinValue;
            if (value >= MaxTickSpan / Constants.TicksPerDay) return MaxValue;

            // Preserve DateTime.Kind, which should be DateTimeKind.Utc.
            return new AstronomicalDateTime(Value.AddDays(value), UnspecifiedKind.IsUtc);
        }

        // Returns a new AstronomicalDateTime that adds the specified number of weeks to the value of this instance.
        public AstronomicalDateTime AddWeeks(double value)
        {
            if (value == 0.0) return this;
            if (value.IsFiniteRealNumber() == false) return this;
            if (value <= MinTickSpan / Constants.TicksPerWeek) return MinValue;
            if (value >= MaxTickSpan / Constants.TicksPerWeek) return MaxValue;

            // Preserve DateTime.Kind, which should be DateTimeKind.Utc.
            return new AstronomicalDateTime(Value.AddDays(value * 7.0), UnspecifiedKind.IsUtc);
        }

        // Returns a new AstronomicalDateTime that adds the specified number of months to the value of this instance.
        public AstronomicalDateTime AddMonths(int value)
        {
            if (value == 0) return this;
            if (value <= 1 - Value.Month + (Value.Year - MinUtcDateTime.Year) * -12) return MinValue;
            if (value >= 12 - Value.Month + (MaxUtcDateTime.Year - Value.Year) * 12) return MaxValue;

            // Preserve DateTime.Kind, which should be DateTimeKind.Utc.
            return new AstronomicalDateTime(Value.AddMonths(value), UnspecifiedKind.IsUtc);
        }

        // Returns a new AstronomicalDateTime that adds the specified number of years to the value of this instance.
        public AstronomicalDateTime AddYears(int value)
        {
            if (value == 0) return this;

            // MinUtcDateTime is the first date at 0h during the calendar year.
            // MaxUtcDateTime is the last date at 11:59:59 during the calendar year.
            if (value <= MinUtcDateTime.Year - Value.Year) return MinValue;
            if (value >= MaxUtcDateTime.Year - Value.Year) return MaxValue;

            // Preserve DateTime.Kind, which should be DateTimeKind.Utc.
            return new AstronomicalDateTime(Value.AddYears(value), UnspecifiedKind.IsUtc);
        }

        // Returns a new AstronomicalDateTime that adds the specified number of ticks to the value of this instance.
        public AstronomicalDateTime AddTicks(long value)
        {
            if (value == 0) return this;
            if (value <= MinTickSpan) return MinValue;
            if (value >= MaxTickSpan) return MaxValue;

            // Preserve DateTime.Kind, which should be DateTimeKind.Utc.
            return new AstronomicalDateTime(Value.AddTicks(value), UnspecifiedKind.IsUtc);
        }

        // Returns the localized date and time for the current time zone.
        public DateTime ToLocalTime()
        {
            // The underlying DateTime value will never be less than MinUtcDateTime nor greater than MaxUtcDateTime,
            // as determined by the static constructor, and thus calling DateTime.ToLocalTime() should always work.
            return Value.ToLocalTime();
        }

        // Returns the localized date and time for the standard time zone, ignoring Daylight Saving Time.
        public DateTime ToStandardTime()
        {
            DateTime local = ToLocalTime();

            // If DST is in effect for the current local timezone, use the base UTC offset to get the standard local time.
            if (SupportsDaylightSavingTime && local.IsDaylightSavingTime())
            {
                double delta = BaseUtcOffset.TotalHours - UtcOffset.TotalHours;

                // Note 1:
                // If the standard time for this AstronomicalDateTime instance is during the switch into DST, then this 
                // resultant DateTime will have an invalid time, as determined by TimeZoneInfo.Local.IsInvalidTime(DateTime).
                //
                // Note 2:
                // Converting back to universal time from this resultant DateTime will produce the incorrect time
                // if DST is in effect for the current local time zone.
                // Use AstronomicalDateTime.FromStandardTime(DateTime) to get the corrected UTC time.

                // No need to check bounds here, since Min- and MaxUtcDateTime are built on the BaseUtcOffset values.
                local = local.AddHours(delta);
            }

            return local;
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