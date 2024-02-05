namespace DST.Core.DateAndTime
{
    public class DateTimeFactory
    {
        // Creates a new IBaseDateTime object given the specified DateTime value and IDateTimeInfo object.
        public static IBaseDateTime CreateBase(DateTime dateTime, IDateTimeInfo dateTimeInfo)
        {
            return CreateAstronomicalDateTime(dateTime, dateTimeInfo);
        }

        // Creates a new IDateTime object given the specified DateTime value and IDateTimeInfo object.
        public static IDateTime CreateDateTime(DateTime dateTime, IDateTimeInfo dateTimeInfo)
        {
            return CreateAstronomicalDateTime(dateTime, dateTimeInfo);
        }

        // Creates a new IClientDateTime object given the specified DateTime value and IDateTimeInfo object.
        public static IClientDateTime CreateClient(DateTime dateTime, IDateTimeInfo dateTimeInfo)
        {
            return CreateAstronomicalDateTime(dateTime, dateTimeInfo);
        }

        // Creates a new IAstronomicalDateTime object given the specified DateTime value and IDateTimeInfo object.
        public static IAstronomicalDateTime CreateAstronomical(DateTime dateTime, IDateTimeInfo dateTimeInfo)
        {
            return CreateAstronomicalDateTime(dateTime, dateTimeInfo);
        }

        // Creates a new IMutableDateTime object given the specified DateTime value and IDateTimeInfo object.
        public static IMutableDateTime CreateMutable(DateTime dateTime, IDateTimeInfo dateTimeInfo)
        {
            return CreateAstronomicalDateTime(dateTime, dateTimeInfo);
        }

        // Creates a new DateTime value in local time for the current date and time, in the timezone of the specified IDateTimeInfo object.
        public static DateTime CreateLocal(IDateTimeInfo dateTimeInfo)
        {
            return CreateAstronomicalDateTime(DateTime.UtcNow, dateTimeInfo).ToLocalTime();
        }

        // Creates a new DateTime value in local time from the given number of ticks, in the timezone of the specified IDateTimeInfo object.
        public static DateTime CreateLocal(long ticks, IDateTimeInfo dateTimeInfo)
        {
            return CreateAstronomicalDateTime(new DateTime(ticks), dateTimeInfo).ToLocalTime();
        }

        // Returns a new IMutableDateTime object with the same value of the specified IBaseDateTime object.
        public static IMutableDateTime ConvertToMutable(IBaseDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            return dateTime switch
            {
                IMutableDateTime mutable => mutable,
                _ => throw new NotSupportedException($"{nameof(IBaseDateTime)} type '{dateTime.GetType()}' is not supported.")
            };
        }

        // Returns a new IAstronomicalDateTime object with the same value of the specified IBaseDateTime object.
        public static IAstronomicalDateTime ConvertToAstronomical(IBaseDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            return dateTime switch
            {
                IAstronomicalDateTime astronomical => astronomical,
                _ => throw new NotSupportedException($"{nameof(IBaseDateTime)} type '{dateTime.GetType()}' is not supported.")
            };
        }

        // Returns a new IAstronomicalDateTime array with the same value and items of the specified IBaseDateTime array.
        public static IAstronomicalDateTime[] ConvertToAstronomical(IBaseDateTime[] dateTimes)
        {
            _ = dateTimes ?? throw new ArgumentNullException(nameof(dateTimes));

            IAstronomicalDateTime[] astronomicalDateTimes = new IAstronomicalDateTime[dateTimes.Length];

            for (int i = 0; i < dateTimes.Length; i++)
            {
                astronomicalDateTimes[i] = ConvertToAstronomical(dateTimes[i]);
            }

            return astronomicalDateTimes;
        }

        // Creates a new AstronomicalDateTime object given the specified DateTime value and IDateTimeInfo object.
        private static AstronomicalDateTime CreateAstronomicalDateTime(DateTime dateTime, IDateTimeInfo dateTimeInfo)
        {
            _ = dateTimeInfo ?? throw new ArgumentNullException(nameof(dateTimeInfo));

            return dateTimeInfo switch
            {
                DateTimeInfo => new AstronomicalDateTime(dateTime, dateTimeInfo),
                _ => throw new NotSupportedException($"{nameof(IDateTimeInfo)} type '{dateTimeInfo.GetType()}' is not supported.")
            };
        }
    }
}