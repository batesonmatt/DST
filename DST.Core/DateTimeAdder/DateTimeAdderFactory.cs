using DST.Core.TimeScalable;

namespace DST.Core.DateTimeAdder
{
    public class DateTimeAdderFactory
    {
        // Creates a new IDateTimeAdder object given the specified ITimeScalable object and TimeUnit value.
        public static IDateTimeAdder Create(ITimeScalable timeScalable, TimeUnit timeUnit)
        {
            _ = timeScalable ?? throw new ArgumentNullException(nameof(timeScalable));

            return timeUnit switch
            {
                TimeUnit.Seconds => new SecondsAdder(timeScalable),
                TimeUnit.Minutes => new MinutesAdder(timeScalable),
                TimeUnit.Hours => new HoursAdder(timeScalable),
                TimeUnit.Days => new DaysAdder(timeScalable),
                TimeUnit.Weeks => new WeeksAdder(timeScalable),
                TimeUnit.Months => new MonthsAdder(timeScalable),
                TimeUnit.Years => new YearsAdder(timeScalable),
                _ => throw new NotSupportedException($"{nameof(TimeUnit)} value '{timeUnit}' is not supported.")
            };
        }
    }
}