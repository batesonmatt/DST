using DST.Core.Components;

namespace DST.Core.Coordinate
{
    public abstract class BaseCoordinate : ICoordinate, IFormattableCoordinate
    {
        // Gets this BaseCoordinate's IComponents value.
        public IComponents Components { get; }

        // Creates a new BaseCoordinate given the specified IComponents object.
        protected BaseCoordinate(IComponents components)
        {
            Components = components ?? throw new ArgumentNullException(nameof(components));
        }

        // Returns the string-representation of this IFormattableCoordinate.
        public override string ToString()
        {
            return Format();
        }

        // Returns the string-representation of this IFormattableCoordinate, formatted by FormatType.Component.
        public string Format()
        {
            return Format(FormatType.Component);
        }

        // Returns the string-representation of this IFormattableCoordinate, formatted by the specified FormatType.
        public abstract string Format(FormatType format);

        // Returns the string-representation of this IFormattableCoordinate, formatted by the specified FormatType and ComponentType.
        public abstract string Format(FormatType format, ComponentType component);
    }
}
