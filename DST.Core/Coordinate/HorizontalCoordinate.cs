using DST.Core.Components;
using DST.Core.Physics;

namespace DST.Core.Coordinate
{
    // Represents an astronomical spherical coordinate composed of altitude and azimuth.
    public class HorizontalCoordinate : BaseCoordinate, IHorizontalCoordinate
    {
        // Gets this HorizontalCoordinate's azimuthal component.
        public Angle Azimuth => Components.Rotation;

        // Gets this HorizontalCoordinate's altitudinal component.
        public Angle Altitude => Components.Inclination;

        // Creates a new HorizontalCoordinate given the specified IComponents object.
        public HorizontalCoordinate(IComponents components)
            : base(components)
        { }

        // Returns the string-representation of this HorizontalCoordinate formatted with the specified FormatType in the current culture.
        public override string Format(FormatType format)
        {
            string altitude;
            string azimuth;

            switch (format)
            {
                case FormatType.Component:
                default:
                    {
                        altitude = Altitude.ToString(Angle.FormatType.ComponentDegrees, Angle.FormatModifierType.Signed);
                        azimuth = Azimuth.ToString(Angle.FormatType.ComponentDegrees, Angle.FormatModifierType.None);

                        break;
                    }
                case FormatType.Decimal:
                    {
                        altitude = Altitude.ToString(Angle.FormatType.DecimalDegrees, Angle.FormatModifierType.Signed);
                        azimuth = Azimuth.ToString(Angle.FormatType.DecimalDegrees, Angle.FormatModifierType.None);

                        break;
                    }
                case FormatType.Compact:
                    {
                        altitude = Altitude.ToString(Angle.FormatType.CompactDegrees, Angle.FormatModifierType.Signed);
                        azimuth = Azimuth.ToString(Angle.FormatType.CompactDegrees, Angle.FormatModifierType.None);

                        break;
                    }
            }

            return string.Format(Resources.AngleStringFormats.CoordinateFormatWithDelimiter, altitude, azimuth);
        }
    }
}
