using DST.Core.Components;

namespace DST.Core.Coordinate
{
    public interface ICoordinate : IFormattableCoordinate
    {
        IComponents Components { get; }
    }
}
