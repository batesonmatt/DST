using DST.Core.DateAndTime;

namespace DST.Core.Trajectory
{
    public interface ITrajectory
    {
        bool IsAboveHorizon(AstronomicalDateTime dateTime);
    }
}
