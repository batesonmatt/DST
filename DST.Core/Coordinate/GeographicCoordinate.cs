using DST.Core.Components;
using DST.Core.Physics;

namespace DST.Core.Coordinate
{
    // Represents a geographic coordinate composed of longitude and latitude.
    public class GeographicCoordinate : BaseCoordinate, IGeographicCoordinate
    {
        // Gets this GeographicCoordinate's longitudinal component.
        public Angle Longitude => Components.Rotation;

        // Gets this GeographicCoordinate's latitudinal component.
        public Angle Latitude => Components.Inclination;

        // Creates a new GeographicCoordinate given the specified IComponents object.
        public GeographicCoordinate(IComponents components)
            : base(components)
        { }

        // Returns the string-representation of this GeographicCoordinate formatted with the specified FormatType in the current culture.
        public override string Format(FormatType format)
        {
            string latitude;
            string longitude;

            switch (format)
            {
                case FormatType.Component:
                default:
                    {
                        latitude = Latitude.ToString(Angle.FormatType.ComponentDegrees, Angle.FormatModifierType.Latitude);
                        longitude = Longitude.ToString(Angle.FormatType.ComponentDegrees, Angle.FormatModifierType.Longitude);

                        break;
                    }
                case FormatType.Decimal:
                    {
                        latitude = Latitude.ToString(Angle.FormatType.DecimalDegrees, Angle.FormatModifierType.Latitude);
                        longitude = Longitude.ToString(Angle.FormatType.DecimalDegrees, Angle.FormatModifierType.Longitude);

                        break;
                    }
                case FormatType.Compact:
                    {
                        latitude = Latitude.ToString(Angle.FormatType.CompactDegrees, Angle.FormatModifierType.Latitude);
                        longitude = Longitude.ToString(Angle.FormatType.CompactDegrees, Angle.FormatModifierType.Longitude);

                        break;
                    }
            }

            return string.Format(Resources.AngleStringFormats.CoordinateFormatNoDelimiter, latitude, longitude);
        }

        // Returns true when this GeographicCoordinate's latitudinal component is either -90° or 90°; false otherwise.
        public bool IsAxial()
        {
            return Math.Abs(Latitude) == 90.0;
        }
    }
}
