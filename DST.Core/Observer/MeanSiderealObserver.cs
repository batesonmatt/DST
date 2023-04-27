using DST.Core.Coordinate;
using DST.Core.Physics;
using DST.Core.TimeKeeper;

namespace DST.Core.Observer
{
    public class MeanSiderealObserver : BaseLocalObserver
    {
        // Creates a new MeanSiderealObserver instance using the specified DateTimeInfo, IGeographicCoordinate,
        // IEquatorialCoordinate, and ITimeKeeper.
        public MeanSiderealObserver(DateTimeInfo dateTimeInfo, IGeographicCoordinate location, IEquatorialCoordinate target, ITimeKeeper timeKeeper)
            : base(dateTimeInfo, location, target, timeKeeper)
        { }
    }
}
