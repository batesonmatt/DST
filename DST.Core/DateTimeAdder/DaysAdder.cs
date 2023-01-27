﻿using DST.Core.Physics;
using DST.Core.TimeScalable;

namespace DST.Core.DateTimeAdder
{
    public class DaysAdder : BaseDateTimeAdder
    {
        // The magnitude of the allowable amount of days to add.
        private const int _magnitude = 100;

        // Gets the minimum allowable amount of days to add.
        public override int Min => -_magnitude;

        // Gets the maximum allowable amount of days to add.
        public override int Max => _magnitude;

        // Creates a new DaysAdder instance given the specified ITimeScalable argument.
        public DaysAdder(ITimeScalable timeScalable)
            : base(timeScalable)
        { }

        // Returns a new AstronomicalDateTime value by adding the given amount of days to a 
        // specified starting AstronomicalDateTime value in mean solar time.
        // The number of days to add is converted to whole cycles in the underlying time scale.
        public override AstronomicalDateTime Add(AstronomicalDateTime start, int value)
        {
            // Convert the days to ticks.
            long ticks = (long)(GetFixedValue(value) * Constants.TicksPerDay);

            // Convert the ticks to whole cycles in the underlying timescale, represented in mean solar time.
            long uniformTicks = _timeScalable.Calculate(ticks);

            // The final date/time value.
            return start.AddTicks(uniformTicks);
        }
    }
}