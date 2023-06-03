using DST.Core.Coordinate;
using DST.Core.TimeKeeper;
using DST.Core.Physics;
using DST.Core.DateAndTime;

namespace DST.Core.Observer
{
    public class SiderealObserver : BaseLocalObserver, IVariableDeclination, IVariableRightAscension
    {
        // Creates a new SiderealObserver instance using the specified IDateTimeInfo, IGeographicCoordinate,
        // IEquatorialCoordinate, and ITimeKeeper.
        public SiderealObserver(IDateTimeInfo dateTimeInfo, IGeographicCoordinate location, IEquatorialCoordinate target, ITimeKeeper timeKeeper)
            : base(dateTimeInfo, location, target, timeKeeper)
        { }

        // Returns the calculated declination angle at the specified IAstronomicalDateTime value for this IObserver.Target.
        public Angle GetDeclination(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            return Target.GetNutation(dateTime).Declination;
        }

        // Returns the calculated right ascension angle at the specified IAstronomicalDateTime value for this IObserver.Target.
        public Angle GetRightAscension(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            return Target.GetNutation(dateTime).RightAscension;
        }
    }
}
