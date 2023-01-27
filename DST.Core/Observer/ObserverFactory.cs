using DST.Core.Coordinate;
using DST.Core.TimeKeeper;

namespace DST.Core.Observer
{
    public class ObserverFactory
    {
        // Returns a new IObserver object given the specified origin and destination ICoordinate, and ITimekeeper.
        public static IObserver Create(ICoordinate origin, ICoordinate destination, ITimeKeeper timeKeeper)
        {
            _ = origin ?? throw new ArgumentNullException(nameof(origin));
            _ = destination ?? throw new ArgumentNullException(nameof(destination));
            _ = timeKeeper ?? throw new ArgumentNullException(nameof(timeKeeper));

            return origin switch
            {
                IGeographicCoordinate location when destination is IEquatorialCoordinate target => Create(location, target, timeKeeper),
                _ => throw new NotSupportedException(
                    $"{nameof(ICoordinate)} types '{origin.GetType()}' and '{destination.GetType()}' are not supported.")
            };
        }

        // Returns a new ILocalObserver object given the specified IGeographicCoordinate, IEquatorialCoordinate, and ITimeKeeper.
        private static ILocalObserver Create(IGeographicCoordinate location, IEquatorialCoordinate target, ITimeKeeper timeKeeper)
        {
            _ = location ?? throw new ArgumentNullException(nameof(location));
            _ = target ?? throw new ArgumentNullException(nameof(target));
            _ = timeKeeper ?? throw new ArgumentNullException(nameof(timeKeeper));

            return timeKeeper switch
            {
                MeanSiderealTimeKeeper => new MeanSiderealObserver(location, target, timeKeeper),
                SiderealTimeKeeper => new SiderealObserver(location, target, timeKeeper),
                StellarTimeKeeper => new StellarObserver(location, target, timeKeeper),
                _ => throw new NotSupportedException($"{nameof(ITimeKeeper)} type '{timeKeeper.GetType()}' is not supported.")
            };
        }
    }
}
