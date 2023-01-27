using DST.Core.Physics;
using DST.Core.TimeScalable;

namespace DST.Core.DateTimeAdder
{
    public class SecondsAdder : BaseDateTimeAdder
    {
        // The magnitude of the allowable amount of seconds to add.
        private const int _magnitude = 60;

        // Gets the minimum allowable amount of seconds to add.
        public override int Min => -_magnitude;

        // Gets the minimum allowable amount of seconds to add.
        public override int Max => _magnitude;

        // Creates a new SecondsAdder instance given the specified ITimeScalable argument.
        public SecondsAdder(ITimeScalable timeScalable)
            : base(timeScalable)
        { }

        // Returns a new AstronomicalDateTime value by adding the given amount of seconds to a 
        // specified starting AstronomicalDateTime value in mean solar time.
        // The number of seconds to add is converted to whole cycles in the underlying time scale.
        public override AstronomicalDateTime Add(AstronomicalDateTime start, int value)
        {
            // Convert the seconds to ticks.
            long ticks = (long)(GetFixedValue(value) * Constants.TicksPerSecond);

            // Convert the ticks to whole cycles in the underlying timescale, represented in mean solar time.
            long uniformTicks = _timeScalable.Calculate(ticks);

            // The final date/time value.
            return start.AddTicks(uniformTicks);
        }
    }
}