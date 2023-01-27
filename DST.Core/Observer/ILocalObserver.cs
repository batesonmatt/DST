using DST.Core.Coordinate;
using DST.Core.LocalHourAngle;
using DST.Core.LocalHourAngleDateTime;
using DST.Core.LocalTimeKeeper;
using DST.Core.TimeKeeper;

namespace DST.Core.Observer
{
    public interface ILocalObserver : IObserver
    {
        IGeographicCoordinate Location { get; }
        IEquatorialCoordinate Target { get; }

        ITimeKeeper TimeKeeper { get; }
        ILocalTimeKeeper LocalTimeKeeper { get; }
        ILocalHourAngle LocalHourAngle { get; }
        ILocalHourAngleDateTime LocalHourAngleDateTime { get; }
    }
}
