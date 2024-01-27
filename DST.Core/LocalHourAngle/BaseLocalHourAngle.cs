using DST.Core.Physics;
using DST.Core.LocalTimeKeeper;
using DST.Core.Observer;
using DST.Core.DateAndTime;

namespace DST.Core.LocalHourAngle
{
    public abstract class BaseLocalHourAngle : ILocalHourAngle
    {
        // The underlying ILocalTimeKeeper object for this BaseLocalHourAngle instance.
        protected readonly ILocalTimeKeeper _localTimeKeeper;

        // Creates a new BaseLocalHourAngle instance given the specified ILocalTimeKeeper argument.
        protected BaseLocalHourAngle(ILocalTimeKeeper localTimeKeeper)
        {
            _localTimeKeeper = localTimeKeeper ?? throw new ArgumentNullException(nameof(localTimeKeeper));
        }

        // Returns the local hour angle (LHA) for the specified ILocalObserver and IAstronomicalDateTime arguments.
        public virtual Angle Calculate(ILocalObserver localObserver, IAstronomicalDateTime dateTime)
        {
            _ = localObserver ?? throw new ArgumentNullException(nameof(localObserver));
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            // Calculate the local rotation time.
            Angle localTime = _localTimeKeeper.Calculate(localObserver, dateTime);

            // Get the appropriate right ascension for the observer's target.
            Angle alpha = localObserver is IVariableRightAscension variableObserver
                ? variableObserver.GetRightAscension(dateTime)
                : localObserver.Target.RightAscension;

            // Calculate the LHA.
            return Calculate(localTime, alpha);
        }

        // Returns the local hour angle (LHA) given the local rotation angle and the target's right ascension.
        protected static Angle Calculate(Angle localTime, Angle rightAscension)
        {
            return Angle.Coterminal(localTime - rightAscension);
        }

        // Returns the string-representation of this BaseLocalHourAngle instance.
        public override string ToString()
        {
            return Resources.DisplayText.AlgorithmLHAFull;
        }
    }
}
