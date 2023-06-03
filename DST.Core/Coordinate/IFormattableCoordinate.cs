namespace DST.Core.Coordinate
{
    public interface IFormattableCoordinate
    {
        string Format();
        string Format(FormatType format);
    }
}
