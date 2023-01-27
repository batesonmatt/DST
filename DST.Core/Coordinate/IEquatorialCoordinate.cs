using DST.Core.Physics;

namespace DST.Core.Coordinate
{
    public interface IEquatorialCoordinate : ICoordinate
    {
        Angle RightAscension { get; }
        Angle Declination { get; }
        IEquatorialCoordinate GetNutation(AstronomicalDateTime dateTime);
        Angle GetIntermediateRightAscension(AstronomicalDateTime dateTime);
        IEclipticCoordinate ToEcliptic(AstronomicalDateTime dateTime);
    }
}
