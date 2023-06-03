using DST.Core.Coordinate;
using DST.Core.DateAndTime;

namespace DST.Core.Vector
{
    public interface IVector
    {
        IBaseDateTime DateTime { get; }
        ICoordinate Coordinate { get; }
    }
}
