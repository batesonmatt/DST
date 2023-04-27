using DST.Core.LocalHourAngle;
using DST.Core.Observer;
using DST.Core.Physics;

namespace DST.Core.LocalHourAngleDateTime
{
    public class LocalMeanSiderealHourAngleDateTime : BaseLocalHourAngleDateTime
    {
        // Creates a new LocalMeanSiderealHourAngleDateTime instance given the specified ILocalHourAngle argument.
        public LocalMeanSiderealHourAngleDateTime(ILocalHourAngle localHourAngle)
            : base(localHourAngle)
        { }

        // Returns an AstronomicalDateTime representing the date and time when the specified ILocalObserver.Target
        // reaches the specified target local hour angle (LHA) as observed in standardized local time, beginning from
        // the specified starting AstronomicalDateTime.
        // If 'cycle' is HourAngleCycle.Next, this returns the next date/time that the targeting object reaches
        // the target LHA, otherwise this returns the previous date/time that the targeting object reached the target LHA.
        public override AstronomicalDateTime Calculate(
            ILocalObserver localObserver, AstronomicalDateTime dateTime, Angle target, HourAngleCycle cycle)
        {
            _ = localObserver ?? throw new ArgumentNullException(nameof(localObserver));

            // The current local hour angle of the targeting object at the starting date and time.
            // This is calculated in mean sidereal time.
            Angle currentLHA = _localHourAngle.Calculate(localObserver, dateTime);

            // Approximate time in sidereal hours until the targeting object reaches the next target LHA,
            // starting from the specified time.
            double hoursToNextLHA = Angle.Coterminal(target - currentLHA).TotalHours;

            // Get the standardized local date and time, ignoring the effects of DST.
            DateTime standardDateTime = dateTime.ToStandardTime();

            // Total hours since midnight (00:00:00) to the specified time, converted to sidereal time.
            double timeOfDay = standardDateTime.TimeOfDay.TotalHours * Constants.SolarToSiderealRatio;

            // Approximate time in sidereal hours until the targeting object reaches the target LHA on the current cycle,
            // beginning at midnight on the specified date.
            // This value may be negative.
            double hoursToTodayLHA = Angle.Coterminal(
                timeOfDay * Constants.RotationPerHour + hoursToNextLHA * Constants.RotationPerHour).TotalHours;

            // If the time until the targeting object reaches the target LHA on this date since midnight
            // occurs before, or on, the current time of day, then adjust the starting date/time
            // so that we may evaluate the next time the object reaches the same LHA.
            double totalHours = cycle switch
            {
                HourAngleCycle.Next when hoursToTodayLHA <= timeOfDay => timeOfDay + hoursToNextLHA,
                _ => hoursToTodayLHA
            };

            // The total hours offset starts from midnight (00:00:00) on the specified date.
            // Use AstronomicalDateTime.Date to truncate the time component.
            // Convert the hours back to mean solar time before adding to the date.
            AstronomicalDateTime result = AstronomicalDateTime.FromStandardTime(standardDateTime.Date, dateTime.Info)
                .AddHours(totalHours * Constants.SiderealToSolarRatio);

            return result;
        }
    }
}
