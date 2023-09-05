namespace DST.Core.Physics
{
    // Defines planetary/astrometric values relating to the Earth's rotation and various time scales.
    public static class Constants
    {
        // Gets the total number of ticks in a single mean solar second.
        public static double TicksPerSecond => 10000000.0;

        // Gets the total number of ticks in a single mean solar minute.
        public static double TicksPerMinute => 600000000.0;

        // Gets the total number of ticks in a single mean solar hour.
        public static double TicksPerHour => 36000000000.0;

        // Gets the total number of ticks in a single mean solar day.
        public static double TicksPerDay => 864000000000.0;

        // Gets the total number of ticks in a single mean solar week.
        public static double TicksPerWeek => 6048000000000.0;

        // Gets the total number of SI seconds in a single mean solar day.
        // This resembles Earth's mean solar rotational period measured relative to the mean fictitious Sun along the celestial equator. 
        public static double SolarDay => 86400.0;

        // Gets the total number of SI seconds in a single sidereal day.
        // This resembles Earth's sidereal rotational period measured relative to the moving March equinox.
        public static double SiderealDay => 86164.09053083288;

        // Gets the total number of SI seconds in a single stellar day.
        // This resembles Earth's stellar rotational period measured relative to the International Celestial Reference Frame (ICRF).
        // Also known as the "true" sidereal period.
        public static double StellarDay => 86164.098903690348;

        // Gets the ratio of mean solar time to sidereal time, or how many units of sidereal time are in a single unit of mean solar time.
        // Factoring SI units by this value results in mean solar units represented in sidereal time.
        public static double SolarToSiderealRatio => SolarDay / SiderealDay;

        // Gets the ratio of sidereal time to mean solar time, or how many units of mean solar time are in a single unit of sidereal time.
        // Factoring SI units by this value results in sidereal units represented in mean solar time.
        public static double SiderealToSolarRatio => SiderealDay / SolarDay;

        // Gets the ratio of mean solar time to stellar time, or how many units of stellar time are in a single unit of mean solar time.
        // Factoring SI units by this value results in mean solar units represented in stellar time.
        public static double SolarToStellarRatio => SolarDay / StellarDay;

        // Gets the ratio of stellar time to mean solar time, or how many units of mean solar time are in a single unit of stellar time.
        // Factoring SI units by this value results in stellar units represented in mean solar time.
        public static double StellarToSolarRatio => StellarDay / SolarDay;

        // Gets the amount that the Earth rotates per mean solar hour, in decimal degrees.
        public static double RotationPerHour => 15.0;

        // Gets the difference of a sidereal day from a mean solar day, in fractional sidereal hours.
        // This is derived from the fact that a solar day is approximately 24.0657 hours, expressed in sidereal time.
        public static double SolarToSiderealOffsetHours => 24.0 * (SolarToSiderealRatio - 1.0);

        /* Equivalent to 360.0 * (SolarToSiderealRatio - 1.0) */
        // Gets the difference of a sidereal day from a mean solar day, in fractional sidereal degrees.
        public static double SolarToSiderealOffsetDegrees => SolarToSiderealOffsetHours * RotationPerHour;

        // Gets the initial GMST relative to the March equinox in decimal degrees on January 1, 2000 at 12h UT.
        public static double GMST0 => 100.46061837503999;

        // Gets the initial rotation of the Earth in fractional revolutions from the Celestial Intermediate Origin (CIO) on January 1, 2000 at 12h UT.
        public static double EpochalRotation => 0.7790572732640;

        // Gets the initial Earth Rotation Angle (ERA) relative to the Celestial Intermediate Origin (CIO) in decimal degrees on January 1, 2000 at 12h UT.
        // This represents the constant term of the ERA polynomial expression, converted to degrees.
        public static double ERA0 => 360.0 * EpochalRotation;

        // Gets the amount the Earth rotates, in decimal degrees, relative to the fixed stars in 1 stellar day.
        // This represents the linear coefficient of the ERA polynomial expression, converted to degrees.
        public static double StellarDayRotation => 360.0 * SolarToStellarRatio;

        // Gets the initial ecliptic longitude of the ascending node of the moon's orbit in decimal degrees on January 1, 2000 at 12h UT.
        // This resembles the constant term of the polynomial expression for the ecliptic longitude.
        public static double Omega0 => 125.04452;

        // Gets the initial mean longitude of the Sun in decimal degrees on January 1, 2000 at 12h UT.
        // This resembles the constant term of the polynomial expression for the mean solar longitude.
        public static double LambdaS0 => 280.4664611;

        // The initial mean longitude of the moon in decimal degrees on January 1, 2000 at 12h UT.
        // This resembles the constant term of the polynomial expression for the mean lunar longitude.
        public static double LambdaM0 => 218.31654591;

        // Gets the initial obliquity of the ecliptic in arc-seconds with respect to the mean equator on January 1, 2000 at 12h UT.
        // This resembles the constant term of the polynomial expression for the mean obliquity of the ecliptic.
        public static double Epsilon0 => 84381.448;
    }
}