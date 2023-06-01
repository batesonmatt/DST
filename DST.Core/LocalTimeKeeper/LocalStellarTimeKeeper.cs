using DST.Core.Observer;
using DST.Core.TimeKeeper;
using DST.Core.Physics;
using DST.Core.DateAndTime;

namespace DST.Core.LocalTimeKeeper
{
    public class LocalStellarTimeKeeper : BaseLocalTimeKeeper
    {
        // Creates a new LocalStellarTimeKeeper given a specified ITimeKeeper argument.
        public LocalStellarTimeKeeper(ITimeKeeper timeKeeper)
            : base(timeKeeper)
        { }

        // Returns the local Earth rotation angle (LERA) for the specified ILocalObserver and AstronomicalDateTime arguments.
        public override Angle Calculate(ILocalObserver localObserver, AstronomicalDateTime dateTime)
        {
            return base.Calculate(localObserver, dateTime);
        }
    }
}
