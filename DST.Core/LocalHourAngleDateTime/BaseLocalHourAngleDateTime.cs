using DST.Core.DateAndTime;
using DST.Core.LocalHourAngle;
using DST.Core.Observer;
using DST.Core.Physics;

namespace DST.Core.LocalHourAngleDateTime
{
    public abstract class BaseLocalHourAngleDateTime : ILocalHourAngleDateTime
    {
        // The underlying ILocalHourAngle object for this BaseLocalHourAngleDateTime instance.
        protected readonly ILocalHourAngle _localHourAngle;

        // Creates a new BaseLocalHourAngleDateTime instance given the specified ILocalHourAngle argument.
        protected BaseLocalHourAngleDateTime(ILocalHourAngle localHourAngle)
        {
            _localHourAngle = localHourAngle ?? throw new ArgumentNullException(nameof(localHourAngle));
        }

        // Returns an IAstronomicalDateTime representing the date and time when the specified ILocalObserver.Target
        // reaches the specified target local hour angle (LHA) as observed in standardized local time, beginning from
        // the specified starting IAstronomicalDateTime.
        // If 'cycle' is HourAngleCycle.Next, this returns the next date/time that the targeting object reaches
        // the target LHA, otherwise this returns the previous date/time that the targeting object reached the target LHA.
        public abstract IAstronomicalDateTime Calculate(
            ILocalObserver localObserver, IAstronomicalDateTime dateTime, Angle target, HourAngleCycle cycle);
    }
}
