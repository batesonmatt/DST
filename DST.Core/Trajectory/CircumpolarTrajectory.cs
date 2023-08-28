using DST.Core.DateAndTime;
using DST.Core.Observer;

namespace DST.Core.Trajectory
{
    // Represents a type of celestial trajectory in which the object is always visible relative to the observer's position,
    // in which the observer is located at either of the geographic poles.
    public class CircumpolarTrajectory : BaseTrajectory, ITrajectory, ICircumpolarTrajectory
    {
        // Creates a new CircumpolarTrajectory instance with the specified ILocalObserver argument.
        public CircumpolarTrajectory(ILocalObserver localObserver)
            : base(localObserver)
        { }

        // Returns a value that indicates whether the target is in the observer's local sky at the specified date and time.
        public override bool IsAboveHorizon(IAstronomicalDateTime dateTime)
        {
            return true;
        }

        // Returns a value that indicates whether the origin of this ICircumpolarTrajectory is offset from the observer's zenith.
        public bool IsOffset()
        {
            return false;
        }

        // Returns the string-representation of this CircumpolarTrajectory instance.
        public override string ToString()
        {
            return DST.Resources.DisplayText.TrajectoryCircumpolar;
        }
    }
}
