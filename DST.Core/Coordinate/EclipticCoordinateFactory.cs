using DST.Core.Components;
using DST.Core.Physics;

namespace DST.Core.Coordinate
{
    public class EclipticCoordinateFactory
    {
        // Creates a new IEclipticCoordinate object given the specified longitude and latitude Angle values.
        public static IEclipticCoordinate Create(Angle longitude, Angle latitude)
        {
            return new EclipticCoordinate(Builders.StandardComponents.Build(longitude, latitude));
        }
    }
}
