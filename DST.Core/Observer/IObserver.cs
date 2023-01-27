using DST.Core.Coordinate;

namespace DST.Core.Observer
{
    public interface IObserver
    {
        ICoordinate Origin { get; }
        ICoordinate Destination { get; }
    }
}
