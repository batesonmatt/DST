using DST.Core.LocalHourAngleDateTime;
using DST.Core.Observer;
using DST.Core.Vector;
using DST.Core.Physics;
using DST.Core.DateAndTime;

namespace DST.Core.Trajectory
{
    // Represents a type of celestial trajectory in which the object is always visible relative to the observer's position,
    // in which the observer is not located at either of the geographic poles.
    public class CircumpolarOffsetTrajectory : BaseVariableTrajectory, ITrajectory, IVariableTrajectory
    {
        // Creates a new CircumpolarOffsetTrajectory instance with the specified ILocalObserver argument.
        public CircumpolarOffsetTrajectory(ILocalObserver localObserver)
            : base(localObserver)
        { }

        // Returns a value that indicates whether the target is in the observer's local sky at the specified date and time.
        public override bool IsAboveHorizon(IAstronomicalDateTime dateTime)
        {
            return true;
        }

        // Returns a tracking of the target when it passes through the observer's meridian,
        // starting from the specified date/time.
        public override IVector GetApex(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            return CalculateVector(dateTime, Angle.Zero, HourAngleCycle.Next);
        }

        // Returns the string-representation of this CircumpolarOffsetTrajectory instance.
        public override string ToString()
        {
            return Resources.DisplayText.TrajectoryCircumpolarOffset;
        }
    }
}
