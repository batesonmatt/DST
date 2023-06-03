using DST.Core.DateAndTime;
using DST.Core.Observer;
using DST.Core.Physics;

namespace DST.Core.LocalHourAngleDateTime
{
    public interface ILocalHourAngleDateTime
    {
        IAstronomicalDateTime Calculate(
            ILocalObserver localObserver, IAstronomicalDateTime dateTime, Angle target, HourAngleCycle cycle);
    }
}
