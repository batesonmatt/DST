using DST.Core.Physics;

namespace DST.Core.DateTimesBuilder
{
    public interface IDateTimesBuilder
    {
        AstronomicalDateTime[] Build(AstronomicalDateTime start, int period, int interval);
    }
}
