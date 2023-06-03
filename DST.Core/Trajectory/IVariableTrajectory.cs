using DST.Core.Vector;
using DST.Core.DateAndTime;

namespace DST.Core.Trajectory
{
    public interface IVariableTrajectory : ITrajectory
    {
        IVector GetApex(IAstronomicalDateTime dateTime);
        IVector[] GetApex(IAstronomicalDateTime start, int cycles);
    }
}
