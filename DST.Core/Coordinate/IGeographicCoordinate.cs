using DST.Core.Physics;

namespace DST.Core.Coordinate
{
    public interface IGeographicCoordinate : ICoordinate
    {
        Angle Longitude { get; }
        Angle Latitude { get; }
        bool IsAxial();
    }
}
