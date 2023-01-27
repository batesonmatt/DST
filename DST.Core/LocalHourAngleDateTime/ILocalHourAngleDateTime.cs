using DST.Core.Observer;
using DST.Core.Physics;

namespace DST.Core.LocalHourAngleDateTime
{
    public interface ILocalHourAngleDateTime
    {
        AstronomicalDateTime Calculate(
            ILocalObserver localObserver, AstronomicalDateTime dateTime, Angle target, HourAngleCycle cycle);
    }
}
