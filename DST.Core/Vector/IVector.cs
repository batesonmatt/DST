using DST.Core.Coordinate;
using DST.Core.DateAndTime;

namespace DST.Core.Vector
{
    public interface IVector
    {
        IMutableDateTime DateTime { get; }
        ICoordinate Coordinate { get; }
    }
}
