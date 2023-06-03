using DST.Core.DateAndTime;
using DST.Core.Physics;
using DST.Core.TimeScalable;

namespace DST.Core.DateTimeAdder
{
    public class WeeksAdder : BaseDateTimeAdder
    {
        // The magnitude of the allowable amount of weeks to add.
        private const int _magnitude = 100;

        // Gets the minimum allowable amount of weeks to add.
        public override int Min => -_magnitude;

        // Gets the minimum allowable amount of weeks to add.
        public override int Max => _magnitude;

        // Creates a new WeeksAdder instance given the specified ITimeScalable argument.
        public WeeksAdder(ITimeScalable timeScalable)
            : base(timeScalable)
        { }

        // Returns a new IMutableDateTime value by adding the given amount of weeks to a 
        // specified starting IMutableDateTime value in mean solar time.
        // The number of weeks to add is converted to whole cycles in the underlying time scale.
        public override IMutableDateTime Add(IMutableDateTime start, int value)
        {
            _ = start ?? throw new ArgumentNullException(nameof(start));

            // Convert the weeks to ticks.
            long ticks = (long)(GetFixedValue(value) * Constants.TicksPerWeek);

            // Convert the ticks to whole cycles in the underlying timescale, represented in mean solar time.
            long uniformTicks = _timeScalable.Calculate(ticks);

            // The final date/time value.
            return start.AddTicks(uniformTicks);
        }
    }
}