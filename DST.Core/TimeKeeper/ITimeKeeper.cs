using DST.Core.DateAndTime;
using DST.Core.Physics;

namespace DST.Core.TimeKeeper
{
    public interface ITimeKeeper
    {
        Angle Calculate(IAstronomicalDateTime dateTime);
    }
}
