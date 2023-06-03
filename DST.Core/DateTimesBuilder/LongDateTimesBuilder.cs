using DST.Core.DateAndTime;
using DST.Core.DateTimeAdder;

namespace DST.Core.DateTimesBuilder
{
    public class LongDateTimesBuilder : BaseDateTimesBuilder
    {
        // Creates a new LongDateTimesBuilder instance given the specified IDateTimeAdder argument.
        public LongDateTimesBuilder(IDateTimeAdder dateTimeAdder)
            : base(dateTimeAdder)
        { }

        // Builds a new IBaseDateTime array given the specified starting IBaseDateTime value,
        // the period length, and the interval length.
        // The date/time value at each interval will be added from the starting date/time value.
        public override IBaseDateTime[] Build(IBaseDateTime start, int period, int interval)
        {
            _ = start ?? throw new ArgumentNullException(nameof(start));

            IMutableDateTime mutableStart = DateTimeFactory.ConvertToMutable(start);

            List<IMutableDateTime> dateTimes = new()
            {
                mutableStart
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
                // Calculate the next datetime from the starting datetime using the total elapsed time.
                dateTimes.Add(_dateTimeAdder.Add(mutableStart, elapsed));

                // Advance the elapsed time by the interval length.
                elapsed += signedInterval;
            }

            return dateTimes.ToArray();
        }
    }
}
