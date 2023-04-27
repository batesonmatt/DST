using DST.Core.Coordinate;
using DST.Core.Physics;
using DST.Core.TimeKeeper;

namespace DST.Core.Observer
{
    public class ObserverFactory
    {
        // Returns a new IObserver object given the specified IDateTimeInfo, origin and destination ICoordinate, and ITimekeeper.
        public static IObserver Create(IDateTimeInfo dateTimeInfo, ICoordinate origin, ICoordinate destination, ITimeKeeper timeKeeper)
        {
            _ = dateTimeInfo ?? throw new ArgumentNullException(nameof(dateTimeInfo));
            _ = origin ?? throw new ArgumentNullException(nameof(origin));
            _ = destination ?? throw new ArgumentNullException(nameof(destination));
            _ = timeKeeper ?? throw new ArgumentNullException(nameof(timeKeeper));

            return origin switch
            {
                IGeographicCoordinate location when destination is IEquatorialCoordinate target
                    => Create(dateTimeInfo, location, target, timeKeeper),
                _ => throw new NotSupportedException(
                    $"{nameof(ICoordinate)} types '{origin.GetType()}' and '{destination.GetType()}' are not supported.")
            };
        }

        // Returns a new ILocalObserver object given the specified IDateTimeInfo, IGeographicCoordinate,
        // IEquatorialCoordinate, and ITimeKeeper.
        private static ILocalObserver Create(IDateTimeInfo dateTimeInfo, IGeographicCoordinate location, IEquatorialCoordinate target, ITimeKeeper timeKeeper)
        {
            _ = dateTimeInfo ?? throw new ArgumentNullException(nameof(dateTimeInfo));
            _ = location ?? throw new ArgumentNullException(nameof(location));
            _ = target ?? throw new ArgumentNullException(nameof(target));
            _ = timeKeeper ?? throw new ArgumentNullException(nameof(timeKeeper));

            return dateTimeInfo switch
            {
                DateTimeInfo info => timeKeeper switch
                {
                    MeanSiderealTimeKeeper => new MeanSiderealObserver(info, location, target, timeKeeper),
                    SiderealTimeKeeper => new SiderealObserver(info, location, target, timeKeeper),
                    StellarTimeKeeper => new StellarObserver(info, location, target, timeKeeper),
                    _ => throw new NotSupportedException($"{nameof(ITimeKeeper)} type '{timeKeeper.GetType()}' is not supported.")
                },
                _ => throw new NotSupportedException($"{nameof(IDateTimeInfo)} type '{dateTimeInfo.GetType()}' is not supported.")
            };
        }
    }
}
