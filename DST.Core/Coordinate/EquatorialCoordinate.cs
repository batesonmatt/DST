using DST.Core.Components;
using DST.Core.DateAndTime;
using DST.Core.Physics;

namespace DST.Core.Coordinate
{
    // Represents an astronomical spherical coordinate composed of right ascension and declination.
    public class EquatorialCoordinate : BaseCoordinate, IEquatorialCoordinate
    {
        // Gets this EquatorialCoordinate's right ascension component.
        public Angle RightAscension => Components.Rotation;

        // Gets this EquatorialCoordinate's declination component.
        public Angle Declination => Components.Inclination;

        // Creates a new EquatorialCoordinate given the specified IComponents object.
        public EquatorialCoordinate(IComponents components)
            : base(components)
        { }

        // Returns the string-representation of this EquatorialCoordinate formatted with the specified FormatType in the current culture.
        public override string Format(FormatType format)
        {
            string rightAscension;
            string declination;

            switch (format)
            {
                case FormatType.Component:
                default:
                    {
                        rightAscension = RightAscension.ToString(Angle.FormatType.ComponentHours, Angle.FormatModifierType.None);
                        declination = Declination.ToString(Angle.FormatType.ComponentDegrees, Angle.FormatModifierType.Signed);

                        break;
                    }
                case FormatType.Decimal:
                    {
                        rightAscension = RightAscension.ToString(Angle.FormatType.DecimalHours, Angle.FormatModifierType.None);
                        declination = Declination.ToString(Angle.FormatType.DecimalDegrees, Angle.FormatModifierType.Signed);

                        break;
                    }
                case FormatType.Compact:
                    {
                        rightAscension = RightAscension.ToString(Angle.FormatType.CompactHours, Angle.FormatModifierType.None);
                        declination = Declination.ToString(Angle.FormatType.CompactDegrees, Angle.FormatModifierType.Signed);

                        break;
                    }
            }

            return string.Format(Resources.AngleStringFormats.CoordinateFormatWithDelimiter, rightAscension, declination);
        }

        // Returns the intermediate right ascension of this EquatorialCoordinate, adjusted by the Celestial Intermediate Origin (CIO).
        public Angle GetIntermediateRightAscension(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            // Calculate the CIO-based (intermediate) right ascension, in decimal degrees, using the Equation of the Origins (EO).
            Angle ra_i = RightAscension + dateTime.GetEquationOfOrigins();

            // Relate the new right ascension onto the interval [0°, 360°).
            return ra_i.Coterminal();
        }

        // Returns the nutated components of this EquatorialCoordinate, for the specified date and time.
        public IEquatorialCoordinate GetNutation(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            // If this equatorial coordinate is close to either of the celestial poles, convert it to an ecliptic coordinate.
            IEquatorialCoordinate result = Math.Abs(Declination) > 85.0 ?
                CalculateEclipticNutation(dateTime) : CalculateNutation(dateTime);

            return result;
        }

        // Returns the ecliptic-representation of this EquatorialCoordinate, at the specified date and time.
        public IEclipticCoordinate ToEcliptic(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            // Convert the components of this equatorial coordinate to radians.
            double alpha = RightAscension.TotalRadians;
            double delta = Declination.TotalRadians;

            // Get the mean obliquity, in radians, for the specified date and time.
            double epsilon = dateTime.GetMeanObliquity().TotalRadians;

            // Solve for the geocentric ecliptic longitude, lambda, in radians.
            double lambda = Math.Atan2(Math.Sin(alpha) * Math.Cos(epsilon) + Math.Tan(delta) * Math.Sin(epsilon), Math.Cos(alpha));

            // Solve for the geocentric ecliptic latitude, beta, in radians.
            double beta = Math.Asin(Math.Sin(delta) * Math.Cos(epsilon) - Math.Cos(delta) * Math.Sin(epsilon) * Math.Sin(alpha));

            // Convert the ecliptic components to degrees.
            Angle longitude = Angle.FromRadians(lambda);
            Angle latitude = Angle.FromRadians(beta);

            return CoordinateFactory.CreateEcliptic(longitude, latitude);
        }

        // Calculates the nutation of this EquatorialCoordinate, for the specified date and time.
        private IEquatorialCoordinate CalculateNutation(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            // Get a signed coefficient for the nutation quantities.
            // If the specified date/time occurs before the epoch, this gives -1.
            // If the specified date/time occurs after the epoch, this gives 1.
            // If the specified date/time is equal to the epoch, this gives 0.
            int direction = Math.Sign(dateTime.GetTicksFromEpoch());

            // Get the nutation in longitude, in degrees, for the specified date and time, factored by the direction.
            double dPsi = direction * dateTime.GetNutationInLongitude();

            // Convert the initial components of this equatorial coordinate to radians.
            double alpha = RightAscension.TotalRadians;
            double delta = Declination.TotalRadians;

            // Get the mean obliquity, in radians, for the specified date and time.
            double epsilon = dateTime.GetMeanObliquity().TotalRadians;

            // Get the nutation in obliquity, in degrees, for the specified date and time, factored by the direction.
            double dEps = direction * dateTime.GetNutationInObliquity();

            // Solve for the nutation in right ascension, expressed in degrees.
            double dAlpha = Math.Cos(epsilon) + Math.Sin(epsilon) * Math.Sin(alpha) * Math.Tan(delta) * dPsi - Math.Cos(alpha) * Math.Tan(delta) * dEps;

            // Solve for the nutation in declination, expressed in degrees.
            double dDelta = Math.Cos(alpha) * Math.Sin(epsilon) * dPsi + Math.Sin(alpha) * dEps;

            // Add the nutation to the original equatorial components.
            Angle ra = new(RightAscension + dAlpha);
            Angle dec = new(Declination + dDelta);

            // The final nutated equatorial components, in degrees.
            return CoordinateFactory.CreateEquatorial(ra, dec);
        }

        // Calculates the nutation of this EquatorialCoordinate as an IEclipticCoordinate, for the specified date and time.
        private IEquatorialCoordinate CalculateEclipticNutation(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            // Convert this equatorial coordinate to an ecliptic coordinate at the specified date and time.
            IEclipticCoordinate ecliptic = ToEcliptic(dateTime);

            // Get the nutation for the ecliptic coordinate.
            ecliptic = ecliptic.GetNutation(dateTime);

            // Convert the ecliptic coordinate back to an equatorial coordinate.
            return ecliptic.ToEquatorial(dateTime);
        }
    }
}
