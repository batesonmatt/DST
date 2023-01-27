using DST.Core.Coordinate;
using DST.Core.Physics;

namespace DST.Core.Vector
{
    public interface IVector
    {
        AstronomicalDateTime DateTime { get; }
        ICoordinate Coordinate { get; }
    }
}
