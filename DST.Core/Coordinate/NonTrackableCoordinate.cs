using DST.Core.Components;

namespace DST.Core.Coordinate
{
    // Represents an astronomical spherical coordinate that was a result of failed tracking.
    // This class is not intended to be instantiated in public assemblies.
    public class NonTrackableCoordinate : BaseCoordinate
    {
        // Creates a new NonTrackableCoordinate with default components.
        internal NonTrackableCoordinate()
            : this(ComponentsFactory.Create())
        { }

        // Creates a new NonTrackableCoordinate given the specified IComponents object.
        internal NonTrackableCoordinate(IComponents components)
            : base(components)
        { }

        // Returns the string-representation of this NonTrackableCoordinate formatted with the specified FormatType in the current culture.
        public override string Format(FormatType format)
        {
            return Resources.AngleFormats.Unown;
        }

        // Returns the string-representation of this NonTrackableCoordinate formatted with the specified FormatType and ComponentType in the current culture.
        public override string Format(FormatType format, ComponentType component)
        {
            return Resources.AngleFormats.Unown;
        }
    }
}
