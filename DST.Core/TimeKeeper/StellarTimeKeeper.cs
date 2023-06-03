using DST.Core.DateAndTime;
using DST.Core.Physics;

namespace DST.Core.TimeKeeper
{
    public class StellarTimeKeeper : ITimeKeeper
    {
        // Returns the Earth rotation angle (ERA) for the specified IAstronomicalDateTime object.
        public Angle Calculate(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            return dateTime.GetEarthRotationAngle();
        }
    }
}
