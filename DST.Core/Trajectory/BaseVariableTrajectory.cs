using DST.Core.Observer;
using DST.Core.Vector;
using DST.Core.Physics;

namespace DST.Core.Trajectory
{
    public abstract class BaseVariableTrajectory : BaseTrajectory, ITrajectory, IVariableTrajectory
    {
        // Creates a new BaseVariableTrajectory instance with the specified ILocalObserver argument.
        protected BaseVariableTrajectory(ILocalObserver localObserver)
            : base(localObserver)
        { }

        // Returns a value that indicates whether the target is in the observer's local sky at the specified date and time.
        public abstract override bool IsAboveHorizon(AstronomicalDateTime dateTime);

        // Returns a tracking of the target when it passes through the observer's meridian,
        // starting from the specified date/time.
        public abstract IVector GetApex(AstronomicalDateTime dateTime);

        // Returns a tracking of the target when it passes through the observer's meridian,
        // starting from the specified date/time, for a specified number of cycles in the underlying time scale.
        public IVector[] GetApex(AstronomicalDateTime start, int cycles)
        {
            AstronomicalDateTime[] dateTimes = GetDateTimes(start, cycles);

            IVector[] vectors = new IVector[dateTimes.Length];

            for (int i = 0; i < dateTimes.Length; i++)
            {
                vectors[i] = GetApex(dateTimes[i]);
            }

            return vectors;
        }
    }
}
