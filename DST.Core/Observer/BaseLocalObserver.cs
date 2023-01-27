using DST.Core.Coordinate;
using DST.Core.LocalHourAngle;
using DST.Core.LocalHourAngleDateTime;
using DST.Core.LocalTimeKeeper;
using DST.Core.TimeKeeper;

namespace DST.Core.Observer
{
    public abstract class BaseLocalObserver : IObserver, ILocalObserver
    {
        // Gets the originating coordinate for this BaseObserver.
        public ICoordinate Origin => Location;

        // Gets the destination coordinate for this BaseObserver.
        public ICoordinate Destination => Target;

        // Gets the geographic location for this BaseObserver.
        public IGeographicCoordinate Location { get; }

        // Gets the equatorial target for this BaseObserver.
        public IEquatorialCoordinate Target { get; }

        // Gets the ITimeKeeper object for this BaseObserver.
        public ITimeKeeper TimeKeeper { get; }

        // Gets the ILocalTimeKeeper object for this BaseObserver.
        public ILocalTimeKeeper LocalTimeKeeper => LocalTimeKeeperFactory.Create(TimeKeeper);

        // Gets the ILocalHourAngle object for this BaseObserver.
        public ILocalHourAngle LocalHourAngle => LocalHourAngleFactory.Create(LocalTimeKeeper);

        // Gets the ILocalHourAngleDateTime object for this BaseObserver.
        public ILocalHourAngleDateTime LocalHourAngleDateTime => LocalHourAngleDateTimeFactory.Create(LocalHourAngle);

        // Creates a new BaseObserver instance using the specified IGeographicCoordinate, IEquatorialCoordinate, and ITimeKeeper.
        protected BaseLocalObserver(IGeographicCoordinate location, IEquatorialCoordinate target, ITimeKeeper timeKeeper)
        {
            Location = location ?? throw new ArgumentNullException(nameof(location));
            Target = target ?? throw new ArgumentNullException(nameof(target));
            TimeKeeper = timeKeeper ?? throw new ArgumentNullException(nameof(timeKeeper));
        }
    }
}
