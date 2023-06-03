using DST.Core.DateAndTime;

namespace DST.Core.DateTimeAdder
{
    public interface IDateTimeAdder
    {
        int Min { get; }
        int Max { get; }
        IMutableDateTime Add(IMutableDateTime start, int value);
    }
}