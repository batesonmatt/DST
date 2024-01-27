using DST.Core.Components;
using DST.Core.Physics;

namespace DST.Core.Coordinate
{
    // Represents a type of Horizontal coordinate in which the azimuthal angle references the
    // IERS Reference Meridian (IRM) at 0° longitude as its initial side.
    // An instance of this type is useful when positioned at either of the geographic poles,
    // where a traditional azimuth is undefined.
    public class HorizontalIRMCoordinate : BaseCoordinate, IHorizontalCoordinate
    {
        // Gets this HorizontalIRMCoordinate's azimuthal component.
        public Angle Azimuth => Components.Rotation;

        // Gets this HorizontalIRMCoordinate's altitudinal component.
        public Angle Altitude => Components.Inclination;

        // Creates a new HorizontalIRMCoordinate given the specified IComponents object.
        public HorizontalIRMCoordinate(IComponents components)
            : base(components)
        { }

        // Returns the string-representation of this HorizontalIRMCoordinate formatted with the specified FormatType in the current culture.
        public override string Format(FormatType format)
        {
            string altitude = Format(format, ComponentType.Inclination);
            string azimuth = Format(format, ComponentType.Rotation);

            return string.Format(Resources.AngleFormats.CoordinateFormatWithDelimiter, altitude, azimuth);
        }

        // Returns the string-representation of this HorizontalIRMCoordinate formatted with the specified FormatType and ComponentType in the current culture.
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
                                    result = Azimuth.ToString(Angle.FormatType.ComponentDegrees, Angle.FormatModifierType.Longitude);
                                    break;
                                }
                            case ComponentType.Inclination:
                                {
                                    result = Altitude.ToString(Angle.FormatType.ComponentDegrees, Angle.FormatModifierType.Signed);
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
                                    result = Azimuth.ToString(Angle.FormatType.DecimalDegrees, Angle.FormatModifierType.Longitude);
                                    break;
                                }
                            case ComponentType.Inclination:
                                {
                                    result = Altitude.ToString(Angle.FormatType.DecimalDegrees, Angle.FormatModifierType.Signed);
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
                                    result = Azimuth.ToString(Angle.FormatType.CompactDegrees, Angle.FormatModifierType.Longitude);
                                    break;
                                }
                            case ComponentType.Inclination:
                                {
                                    result = Altitude.ToString(Angle.FormatType.CompactDegrees, Angle.FormatModifierType.Signed);
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
    }
}
