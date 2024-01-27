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
            string latitude = Format(format, ComponentType.Inclination);
            string longitude = Format(format, ComponentType.Rotation);

            return string.Format(Resources.AngleFormats.CoordinateFormatNoDelimiter, latitude, longitude);
        }

        // Returns the string-representation of this GeographicCoordinate formatted with the specified FormatType and ComponentType in the current culture.
        public override string Format(FormatType format, ComponentType component)
        {
            string result;

            switch (format)
            {
                case FormatType.Component:
                default:
                    {
                        switch (component)
                        {
                            case ComponentType.Rotation:
                                {
                                    result = Longitude.ToString(Angle.FormatType.ComponentDegrees, Angle.FormatModifierType.Longitude);
                                    break;
                                }
                            case ComponentType.Inclination:
                                {
                                    result = Latitude.ToString(Angle.FormatType.ComponentDegrees, Angle.FormatModifierType.Latitude);
                                    break;
                                }
                            default:
                                {
                                    result = string.Empty;
                                    break;
                                }
                        }

                        break;
                    }
                case FormatType.Decimal:
                    {
                        switch (component)
                        {
                            case ComponentType.Rotation:
                                {
                                    result = Longitude.ToString(Angle.FormatType.DecimalDegrees, Angle.FormatModifierType.Longitude);
                                    break;
                                }
                            case ComponentType.Inclination:
                                {
                                    result = Latitude.ToString(Angle.FormatType.DecimalDegrees, Angle.FormatModifierType.Latitude);
                                    break;
                                }
                            default:
                                {
                                    result = string.Empty;
                                    break;
                                }
                        }

                        break;
                    }
                case FormatType.Compact:
                    {
                        switch (component)
                        {
                            case ComponentType.Rotation:
                                {
                                    result = Longitude.ToString(Angle.FormatType.CompactDegrees, Angle.FormatModifierType.Longitude);
                                    break;
                                }
                            case ComponentType.Inclination:
                                {
                                    result = Latitude.ToString(Angle.FormatType.CompactDegrees, Angle.FormatModifierType.Latitude);
                                    break;
                                }
                            default:
                                {
                                    result = string.Empty;
                                    break;
                                }
                        }

                        break;
                    }
            }

            return result;
        }

        // Returns true when this GeographicCoordinate's latitudinal component is either -90° or 90°; false otherwise.
        public bool IsAxial()
        {
            return Math.Abs(Latitude) == 90.0;
        }
    }
}
