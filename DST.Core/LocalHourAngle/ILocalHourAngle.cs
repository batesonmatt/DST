using DST.Core.Observer;
using DST.Core.Physics;

namespace DST.Core.LocalHourAngle
{
    public interface ILocalHourAngle
    {
        Angle Calculate(ILocalObserver localObserver, AstronomicalDateTime dateTime);
    }
}
