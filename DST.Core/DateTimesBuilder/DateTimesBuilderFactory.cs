using DST.Core.DateTimeAdder;

namespace DST.Core.DateTimesBuilder
{
    public class DateTimesBuilderFactory
    {
        // Creates a new IDateTimesBuilder object given the specified IDateTimeAdder object and whether
        // to add each interval from the starting date (aggregated) or previous date (successive).
        public static IDateTimesBuilder Create(IDateTimeAdder dateTimeAdder, bool aggregate)
        {
            _ = dateTimeAdder ?? throw new ArgumentNullException(nameof(dateTimeAdder));

            return aggregate switch
            {
                true => new LongDateTimesBuilder(dateTimeAdder),
                _ => new ShortDateTimesBuilder(dateTimeAdder)
            };
        }
    }
}
