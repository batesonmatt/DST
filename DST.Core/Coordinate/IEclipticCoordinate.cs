using DST.Core.DateAndTime;
using DST.Core.Physics;

namespace DST.Core.Coordinate
{
    public interface IEclipticCoordinate : ICoordinate
    {
        Angle Longitude { get; }
        Angle Latitude { get; }
        IEclipticCoordinate GetNutation(AstronomicalDateTime dateTime);
        IEquatorialCoordinate ToEquatorial(AstronomicalDateTime dateTime);
    }
}
