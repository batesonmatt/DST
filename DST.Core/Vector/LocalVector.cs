using DST.Core.Coordinate;
using DST.Core.DateAndTime;

namespace DST.Core.Vector
{
    // Represents the observable position of a celestial object at some point in time.
    public class LocalVector : IVector, ILocalVector
    {
        protected readonly IHorizontalCoordinate _position;

        // Gets this LocalVector's date/time value.
        public IMutableDateTime DateTime { get; }

        // Gets this LocalVector's ICoordinate object.
        public ICoordinate Coordinate => _position;

        // Gets this LocalVector's observable position.
        public IHorizontalCoordinate Position => _position;

        // Creates a new LocalVector instance with the specified IMutableDateTime and IHorizontalCoordinate arguments.
        public LocalVector(IMutableDateTime dateTime, IHorizontalCoordinate position)
        {
            DateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
            _position = position ?? throw new ArgumentNullException(nameof(position));
        }
    }
}
