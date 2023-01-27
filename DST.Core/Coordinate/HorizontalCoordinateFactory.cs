using DST.Core.Components;
using DST.Core.Physics;

namespace DST.Core.Coordinate
{
    public class HorizontalCoordinateFactory
    {
        // Creates a new IHorizontalCoordinate object given the specified azimuth and altitude Angle values.
        // The argument for 'referencesIRM' indicates whether the azimuthal angle needs to be modified.
        public static IHorizontalCoordinate Create(Angle azimuth, Angle altitude, bool referencesIRM)
        {
            return referencesIRM switch
            {
                // The IRM azimuth should lie on (-180°, 180°], East or West of the Prime Meridian.
                true => new HorizontalIRMCoordinate(Builders.ModifiedComponents.Build(azimuth, altitude)),

                _ => new HorizontalCoordinate(Builders.StandardComponents.Build(azimuth, altitude))
            };
        }
    }
}
