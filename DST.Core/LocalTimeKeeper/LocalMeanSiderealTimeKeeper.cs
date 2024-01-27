using DST.Core.Observer;
using DST.Core.TimeKeeper;
using DST.Core.Physics;
using DST.Core.DateAndTime;

namespace DST.Core.LocalTimeKeeper
{
    public class LocalMeanSiderealTimeKeeper : BaseLocalTimeKeeper
    {
        // Creates a new LocalMeanSiderealTimeKeeper given a specified ITimeKeeper argument.
        public LocalMeanSiderealTimeKeeper(ITimeKeeper timeKeeper)
            : base(timeKeeper)
        { }

        // Returns the local mean sidereal time (LMST) for the specified ILocalObserver and IAstronomicalDateTime arguments.
        public override Angle Calculate(ILocalObserver localObserver, IAstronomicalDateTime dateTime)
        {
            return base.Calculate(localObserver, dateTime);
        }

        // Returns the string-representation of this LocalMeanSiderealTimeKeeper instance.
        public override string ToString()
        {
            return Resources.DisplayText.AlgorithmLMSTFull;
        }
    }
}
