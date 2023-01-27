using DST.Core.LocalTimeKeeper;
using DST.Core.Observer;
using DST.Core.Physics;

namespace DST.Core.LocalHourAngle
{
    public class LocalMeanSiderealHourAngle : BaseLocalHourAngle
    {
        // Creates a new LocalMeanSiderealHourAngle given the specified ILocalTimeKeeper argument.
        public LocalMeanSiderealHourAngle(ILocalTimeKeeper localTimeKeeper)
            : base(localTimeKeeper)
        { }

        // Returns the mean sidereal local hour angle (LHA) for the specified ILocalObserver and AstronomicalDateTime arguments.
        public override Angle Calculate(ILocalObserver localObserver, AstronomicalDateTime dateTime)
        {
            return base.Calculate(localObserver, dateTime);
        }
    }
}
