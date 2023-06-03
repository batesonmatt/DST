using DST.Core.Coordinate;
using DST.Core.DateAndTime;

namespace DST.Core.Vector
{
    public class VectorFactory
    {
        // Returns a new IVector object given the specified IBaseDateTime and ICoordinate arguments.
        public static IVector Create(IBaseDateTime dateTime, ICoordinate coordinate)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
            _ = coordinate ?? throw new ArgumentNullException(nameof(coordinate));

            return coordinate switch
            {
                IHorizontalCoordinate position => new LocalVector(dateTime, position),
                _ => throw new NotSupportedException($"{nameof(ICoordinate)} type '{coordinate.GetType()}' is not supported.")
            };
        }
    }
}
