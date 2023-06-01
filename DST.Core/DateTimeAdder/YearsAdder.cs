using DST.Core.TimeScalable;
using DST.Core.DateAndTime;

namespace DST.Core.DateTimeAdder
{
    public class YearsAdder : BaseDateTimeAdder
    {
        // The magnitude of the allowable amount of years to add.
        private const int _magnitude = 100;

        // Gets the minimum allowable amount of years to add.
        public override int Min => -_magnitude;

        // Gets the minimum allowable amount of years to add.
        public override int Max => _magnitude;

        // Creates a new YearsAdder instance given the specified ITimeScalable argument.
        public YearsAdder(ITimeScalable timeScalable)
            : base(timeScalable)
        { }

        // Returns a new AstronomicalDateTime value by adding the given amount of years to a 
        // specified starting AstronomicalDateTime value in mean solar time.
        // The number of years to add is converted to whole cycles in the underlying time scale.
        public override AstronomicalDateTime Add(AstronomicalDateTime start, int value)
        {
            // Let AddYears determine the ending date/time value in mean solar time.
            AstronomicalDateTime next = start.AddYears(GetFixedValue(value));

            // Get the duration in ticks.
            long ticks = next.Ticks - start.Ticks;

            // Convert the ticks to whole cycles in the underlying timescale, represented in mean solar time.
            long uniformTicks = _timeScalable.Calculate(ticks);

            // The final date/time value.
            return start.AddTicks(uniformTicks);
        }
    }
}