using DST.Core.DateAndTime;
using DST.Core.Observer;

namespace DST.Core.Trajectory
{
    // Represents a type of celestial trajectory in which the object is never visible relative to the observer's position.
    public class NeverRiseTrajectory : BaseTrajectory, ITrajectory
    {
        // Creates a new NeverRiseTrajectory instance with the specified ILocalObserver argument.
        public NeverRiseTrajectory(ILocalObserver localObserver)
            : base(localObserver)
        { }

        // Returns a value that indicates whether the target is in the observer's local sky at the specified date and time.
        public override bool IsAboveHorizon(IAstronomicalDateTime dateTime)
        {
            return false;
        }

        // Returns the string-representation of this NeverRiseTrajectory instance.
        public override string ToString()
        {
            return DST.Resources.DisplayText.TrajectoryNeverRise;
        }
    }
}
