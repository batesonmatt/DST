namespace DST.Core.TimeScalable
{
    public class MeanSolarTimeScalable : ITimeScalable
    {
        // Returns a new value that represents the given number of ticks in whole cycles of mean solar time,
        // when converted to mean solar time.
        // Assumes 'value' is in SI units, or is already in mean solar units, and is greater than
        // or equal to one whole day.
        // This just returns the same value back, or 0 if the value is long.MinValue or long.MaxValue.
        public long Calculate(long value)
        {
            return value == long.MinValue || value == long.MaxValue ? 0 : value;
        }
    }
}