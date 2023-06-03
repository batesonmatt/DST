namespace DST.Core.DateAndTime
{
    public interface IDateTime : IBaseDateTime
    {
        DateTimeKind Kind { get; }
        IDateTime Date { get; }
        TimeSpan Time { get; }
        TimeSpan UtcOffset { get; }
    }
}