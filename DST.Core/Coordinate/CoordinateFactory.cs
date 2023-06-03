using DST.Core.Components;
using DST.Core.Physics;

namespace DST.Core.Coordinate
{
    public class CoordinateFactory
    {
        // Creates a new IEclipticCoordinate object given the specified longitude and latitude Angle values.
        public static IEclipticCoordinate CreateEcliptic(Angle longitude, Angle latitude)
        {
            return new EclipticCoordinate(Builders.StandardComponents.Build(longitude, latitude));
        }

        // Creates a new IEquatorialCoordinate object given the specified right ascension and declination Angle values.
        public static IEquatorialCoordinate CreateEquatorial(Angle rightAscension, Angle declination)
        {
            return new EquatorialCoordinate(Builders.StandardComponents.Build(rightAscension, declination));
        }

        // Creates a new IGeographicCoordinate object given the specified longitude and latitude Angle values.
        public static IGeographicCoordinate CreateGeographic(Angle longitude, Angle latitude)
        {
            // The longitude should lie on (-180°, 180°], East or West of the Prime Meridian.
            return new GeographicCoordinate(Builders.ModifiedComponents.Build(longitude, latitude));
        }

        // Creates a new IHorizontalCoordinate object given the specified azimuth and altitude Angle values.
        // The argument for 'referencesIRM' indicates whether the azimuthal angle needs to be modified.
        public static IHorizontalCoordinate CreateHorizontal(Angle azimuth, Angle altitude, bool referencesIRM)
        {
            return referencesIRM switch
            {
                // The IRM azimuth should lie on (-180°, 180°], East or West of the Prime Meridian.
                true => new HorizontalIRMCoordinate(Builders.ModifiedComponents.Build(azimuth, altitude)),

                _ => new HorizontalCoordinate(Builders.StandardComponents.Build(azimuth, altitude))
            };
        }

        // Returns a new IFormattableCoordinate object with the same value of the specified ICoordinate object.
        public static IFormattableCoordinate ConvertToFormattable(ICoordinate coordinate)
        {
            _ = coordinate ?? throw new ArgumentNullException(nameof(coordinate));

            return coordinate switch
            {
                IFormattableCoordinate formattable => formattable,
                _ => throw new NotSupportedException($"{nameof(ICoordinate)} type '{coordinate.GetType()}' is not supported.")
            };
        }
    }
}
