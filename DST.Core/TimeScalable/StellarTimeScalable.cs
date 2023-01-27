using DST.Core.Physics;

namespace DST.Core.TimeScalable
{
    public class StellarTimeScalable : ITimeScalable
    {
        // Returns a new value that represents the given number of ticks in whole cycles of stellar time,
        // when converted to stellar time.
        // Assumes 'value' is in SI units, or is already in mean solar units, and is greater than
        // or equal to one whole day.
        // This returns 0 if the value is long.MinValue or long.MaxValue.
        public long Calculate(long value)
        {
            if (value == long.MinValue || value == long.MaxValue) return 0;

            // Convert the ticks to total days (SI).
            double totalDays = value / Constants.TicksPerDay;

            // Convert the SI days to total stellar days, truncate the remaining fractional stellar day,
            // then convert back to mean solar days.
            double result = Math.Truncate(totalDays * Constants.SolarToStellarRatio) * Constants.StellarToSolarRatio;

            // Convert back to ticks.
            return (long)(result * Constants.TicksPerDay);
        }
    }
}