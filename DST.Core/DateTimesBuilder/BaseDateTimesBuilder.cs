﻿using DST.Core.DateAndTime;
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

        // Builds a new IBaseDateTime array given the specified starting IBaseDateTime value,
        // the period length, and the interval length.
        public abstract IBaseDateTime[] Build(IBaseDateTime start, int period, int interval);

        // Returns a value that indicates whether the builder may continue to add before going out of range.
        protected virtual bool IsReady(IMutableDateTime previous, int timeElapsed, int period)
        {
            _ = previous ?? throw new ArgumentNullException(nameof(previous));

            // Stop if the previous datetime has reached the minimum or maximum supported datetime,
            // and all remaining possible datetimes in this period are out of range.
            if ((previous.IsMinValue() && period <= 0) || (previous.IsMaxValue() && period >= 0))
            {
                return false;
            }

            // Stop if the total elapsed time has surpassed the minimum or maximum allowable time.
            if (timeElapsed < _dateTimeAdder.Min || timeElapsed > _dateTimeAdder.Max) return false;

            // Stop if the total elapsed time has surpassed the period length.
            if (Math.Abs(timeElapsed) > Math.Abs(period)) return false;

            return true;
        }
    }
}
