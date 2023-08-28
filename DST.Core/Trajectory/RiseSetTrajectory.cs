using DST.Core.LocalHourAngleDateTime;
using DST.Core.Observer;
using DST.Core.Vector;
using DST.Core.Physics;
using DST.Core.DateAndTime;

namespace DST.Core.Trajectory
{
    // Represents a type of celestial trajectory in which the object is sometimes visible relative to the observer's position.
    public class RiseSetTrajectory : BaseVariableTrajectory, ITrajectory, IVariableTrajectory, IRiseSetTrajectory
    {
        // Creates a new RiseSetTrajectory instance with the specified ILocalObserver argument.
        public RiseSetTrajectory(ILocalObserver localObserver)
            : base(localObserver)
        { }

        // Returns a value that indicates whether the target is in the observer's local sky at the specified date and time.
        public override bool IsAboveHorizon(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            // Calculate the local hour angle (LHA) of the ILocalObserver at the specified date and time.
            Angle lha = _localObserver.LocalHourAngle.Calculate(_localObserver, dateTime);

            // If the LHA is not between the set and rise local hour angles, then it is above the observer's horizon.
            // Equivalent to GetRiseHourAngle() < LHA < 360.0 || 0.0 <= LHA <= GetSetHourAngle()
            return !(GetSetHourAngle() <= lha && lha <= GetRiseHourAngle());
        }

        // Returns a tracking of the target when it passes through the observer's meridian,
        // starting from the specified date/time.
        public override IVector GetApex(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            // If the target has already reached its apex position and is approaching the observer's horizon,
            // calculate the previous apex position.
            if (IsAboveHorizon(dateTime) && IsSetting(dateTime))
            {
                return CalculateVector(dateTime, Angle.Zero, HourAngleCycle.Previous);
            }

            // The target has either already set from the observer's horizon, or is currently approaching
            // the observer's meridian, so calculate the next apex position.
            return CalculateVector(dateTime, Angle.Zero, HourAngleCycle.Next);
        }

        // Returns a value that indicates whether the target has risen from the observer's horizon
        // and is approaching the observer's meridian at the specified date and time.
        public bool IsRising(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            // Calculate the local hour angle (LHA) of the ILocalObserver at the specified date and time.
            Angle lha = _localObserver.LocalHourAngle.Calculate(_localObserver, dateTime);

            // The target is approaching the observer's meridian if its current LHA lies between its
            // rising hour angle and its apex position.
            return GetRiseHourAngle() < lha && lha < 360.0;
        }

        // Returns a value that indicates whether the target has reached its apex at the observer's meridian
        // and is approaching the observer's horizon at the specified date and time.
        public bool IsSetting(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            // Calculate the local hour angle (LHA) of the ILocalObserver at the specified date and time.
            Angle lha = _localObserver.LocalHourAngle.Calculate(_localObserver, dateTime);

            // The target is approaching the observer's horizon if its current LHA lies between its
            // apex position and its setting hour angle.
            return 0.0 < lha && lha < GetSetHourAngle();
        }

        // Returns a tracking of the target when it rises from the observer's horizon, starting from the specified date/time.
        public IVector GetRise(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            // If the target has already risen from the observer's horizon, calculate the previous rising position.
            if (IsAboveHorizon(dateTime))
            {
                return CalculateVector(dateTime, GetRiseHourAngle(), HourAngleCycle.Previous);
            }

            // The target has either already reached its apex position, or has already set and is currently approaching
            // the observer's horizon, so calculate the next rising position.
            return CalculateVector(dateTime, GetRiseHourAngle(), HourAngleCycle.Next);
        }

        // Returns a tracking of the target when it sets at the observer's horizon, starting from the specified date/time.
        public IVector GetSet(IAstronomicalDateTime dateTime)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            // Calculate the target's next setting position.
            return CalculateVector(dateTime, GetSetHourAngle(), HourAngleCycle.Next);
        }

        // Returns a tracking of the target when it rises from the observer's horizon, starting from the specified date/time,
        // for a specified number of cycles in the underlying time scale.
        public IVector[] GetRise(IAstronomicalDateTime start, int cycles)
        {
            _ = start ?? throw new ArgumentNullException(nameof(start));

            IAstronomicalDateTime[] dateTimes = GetDateTimes(start, cycles);

            IVector[] vectors = new IVector[dateTimes.Length];

            for (int i = 0; i < dateTimes.Length; i++)
            {
                vectors[i] = GetRise(dateTimes[i]);
            }

            return vectors;
        }

        // Returns a tracking of the target when it sets at the observer's horizon, starting from the specified date/time,
        // for a specified number of cycles in the underlying time scale.
        public IVector[] GetSet(IAstronomicalDateTime start, int cycles)
        {
            _ = start ?? throw new ArgumentNullException(nameof(start));

            IAstronomicalDateTime[] dateTimes = GetDateTimes(start, cycles);

            IVector[] vectors = new IVector[dateTimes.Length];

            for (int i = 0; i < dateTimes.Length; i++)
            {
                vectors[i] = GetSet(dateTimes[i]);
            }

            return vectors;
        }

        // Returns the local hour angle (LHA) of the targeting object when its observable altitude is zero degrees (0°).
        // This is equivalent to the setting LHA.
        private Angle GetZeroAltitudeHourAngle()
        {
            // Get the target's declination (delta) and the latitude (phi), in radians.
            double delta = _localObserver.Target.Declination.TotalRadians;
            double phi = _localObserver.Location.Latitude.TotalRadians;

            // Calculate the local hour angle (omega), in radians, when the altitude is 0°.
            // This represents the anticipated rise/set local hour angle, so we don't need to use the nutated declination.
            double omega = Math.Acos(-Math.Tan(delta) * Math.Tan(phi));

            // The hour angle could not be calculated.
            if (double.IsNaN(omega)) return default;

            // Convert the hour angle from radians to degrees onto the interval [0°, 360°).
            Angle lha = Angle.FromRadians(omega).Coterminal();

            return lha;
        }

        // Returns the local hour angle (LHA) of the targeting object as it is about to set at the observer's local horizon.
        private Angle GetSetHourAngle()
        {
            return GetZeroAltitudeHourAngle();
        }

        // Returns the local hour angle (LHA) of the targeting object as it is about to rise at the observer's local horizon.
        private Angle GetRiseHourAngle()
        {
            // Get the 0° altitude LHA of the targeting object.
            Angle lha = GetSetHourAngle();

            // Negate the hour angle and relate back onto the interval [0°, 360°).
            return Angle.Coterminal(-lha);
        }

        // Returns the string-representation of this RiseSetTrajectory instance.
        public override string ToString()
        {
            return DST.Resources.DisplayText.TrajectoryRiseAndSet;
        }
    }
}
