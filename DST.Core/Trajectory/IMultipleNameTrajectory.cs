namespace DST.Core.Trajectory
{
    public interface IMultipleNameTrajectory : ITrajectory
    {
        string GetPrimaryName();
        string GetSecondaryName();
    }
}
