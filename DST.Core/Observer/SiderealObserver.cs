using DST.Core.Coordinate;
using DST.Core.TimeKeeper;
using DST.Core.Physics;

namespace DST.Core.Observer
{
    public class SiderealObserver : BaseLocalObserver, IVariableDeclination, IVariableRightAscension
    {
        // Creates a new SiderealObserver instance using the specified IGeographicCoordinate, IEquatorialCoordinate, and ITimeKeeper.
        public SiderealObserver(IGeographicCoordinate location, IEquatorialCoordinate target, ITimeKeeper timeKeeper)
            : base(location, target, timeKeeper)
        { }

        // Returns the calculated declination angle at the specified AstronomicalDateTime value for this IObserver.Target.
        public Angle GetDeclination(AstronomicalDateTime dateTime)
        {
            return Target.GetNutation(dateTime).Declination;
        }

        // Returns the calculated right ascension angle at the specified AstronomicalDateTime value for this IObserver.Target.
        public Angle GetRightAscension(AstronomicalDateTime dateTime)
        {
            return Target.GetNutation(dateTime).RightAscension;
        }
    }
}
