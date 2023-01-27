using DST.Core.Coordinate;
using DST.Core.TimeKeeper;
using DST.Core.Physics;

namespace DST.Core.Observer
{
    public class StellarObserver : BaseLocalObserver, IVariableRightAscension
    {
        // Creates a new StellarObserver instance using the specified IGeographicCoordinate, IEquatorialCoordinate, and ITimeKeeper.
        public StellarObserver(IGeographicCoordinate location, IEquatorialCoordinate target, ITimeKeeper timeKeeper)
            : base(location, target, timeKeeper)
        { }

        // Returns the calculated right ascension angle at the specified AstronomicalDateTime value for this IObserver.Target.
        public Angle GetRightAscension(AstronomicalDateTime dateTime)
        {
            return Target.GetIntermediateRightAscension(dateTime);
        }
    }
}
