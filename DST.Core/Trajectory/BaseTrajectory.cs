﻿using DST.Core.Coordinate;
using DST.Core.DateTimeAdder;
using DST.Core.DateTimesBuilder;
using DST.Core.LocalHourAngleDateTime;
using DST.Core.Observer;
using DST.Core.TimeKeeper;
using DST.Core.TimeScalable;
using DST.Core.Tracker;
using DST.Core.Vector;
using DST.Core.Physics;
using DST.Core.DateAndTime;

namespace DST.Core.Trajectory
{
    public abstract class BaseTrajectory : ITrajectory
    {
        // The underlying ILocalObserver object for this BaseTrajectory instance.
        protected readonly ILocalObserver _localObserver;

        // The underlying ITracker object for this BaseTrajectory instance.
        protected readonly ITracker _tracker;

        // Creates a new BaseTrajectory instance with the specified ILocalObserver argument.
        protected BaseTrajectory(ILocalObserver localObserver)
        {
            _localObserver = localObserver ?? throw new ArgumentNullException(nameof(localObserver));
            _tracker = TrackerFactory.Create(_localObserver);
        }

        // Returns a value that indicates whether the target is in the observer's local sky at the specified date and time.
        public abstract bool IsAboveHorizon(IAstronomicalDateTime dateTime);

        // Returns the type of time scale this ITrajectory instance will use for calculating vectors over multiple cycles.
        public virtual TimeScale GetTimeScale()
        {
            // The current tracking algorithms perform in either sidereal time (GMST and GAST) or stellar time (ERA).
            // Technically, apparent sidereal time should be using a different timescale than mean sidereal time, 
            // though the difference is only a few arc-seconds at most.
            return _localObserver.TimeKeeper switch
            {
                MeanSiderealTimeKeeper => TimeScale.SiderealTime,
                SiderealTimeKeeper => TimeScale.SiderealTime,
                StellarTimeKeeper => TimeScale.StellarTime,
                _ => throw new NotSupportedException($"ITimeKeeper object '{_localObserver.TimeKeeper.GetType()}' is not supported.")
            };
        }

        // Returns a tracking of the targeting object at a specified local hour angle, beginning on a specified date and time.
        // If 'cycle' is HourAngleCycle.Next, this will track the target for the next date/time it reaches the local hour angle.
        // Otherwise, this will track the target for the previous date/time it reached the local hour angle.
        protected IVector CalculateVector(IAstronomicalDateTime dateTime, Angle target, HourAngleCycle cycle)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));

            // Calculate the date and time of the expected LHA.
            IAstronomicalDateTime finalDateTime = _localObserver.LocalHourAngleDateTime
                .Calculate(_localObserver, dateTime, target, cycle);

            // Track the observer at the calculated date/time.
            ICoordinate position = _tracker.Track(finalDateTime);

            // Get the datetime as an IMutableDateTime object.
            IMutableDateTime mutableDateTime = DateTimeFactory.ConvertToMutable(finalDateTime);

            // The IVector, containing the date/time and position values.
            return VectorFactory.Create(mutableDateTime, position);
        }

        // Builds an array of date/time values in the underlying timescale, starting from the specified date/time value,
        // for the specified number of cycles.
        protected IAstronomicalDateTime[] GetDateTimes(IBaseDateTime start, int cycles)
        {
            _ = start ?? throw new ArgumentNullException(nameof(start));

            IMutableDateTime mutableStart = DateTimeFactory.ConvertToMutable(start);

            // Return an empty array if all possible datetimes in this period are out of range.
            if ((mutableStart.IsMinValue() && cycles <= 0) || (mutableStart.IsMaxValue() && cycles >= 0))
            {
                return Array.Empty<IAstronomicalDateTime>();
            }

            // The number of cycles must be non-zero.
            if (cycles == 0)
            {
                // There are no cycles to evaluate from the given starting date.
                return new IAstronomicalDateTime[] { DateTimeFactory.ConvertToAstronomical(start) };
            }

            // Get the underlying time scale.
            TimeScale timeScale = GetTimeScale();

            // The appropriate time unit will be in days, since the cycles are incremented/decremented
            // in single, full cycles of the underlying time scale.
            ITimeScalable timeScalable = TimeScalableFactory.Create(timeScale);
            IDateTimeAdder dateTimeAdder = DateTimeAdderFactory.Create(timeScalable, TimeUnit.Days);
            IDateTimesBuilder dateTimesBuilder = DateTimesBuilderFactory.Create(dateTimeAdder, true);
            IBaseDateTime[] dateTimes = dateTimesBuilder.Build(start, cycles, 1);

            return DateTimeFactory.ConvertToAstronomical(dateTimes);
        }
    }
}
