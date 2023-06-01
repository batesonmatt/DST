using DST.Core.DateAndTime;
using DST.Core.DateTimeAdder;

namespace DST.Core.DateTimesBuilder
{
    public abstract class BaseDateTimesBuilder : IDateTimesBuilder
    {
        // The underlying IDateTimeAdder object for this BaseDateTimesBuilder instance.
        protected readonly IDateTimeAdder _dateTimeAdder;

        // Creates a new BaseDateTimesBuilder instance given the specified IDateTimeAdder argument.
        protected BaseDateTimesBuilder(IDateTimeAdder dateTimeAdder)
        {
            _dateTimeAdder = dateTimeAdder ?? throw new ArgumentNullException(nameof(dateTimeAdder));
        }

        // Builds a new AstronomicalDateTime array given the specified starting date/time value,
        // the period length, and the interval length.
        public abstract AstronomicalDateTime[] Build(AstronomicalDateTime start, int period, int interval);

        // Returns a value that indicates whether the builder may continue to add before going out of range.
        protected virtual bool IsReady(AstronomicalDateTime previous, int timeElapsed, int period)
        {
            // Stop if the previous datetime has reached the minimum or maximum supported datetime.
            if (previous.IsMinOrMaxValue()) return false;

            // Stop if the total elapsed time has reached the minimum or maximum allowable time.
            if (timeElapsed <= _dateTimeAdder.Min || timeElapsed >= _dateTimeAdder.Max) return false;

            // Stop if the total elapsed time has reached the period length.
            if (Math.Abs(timeElapsed) > Math.Abs(period)) return false;

            return true;
        }
    }
}
