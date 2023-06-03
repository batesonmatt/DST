using DST.Core.DateAndTime;
using DST.Core.Physics;

namespace DST.Core.Coordinate
{
    public interface IEquatorialCoordinate : ICoordinate
    {
        Angle RightAscension { get; }
        Angle Declination { get; }
        IEquatorialCoordinate GetNutation(IAstronomicalDateTime dateTime);
        Angle GetIntermediateRightAscension(IAstronomicalDateTime dateTime);
        IEclipticCoordinate ToEcliptic(IAstronomicalDateTime dateTime);
    }
}
