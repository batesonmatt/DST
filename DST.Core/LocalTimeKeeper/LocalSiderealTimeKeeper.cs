using DST.Core.Observer;
using DST.Core.TimeKeeper;
using DST.Core.Physics;
using DST.Core.DateAndTime;

namespace DST.Core.LocalTimeKeeper
{
    public class LocalSiderealTimeKeeper : BaseLocalTimeKeeper
    {
        // Creates a new LocalSiderealTimeKeeper given a specified ITimeKeeper argument.
        public LocalSiderealTimeKeeper(ITimeKeeper timeKeeper)
            : base(timeKeeper)
        { }

        // Returns the local apparent sidereal time (LAST/LST) for the specified ILocalObserver and IAstronomicalDateTime arguments.
        public override Angle Calculate(ILocalObserver localObserver, IAstronomicalDateTime dateTime)
        {
            return base.Calculate(localObserver, dateTime);
        }
    }
}
