using DST.Core.Physics;

namespace DST.Core.TimeKeeper
{
    public class StellarTimeKeeper : ITimeKeeper
    {
        // Returns the Earth rotation angle (ERA) for the specified AstronomicalDateTime value.
        public Angle Calculate(AstronomicalDateTime dateTime)
        {
            return dateTime.GetEarthRotationAngle();
        }
    }
}
