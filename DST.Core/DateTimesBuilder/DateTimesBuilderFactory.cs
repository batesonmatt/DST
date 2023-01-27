using DST.Core.DateTimeAdder;

namespace DST.Core.DateTimesBuilder
{
    public class DateTimesBuilderFactory
    {
        // Creates a new IDateTimesBuilder object given the specified IDateTimeAdder object and whether
        // to add each interval from the starting or previous date/time value.
        public static IDateTimesBuilder Create(IDateTimeAdder dateTimeAdder, bool addFromStart)
        {
            _ = dateTimeAdder ?? throw new ArgumentNullException(nameof(dateTimeAdder));

            return addFromStart switch
            {
                true => new LongDateTimesBuilder(dateTimeAdder),
                _ => new ShortDateTimesBuilder(dateTimeAdder)
            };
        }
    }
}
