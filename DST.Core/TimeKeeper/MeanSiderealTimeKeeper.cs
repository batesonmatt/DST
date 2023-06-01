using DST.Core.DateAndTime;
using DST.Core.Physics;

namespace DST.Core.TimeKeeper
{
    public class MeanSiderealTimeKeeper : ITimeKeeper
    {
        // Returns the Greenwich mean sidereal time (GMST) for the specified AstronomicalDateTime value.
        public Angle Calculate(AstronomicalDateTime dateTime)
        {
            return dateTime.GetMeanSiderealTime();
        }
    }
}
