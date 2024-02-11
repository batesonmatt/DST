using DST.Core.DateAndTime;
using DST.Core.LocalHourAngle;
using DST.Core.Observer;
using DST.Core.Physics;
using System.Diagnostics;

namespace DST.Core.LocalHourAngleDateTime
{
    public class LocalStellarHourAngleDateTime : BaseLocalHourAngleDateTime
    {
        // Creates a new LocalSiderealHourAngleDateTime instance given the specified ILocalHourAngle argument.
        public LocalStellarHourAngleDateTime(ILocalHourAngle localHourAngle)
            : base(localHourAngle)
        { }

        // Returns an IAstronomicalDateTime representing the date and time when the specified ILocalObserver.Target
        // reaches the specified target local hour angle (LHA) as observed in standardized local time, beginning from
        // the specified starting IAstronomicalDateTime.
        // If 'cycle' is HourAngleCycle.Previous, this returns the previous date/time that the targeting object reaches
        // the target LHA, otherwise this returns the next date/time that the targeting object reached the target LHA.
        public override IAstronomicalDateTime Calculate(
            ILocalObserver localObserver, IAstronomicalDateTime dateTime, Angle target, HourAngleCycle cycle)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
            _ = localObserver ?? throw new ArgumentNullException(nameof(localObserver));

            // The current local hour angle of the targeting object at the starting date and time.
            // This is calculated in stellar time
            Angle currentLHA = _localHourAngle.Calculate(localObserver, dateTime);
#if DEBUG
            Debug.Assert(target.IsCoterminal() && currentLHA.IsCoterminal());
#endif
            // Get the standardized local date and time, ignoring the effects of DST.
            DateTime standardDateTime = DateTimeFactory.ConvertToMutable(dateTime).ToStandardTime();

            // Total hours since midnight (00:00:00) to the specified time, converted to stellar time.
            double timeOfDay = standardDateTime.TimeOfDay.TotalHours * Constants.SolarToStellarRatio;

            // Total hours until the targeting object reaches the target LHA, starting from the current LHA.
            double hoursToLHA = cycle switch
            {
                // Approximate time in stellar hours until the targeting object reaches the previous target LHA,
                // starting from the specified time.
                // This value will always be negative.
                HourAngleCycle.Previous => -Angle.Coterminal(currentLHA - target).TotalHours,

                // Approximate time in stellar hours until the targeting object reaches the next target LHA,
                // starting from the specified time.
                HourAngleCycle.Next or _ => Angle.Coterminal(target - currentLHA).TotalHours
            };
#if DEBUG
            if (cycle == HourAngleCycle.Previous)
            {
                Debug.Assert(hoursToLHA <= 0);
            }
#endif
            // Total hours until the targeting object reaches the target LHA, starting from midnight on the specified date.
            double totalHours = timeOfDay + hoursToLHA;

            // The total hours offset starts from midnight (00:00:00) on the specified date.
            // Use DateTime.Date to truncate the time component.
            IMutableDateTime result = localObserver.DateTimeInfo.ConvertTimeFromStandard(standardDateTime.Date);

#if DEBUG
#warning Possible false positive may occur when the result of DateTime.Date exactly equals MinUtcDateTime or MaxUtcDateTime.
#endif

            // Calling DateTime.Date in the previous step could cause the value to fall out of range.
            // Do not attempt to modify the min/max datetime value because it could lead to unexpected results.
            if (!result.IsMinOrMaxValue())
            {
                // Convert the hours back to mean solar time before adding to the date.
                result = result.AddHours(totalHours * Constants.StellarToSolarRatio);
            }

            return DateTimeFactory.ConvertToAstronomical(result);
        }
    }
}
