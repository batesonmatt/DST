using DST.Core.DateAndTime;

namespace DST.Core.DateTimesBuilder
{
    public interface IDateTimesBuilder
    {
        IBaseDateTime[] Build(IBaseDateTime start, int period, int interval);
    }
}
