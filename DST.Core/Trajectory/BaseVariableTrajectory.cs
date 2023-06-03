using DST.Core.Observer;
using DST.Core.Vector;
using DST.Core.DateAndTime;

namespace DST.Core.Trajectory
{
    public abstract class BaseVariableTrajectory : BaseTrajectory, ITrajectory, IVariableTrajectory
    {
        // Creates a new BaseVariableTrajectory instance with the specified ILocalObserver argument.
        protected BaseVariableTrajectory(ILocalObserver localObserver)
            : base(localObserver)
        { }

        // Returns a value that indicates whether the target is in the observer's local sky at the specified date and time.
        public abstract override bool IsAboveHorizon(IAstronomicalDateTime dateTime);

        // Returns a tracking of the target when it passes through the observer's meridian,
        // starting from the specified date/time.
        public abstract IVector GetApex(IAstronomicalDateTime dateTime);

        // Returns a tracking of the target when it passes through the observer's meridian,
        // starting from the specified date/time, for a specified number of cycles in the underlying time scale.
        public IVector[] GetApex(IAstronomicalDateTime start, int cycles)
        {
            _ = start ?? throw new ArgumentNullException(nameof(start));

            IAstronomicalDateTime[] dateTimes = GetDateTimes(start, cycles);

            IVector[] vectors = new IVector[dateTimes.Length];

            for (int i = 0; i < dateTimes.Length; i++)
            {
                vectors[i] = GetApex(dateTimes[i]);
            }

            return vectors;
        }
    }
}
