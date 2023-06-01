using DST.Core.TimeScalable;
using DST.Core.DateAndTime;

namespace DST.Core.DateTimeAdder
{
    public abstract class BaseDateTimeAdder : IDateTimeAdder
    {
        // The underlying ITimeScalable object for this BaseDateTimeAdder instance.
        protected readonly ITimeScalable _timeScalable;

        // Gets the minimum allowable amount of time to add.
        public virtual int Min { get; }

        // Gets the maximum allowable amount of time to add.
        public virtual int Max { get; }

        // Creates a new BaseDateTimeAdder instance given the specified ITimeScalable argument.
        protected BaseDateTimeAdder(ITimeScalable timeScalable)
        {
            _timeScalable = timeScalable ?? throw new ArgumentNullException(nameof(timeScalable)); ;
        }

        // Returns a new AstronomicalDateTime value by adding the given amount of time to a 
        // specified starting AstronomicalDateTime value.
        public abstract AstronomicalDateTime Add(AstronomicalDateTime start, int value);

        // Returns a new value by adjusting the given amount of time if it is outside the allowable range.
        protected virtual int GetFixedValue(int value)
        {
            if (value < Min) return Min;
            if (value > Max) return Max;
            return value;
        }
    }
}