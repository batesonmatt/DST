using DST.Core.DateTimeAdder;
using DST.Core.Physics;

namespace DST.Core.DateTimesBuilder
{
    public class ShortDateTimesBuilder : BaseDateTimesBuilder
    {
        // Creates a new ShortDateTimesBuilder instance given the specified IDateTimeAdder argument.
        public ShortDateTimesBuilder(IDateTimeAdder dateTimeAdder)
            : base(dateTimeAdder)
        { }

        // Builds a new AstronomicalDateTime array given the specified starting date/time value,
        // the period length, and the interval length.
        // The date/time value at each interval will be added from the previous date/time value.
        public override AstronomicalDateTime[] Build(AstronomicalDateTime start, int period, int interval)
        {
            List<AstronomicalDateTime> dateTimes = new()
            {
                start
            };

            // The period length must be non-zero
            // The interval length must be positive and non-zero.
            if (period == 0 || interval <= 0)
            {
                return dateTimes.ToArray();
            }

            // Positive for future periods, or negative for historical periods.
            int direction = Math.Sign(period);

            // Get the interval length factored by the direction.
            int signedInterval = direction * interval;

            // The amount of time elapsed since the starting date/time value.
            int elapsed = signedInterval;

            while (IsReady(dateTimes[dateTimes.Count - 1], elapsed, period))
            {
                // Calculate the next datetime from the previous datetime using the interval length.
                dateTimes.Add(_dateTimeAdder.Add(dateTimes[dateTimes.Count - 1], signedInterval));

                // Advance the elapsed time by the interval length.
                elapsed += signedInterval;
            }

            return dateTimes.ToArray();
        }
    }
}
