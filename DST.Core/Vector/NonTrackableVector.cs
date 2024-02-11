using DST.Core.Coordinate;
using DST.Core.DateAndTime;

namespace DST.Core.Vector
{
    // Represents a non-observable position of a celestial object that was a result of failed tracking.
    // This class is not intended to be instantiated in public assemblies.
    public class NonTrackableVector : IVector
    {
        // Gets this NonTrackableVector's date/time value.
        public IMutableDateTime DateTime { get; }

        // Gets this NonTrackableVector's ICoordinate object.
        public ICoordinate Coordinate { get; }

        // Creates a new NonTrackableVector instance with the specified IMutableDateTime and NonTrackableCoordinate arguments.
        internal NonTrackableVector(IMutableDateTime dateTime, NonTrackableCoordinate coordinate)
        {
            DateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
            Coordinate = coordinate ?? throw new ArgumentNullException(nameof(coordinate));
        }
    }
}
