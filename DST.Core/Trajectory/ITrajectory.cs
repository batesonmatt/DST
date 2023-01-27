using DST.Core.Physics;

namespace DST.Core.Trajectory
{
    public interface ITrajectory
    {
        bool IsAboveHorizon(AstronomicalDateTime dateTime);
    }
}
