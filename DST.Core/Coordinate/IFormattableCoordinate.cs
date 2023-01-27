namespace DST.Core.Coordinate
{
    public interface IFormattableCoordinate : ICoordinate
    {
        string Format();
        string Format(FormatType format);
    }
}
