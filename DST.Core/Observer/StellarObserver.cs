using DST.Core.Coordinate;
using DST.Core.TimeKeeper;
using DST.Core.Physics;
using DST.Core.DateAndTime;

namespace DST.Core.Observer
{
    public class StellarObserver : BaseLocalObserver, IVariableRightAscension
    {
        // Creates a new StellarObserver instance using the specified DateTimeInfo, IGeographicCoordinate,
        // IEquatorialCoordinate, and ITimeKeeper.
        public StellarObserver(DateTimeInfo dateTimeInfo, IGeographicCoordinate location, IEquatorialCoordinate target, ITimeKeeper timeKeeper)
            : base(dateTimeInfo, location, target, timeKeeper)
        { }

        // Returns the calculated right ascension angle at the specified AstronomicalDateTime value for this IObserver.Target.
        public Angle GetRightAscension(AstronomicalDateTime dateTime)
        {
            return Target.GetIntermediateRightAscension(dateTime);
        }
    }
}
