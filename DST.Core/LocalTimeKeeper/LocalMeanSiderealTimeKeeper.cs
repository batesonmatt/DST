using DST.Core.Observer;
using DST.Core.TimeKeeper;
using DST.Core.Physics;

namespace DST.Core.LocalTimeKeeper
{
    public class LocalMeanSiderealTimeKeeper : BaseLocalTimeKeeper
    {
        // Creates a new LocalMeanSiderealTimeKeeper given a specified ITimeKeeper argument.
        public LocalMeanSiderealTimeKeeper(ITimeKeeper timeKeeper)
            : base(timeKeeper)
        { }

        // Returns the local mean sidereal time (LMST) for the specified ILocalObserver and AstronomicalDateTime arguments.
        public override Angle Calculate(ILocalObserver localObserver, AstronomicalDateTime dateTime)
        {
            return base.Calculate(localObserver, dateTime);
        }
    }
}
