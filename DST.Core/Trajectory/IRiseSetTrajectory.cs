using DST.Core.Vector;
using DST.Core.DateAndTime;

namespace DST.Core.Trajectory
{
    public interface IRiseSetTrajectory : ITrajectory, IVariableTrajectory
    {
        bool IsRising(IAstronomicalDateTime dateTime);
        bool IsSetting(IAstronomicalDateTime dateTime);
        IVector GetRise(IAstronomicalDateTime dateTime);
        IVector GetSet(IAstronomicalDateTime dateTime);
        IVector[] GetRise(IAstronomicalDateTime start, int cycles);
        IVector[] GetSet(IAstronomicalDateTime start, int cycles);
    }
}
