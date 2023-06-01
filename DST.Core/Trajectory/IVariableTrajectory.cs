using DST.Core.Vector;
using DST.Core.DateAndTime;

namespace DST.Core.Trajectory
{
    public interface IVariableTrajectory : ITrajectory
    {
        IVector GetApex(AstronomicalDateTime dateTime);
        IVector[] GetApex(AstronomicalDateTime start, int cycles);
    }
}
