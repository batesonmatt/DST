using DST.Core.Vector;
using DST.Core.DateAndTime;

namespace DST.Core.Trajectory
{
    public interface IRiseSetTrajectory : ITrajectory, IVariableTrajectory
    {
        bool IsRising(AstronomicalDateTime dateTime);
        bool IsSetting(AstronomicalDateTime dateTime);
        IVector GetRise(AstronomicalDateTime dateTime);
        IVector GetSet(AstronomicalDateTime dateTime);
        IVector[] GetRise(AstronomicalDateTime start, int cycles);
        IVector[] GetSet(AstronomicalDateTime start, int cycles);
    }
}
