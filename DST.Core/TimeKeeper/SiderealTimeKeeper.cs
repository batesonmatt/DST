using DST.Core.DateAndTime;
using DST.Core.Physics;

namespace DST.Core.TimeKeeper
{
    public class SiderealTimeKeeper : ITimeKeeper
    {
        // Returns the Greenwich apparent sidereal time (GAST/GST) for the specified AstronomicalDateTime value.
        public Angle Calculate(AstronomicalDateTime dateTime)
        {
            return dateTime.GetSiderealTime();
        }
    }
}
