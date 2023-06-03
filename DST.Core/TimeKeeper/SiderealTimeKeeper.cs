using DST.Core.DateAndTime;
using DST.Core.Physics;

namespace DST.Core.TimeKeeper
{
    public class SiderealTimeKeeper : ITimeKeeper
    {
        // Returns the Greenwich apparent sidereal time (GAST/GST) for the specified IAstronomicalDateTime object.
        public Angle Calculate(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            return dateTime.GetSiderealTime();
        }
    }
}
