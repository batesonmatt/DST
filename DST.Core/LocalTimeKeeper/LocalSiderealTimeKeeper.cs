using DST.Core.Observer;
using DST.Core.TimeKeeper;
using DST.Core.Physics;

namespace DST.Core.LocalTimeKeeper
{
    public class LocalSiderealTimeKeeper : BaseLocalTimeKeeper
    {
        // Creates a new LocalSiderealTimeKeeper given a specified ITimeKeeper argument.
        public LocalSiderealTimeKeeper(ITimeKeeper timeKeeper)
            : base(timeKeeper)
        { }

        // Returns the local apparent sidereal time (LAST/LST) for the specified ILocalObserver and AstronomicalDateTime arguments.
        public override Angle Calculate(ILocalObserver localObserver, AstronomicalDateTime dateTime)
        {
            return base.Calculate(localObserver, dateTime);
        }
    }
}
