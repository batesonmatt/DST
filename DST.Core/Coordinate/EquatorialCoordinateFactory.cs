using DST.Core.Components;
using DST.Core.Physics;

namespace DST.Core.Coordinate
{
    public class EquatorialCoordinateFactory
    {
        // Creates a new IEquatorialCoordinate object given the specified right ascension and declination Angle values.
        public static IEquatorialCoordinate Create(Angle rightAscension, Angle declination)
        {
            return new EquatorialCoordinate(Builders.StandardComponents.Build(rightAscension, declination));
        }
    }
}
