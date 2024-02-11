using DST.Core.Coordinate;
using DST.Core.DateAndTime;

namespace DST.Core.Vector
{
    public class VectorFactory
    {
        // Returns a new IVector object given the specified IMutableDateTime and ICoordinate arguments.
        public static IVector Create(IMutableDateTime dateTime, ICoordinate coordinate)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
            _ = coordinate ?? throw new ArgumentNullException(nameof(coordinate));

            return coordinate switch
            {
                IHorizontalCoordinate position => CreateLocal(dateTime, position),
                NonTrackableCoordinate nonTrackable => CreateNonTrackable(dateTime, nonTrackable),
                _ => throw new NotSupportedException($"{nameof(ICoordinate)} type '{coordinate.GetType()}' is not supported.")
            };
        }

        // Returns a new ILocalVector object given the specified IMutableDateTime and IHorizontalCoordinate arguments.
        private static ILocalVector CreateLocal(IMutableDateTime dateTime, IHorizontalCoordinate coordinate)
        {
            return new LocalVector(dateTime, coordinate);
        }

        // Returns a new ILocalVector object given the specified IMutableDateTime and NonTrackableCoordinate arguments.
        private static NonTrackableVector CreateNonTrackable(IMutableDateTime dateTime, NonTrackableCoordinate coordinate)
        {
            return new NonTrackableVector(dateTime, coordinate);
        }
    }
}
