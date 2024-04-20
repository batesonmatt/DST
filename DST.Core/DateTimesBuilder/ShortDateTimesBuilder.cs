using DST.Core.DateAndTime;
using DST.Core.DateTimeAdder;

namespace DST.Core.DateTimesBuilder
{
    public class ShortDateTimesBuilder : BaseDateTimesBuilder
    {
        // Creates a new ShortDateTimesBuilder instance given the specified IDateTimeAdder argument.
        public ShortDateTimesBuilder(IDateTimeAdder dateTimeAdder)
            : base(dateTimeAdder)
        { }

        // Builds a new IBaseDateTime array given the specified starting IBaseDateTime value,
        // the period length, and the interval length.
        //
        // The date/time value at each interval will be added from the previous date/time value.
        //
        // By performing a successive interval calculation when adding in Months or Years
        // (where the number of days is not consistent) for time scales other than Mean Solar Time,
        // each consecutive date/time will resemble a full interval in whole days of the underlying
        // time scale.
        //
        // This more accurately depicts the length of each interval in the underlying time scale,
        // but might be less intuitive for some people.
        public override IBaseDateTime[] Build(IBaseDateTime start, int period, int interval)
        {
            _ = start ?? throw new ArgumentNullException(nameof(start));

            IMutableDateTime mutableStart = DateTimeFactory.ConvertToMutable(start);

            // Return an empty array if all possible datetimes in this period are out of range.
            if ((mutableStart.IsMinValue() && period <= 0) || (mutableStart.IsMaxValue() && period >= 0))
            {
                return Array.Empty<IBaseDateTime>();
            }

            List<IMutableDateTime> dateTimes = new();

            // Only add the starting datetime if it is in range.
            if (!mutableStart.IsMinOrMaxValue())
            {
                dateTimes.Add(mutableStart);
            }

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

            // The datetime at the previous interval.
            IMutableDateTime previous = mutableStart;

            // The datetime at the next interval.
            IMutableDateTime next;

            // Attempt to calculate all datetimes in the period, at each interval.
            while (IsReady(previous, elapsed, period))
            {
                // Get the previous datetime.
                previous = dateTimes.Count > 1 ? dateTimes[dateTimes.Count - 1] : mutableStart;

                // Calculate the next datetime from the previous datetime using the interval length.
                next = _dateTimeAdder.Add(previous, signedInterval);

                // Only add valid datetimes to the list.
                if (!next.IsMinOrMaxValue())
                {
                    dateTimes.Add(next);
                }

                // Advance the elapsed time by the interval length.
                elapsed += signedInterval;
            }

            return dateTimes.ToArray();
        }
    }
}
