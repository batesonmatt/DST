using DST.Core.Components;
using DST.Core.Physics;

namespace DST.Core.Coordinate
{
    public class GeographicCoordinateFactory
    {
        [Obsolete("Use CoordinateFactory.CreateGeographic instead.")]
        // Creates a new IGeographicCoordinate object given the specified longitude and latitude Angle values.
        public static IGeographicCoordinate Create(Angle longitude, Angle latitude)
        {
            // The longitude should lie on (-180°, 180°], East or West of the Prime Meridian.
            return new GeographicCoordinate(Builders.ModifiedComponents.Build(longitude, latitude));
        }
    }
}
