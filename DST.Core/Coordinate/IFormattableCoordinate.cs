using DST.Core.Components;

namespace DST.Core.Coordinate
{
    public interface IFormattableCoordinate
    {
        string Format();
        string Format(FormatType format);
        string Format(FormatType format, ComponentType component);
    }
}
