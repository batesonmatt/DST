using DST.Core.DateAndTime;
using DST.Core.Physics;

namespace DST.Core.TimeKeeper
{
    public class MeanSiderealTimeKeeper : ITimeKeeper
    {
        // Returns the Greenwich mean sidereal time (GMST) for the specified IAstronomicalDateTime object.
        public Angle Calculate(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            return dateTime.GetMeanSiderealTime();
        }

        // Returns the string-representation of this MeanSiderealTimeKeeper instance.
        public override string ToString()
        {
            return Resources.DisplayText.AlgorithmGMSTFull;
        }
    }
}
