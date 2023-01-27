using DST.Core.Physics;
using DST.Core.TimeScalable;

namespace DST.Core.DateTimeAdder
{
    public class HoursAdder : BaseDateTimeAdder
    {
        // The magnitude of the allowable amount of hours to add.
        private const int _magnitude = 24;

        // Gets the minimum allowable amount of hours to add.
        public override int Min => -_magnitude;

        // Gets the maximum allowable amount of hours to add.
        public override int Max => _magnitude;

        // Creates a new HoursAdder instance given the specified ITimeScalable argument.
        public HoursAdder(ITimeScalable timeScalable)
            : base(timeScalable)
        { }

        // Returns a new AstronomicalDateTime value by adding the given amount of hours to a 
        // specified starting AstronomicalDateTime value in mean solar time.
        // The number of hours to add is converted to whole cycles in the underlying time scale.
        public override AstronomicalDateTime Add(AstronomicalDateTime start, int value)
        {
            // Convert the hours to ticks.
            long ticks = (long)(GetFixedValue(value) * Constants.TicksPerHour);

            // Convert the ticks to whole cycles in the underlying timescale, represented in mean solar time.
            long uniformTicks = _timeScalable.Calculate(ticks);

            // The final date/time value.
            return start.AddTicks(uniformTicks);
        }
    }
}