using DST.Core.Observer;
using DST.Core.TimeKeeper;
using DST.Core.Physics;

namespace DST.Core.LocalTimeKeeper
{
    public abstract class BaseLocalTimeKeeper : ILocalTimeKeeper
    {
        // The underlying ITimeKeeper object for this BaseLocalTimeKeeper instance.
        protected readonly ITimeKeeper _timeKeeper;

        // Creates a new BaseLocalTimeKeeper instance given a specified ITimeKeeper argument.
        protected BaseLocalTimeKeeper(ITimeKeeper timeKeeper)
        {
            _timeKeeper = timeKeeper ?? throw new ArgumentNullException(nameof(timeKeeper));
        }

        // Returns the localized rotational angle at the specified AstronomicalDateTime value,
        // for the specified ILocalObserver argument.
        public virtual Angle Calculate(ILocalObserver localObserver, AstronomicalDateTime dateTime)
        {
            _ = localObserver ?? throw new ArgumentNullException(nameof(localObserver));

            // The original (base) rotational angle for the underlying ITimeKeeper.
            Angle original = _timeKeeper.Calculate(dateTime);

            // Convert the original angle to a local angle.
            return Calculate(localObserver, original);
        }

        // Returns the localized rotational angle from the specified original angle for the specified ILocalObserver.
        protected static Angle Calculate(ILocalObserver localObserver, Angle original)
        {
            _ = localObserver ?? throw new ArgumentNullException(nameof(localObserver));

            // If the observer is located at either of the geographic poles, then there is no longitude to add.
            if (localObserver.Location.IsAxial()) return original;

            // Adjust the original angle by adding the observer's longitudinal angle.
            Angle local = original + localObserver.Location.Longitude;

            // Relate the angle onto the interval [0°, 360°).
            return local.Coterminal();
        }
    }
}
