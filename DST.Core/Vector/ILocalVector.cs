using DST.Core.Coordinate;

namespace DST.Core.Vector
{
    public interface ILocalVector : IVector
    {
        IHorizontalCoordinate Position { get; }
    }
}