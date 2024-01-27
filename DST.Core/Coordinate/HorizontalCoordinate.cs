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
            string altitude = Format(format, ComponentType.Inclination);
            string azimuth = Format(format, ComponentType.Rotation);

            return string.Format(Resources.AngleFormats.CoordinateFormatWithDelimiter, altitude, azimuth);
        }

        // Returns the string-representation of this HorizontalCoordinate formatted with the specified FormatType and ComponentType in the current culture.
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
                                    result = Azimuth.ToString(Angle.FormatType.ComponentDegrees, Angle.FormatModifierType.None);
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
                                    result = Azimuth.ToString(Angle.FormatType.DecimalDegrees, Angle.FormatModifierType.None);
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
                                    result = Azimuth.ToString(Angle.FormatType.CompactDegrees, Angle.FormatModifierType.None);
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
