using DST.Core.Coordinate;
using DST.Core.DateAndTime;
using DST.Core.TimeKeeper;

namespace DST.Core.Observer
{
    public class MeanSiderealObserver : BaseLocalObserver
    {
        // Creates a new MeanSiderealObserver instance using the specified IDateTimeInfo, IGeographicCoordinate,
        // IEquatorialCoordinate, and ITimeKeeper.
        public MeanSiderealObserver(IDateTimeInfo dateTimeInfo, IGeographicCoordinate location, IEquatorialCoordinate target, ITimeKeeper timeKeeper)
            : base(dateTimeInfo, location, target, timeKeeper)
        { }
    }
}
