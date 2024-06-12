using DST.Core.DateAndTime;
using DST.Core.TimeScalable;

namespace DST.Core.Trajectory
{
    public interface ITrajectory
    {
        TimeScale GetTimeScale();
        bool IsAboveHorizon(IAstronomicalDateTime dateTime);
    }
}
