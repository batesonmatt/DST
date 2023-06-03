using DST.Core.DateAndTime;
using DST.Core.Physics;

namespace DST.Core.Coordinate
{
    public interface IEclipticCoordinate : ICoordinate
    {
        Angle Longitude { get; }
        Angle Latitude { get; }
        IEclipticCoordinate GetNutation(IAstronomicalDateTime dateTime);
        IEquatorialCoordinate ToEquatorial(IAstronomicalDateTime dateTime);
    }
}
