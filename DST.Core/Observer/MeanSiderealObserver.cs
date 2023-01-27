using DST.Core.Coordinate;
using DST.Core.TimeKeeper;

namespace DST.Core.Observer
{
    public class MeanSiderealObserver : BaseLocalObserver
    {
        // Creates a new MeanSiderealObserver instance using the specified IGeographicCoordinate, IEquatorialCoordinate, and ITimeKeeper.
        public MeanSiderealObserver(IGeographicCoordinate location, IEquatorialCoordinate target, ITimeKeeper timeKeeper)
            : base(location, target, timeKeeper)
        { }
    }
}
