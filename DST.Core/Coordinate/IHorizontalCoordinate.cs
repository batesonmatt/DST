using DST.Core.Physics;

namespace DST.Core.Coordinate
{
    public interface IHorizontalCoordinate : ICoordinate
    {
        Angle Azimuth { get; }
        Angle Altitude { get; }
    }
}
