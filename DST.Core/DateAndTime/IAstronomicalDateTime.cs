using DST.Core.Physics;

namespace DST.Core.DateAndTime
{
    public interface IAstronomicalDateTime : IBaseDateTime
    {
        long GetTicksFromEpoch();
        Angle GetEarthRotationAngle();
        Angle GetMeanSiderealTime();
        Angle GetSiderealTime();
        Angle GetEclipticLongitude();
        Angle GetMeanSolarLongitude();
        Angle GetMeanLunarLongitude();
        Angle GetMeanObliquity();
        Angle GetNutationInLongitude();
        Angle GetNutationInObliquity();
        Angle GetEquationOfEquinoxes();
        Angle GetEquationOfOrigins();
    }
}
