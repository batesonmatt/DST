using DST.Core.Components;
using DST.Core.DateAndTime;
using DST.Core.Physics;

namespace DST.Core.Coordinate
{
    // Represents an astronomical spherical coordinate composed of ecliptic longitude and ecliptic latitude.
    public class EclipticCoordinate : BaseCoordinate, IEclipticCoordinate
    {
        // Gets this EclipticCoordinate's longitudinal component.
        public Angle Longitude => Components.Rotation;

        // Gets this EclipticCoordinate's latitudinal component.
        public Angle Latitude => Components.Inclination;

        // Creates a new EclipticCoordinate given the specified IComponents object.
        public EclipticCoordinate(IComponents components)
            : base(components)
        { }

        // Returns the string-representation of this EclipticCoordinate formatted with the specified FormatType in the current culture.
        public override string Format(FormatType format)
        {
            string longitude;
            string latitude;

            switch (format)
            {
                case FormatType.Component:
                default:
                    {
                        longitude = Longitude.ToString(Angle.FormatType.ComponentDegrees, Angle.FormatModifierType.None);
                        latitude = Latitude.ToString(Angle.FormatType.ComponentDegrees, Angle.FormatModifierType.Signed);

                        break;
                    }
                case FormatType.Decimal:
                    {
                        longitude = Longitude.ToString(Angle.FormatType.DecimalDegrees, Angle.FormatModifierType.None);
                        latitude = Latitude.ToString(Angle.FormatType.DecimalDegrees, Angle.FormatModifierType.Signed);

                        break;
                    }
                case FormatType.Compact:
                    {
                        longitude = Longitude.ToString(Angle.FormatType.CompactDegrees, Angle.FormatModifierType.None);
                        latitude = Latitude.ToString(Angle.FormatType.CompactDegrees, Angle.FormatModifierType.Signed);

                        break;
                    }
            }

            return string.Format(Resources.AngleStringFormats.CoordinateFormatWithDelimiter, longitude, latitude);
        }

        // Returns the nutated components of this EclipticCoordinate, for the specified date and time.
        public IEclipticCoordinate GetNutation(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            // Get a signed coefficient for the nutation quantities.
            // If the specified date/time occurs before the epoch, this gives -1.
            // If the specified date/time occurs after the epoch, this gives 1.
            // If the specified date/time is equal to the epoch, this gives 0.
            int direction = Math.Sign(dateTime.GetTicksFromEpoch());

            // Get the nutation in longitude, in degrees, for the specified date and time, factored by the direction.
            double dPsi = direction * dateTime.GetNutationInLongitude();

            // Add the nutation in longitude to the ecliptic longitude, and restrict onto [0°, 360°).
            Angle dLambda = Angle.Coterminal(Longitude + dPsi);

            // Modify the original ecliptic coordinate with the new longitude.
            return EclipticCoordinateFactory.Create(dLambda, Latitude);
        }

        // Returns the equatorial-representation of this EclipticCoordinate, at the specified date and time.
        public IEquatorialCoordinate ToEquatorial(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            // Convert the initial components of this equatorial coordinate to radians.
            double lambda = Longitude.TotalRadians;
            double beta = Latitude.TotalRadians;

            // Get the mean obliquity, in radians, for the specified date and time.
            double epsilon = dateTime.GetMeanObliquity().TotalRadians;

            // Solve for the equatorial right ascension, alpha, in radians.
            double alpha = Math.Atan2(
                Math.Sin(lambda) * Math.Cos(epsilon) -
                Math.Tan(beta) * Math.Sin(epsilon),
                Math.Cos(lambda));

            // Solve for the equatorial declination, delta, in radians.
            double delta = Math.Asin(
                Math.Sin(beta) * Math.Cos(epsilon) +
                Math.Cos(beta) * Math.Sin(epsilon) *
                Math.Sin(lambda));

            // Convert the equatorial components to degrees.
            Angle rightAscension = Angle.FromRadians(alpha);
            Angle declination = Angle.FromRadians(delta);

            return EquatorialCoordinateFactory.Create(rightAscension, declination);
        }
    }
}
