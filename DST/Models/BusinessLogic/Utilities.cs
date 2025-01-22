using DST.Models.DomainModels;
using DST.Core.Coordinate;
using DST.Core.Physics;
using DST.Core.DateAndTime;
using DST.Core.TimeKeeper;
using DST.Core.Observer;
using DST.Core.Trajectory;
using System;
using DST.Models.Extensions;
using System.Linq;
using System.Globalization;
using DST.Core.Vector;
using DST.Core.DateTimeAdder;
using DST.Core.TimeScalable;
using DST.Core.DateTimesBuilder;
using DST.Core.Tracker;
using DST.Core.Components;
using DST.Models.ViewModels;

namespace DST.Models.BusinessLogic
{
    public class Utilities
    {
        public static FormatType GetCoordinateFormat(string name)
        {
            FormatType format;

            if (name.EqualsSeo(CoordinateFormatName.Component))
            {
                format = FormatType.Component;
            }
            else if (name.EqualsSeo(CoordinateFormatName.Decimal))
            {
                format = FormatType.Decimal;
            }
            else if (name.EqualsSeo(CoordinateFormatName.Compact))
            {
                format = FormatType.Compact;
            }
            else
            {
                format = FormatType.Default;
            }

            return format;
        }

        public static Algorithm GetAlgorithm(string name)
        {
            Algorithm algorithm;

            if (name.EqualsSeo(AlgorithmName.GMST))
            {
                algorithm = Algorithm.GMST;
            }
            else if (name.EqualsSeo(AlgorithmName.GAST))
            {
                algorithm = Algorithm.GAST;
            }
            else if (name.EqualsSeo(AlgorithmName.ERA))
            {
                algorithm = Algorithm.ERA;
            }
            else
            {
                algorithm = Algorithm.Default;
            }

            return algorithm;
        }

        public static Phase GetPhase(string name)
        {
            Phase phase;

            if (name.EqualsSeo(PhaseName.Rise))
            {
                phase = Phase.Rise;
            }
            else if (name.EqualsSeo(PhaseName.Apex))
            {
                phase = Phase.Apex;
            }
            else if (name.EqualsSeo(PhaseName.Set))
            {
                phase = Phase.Set;
            }
            else
            {
                phase = Phase.Default;
            }

            return phase;
        }

        public static TimeUnit GetTimeUnit(string name)
        {
            TimeUnit timeUnit;

            if (name.EqualsSeo(TimeUnitName.Seconds))
            {
                timeUnit = TimeUnit.Seconds;
            }
            else if (name.EqualsSeo(TimeUnitName.Minutes))
            {
                timeUnit = TimeUnit.Minutes;
            }
            else if (name.EqualsSeo(TimeUnitName.Hours))
            {
                timeUnit = TimeUnit.Hours;
            }
            else if (name.EqualsSeo(TimeUnitName.Days))
            {
                timeUnit = TimeUnit.Days;
            }
            else if (name.EqualsSeo(TimeUnitName.Weeks))
            {
                timeUnit = TimeUnit.Weeks;
            }
            else if (name.EqualsSeo(TimeUnitName.Months))
            {
                timeUnit = TimeUnit.Months;
            }
            else if (name.EqualsSeo(TimeUnitName.Years))
            {
                timeUnit = TimeUnit.Years;
            }
            else
            {
                timeUnit = TimeUnit.Default;
            }

            return timeUnit;
        }

        public static bool SupportsFixedTracking(TimeUnit timeUnit)
        {
            // Fixed tracking is not supported for time units less than days.
            return timeUnit switch
            {
                TimeUnit.Days or TimeUnit.Weeks or TimeUnit.Months or TimeUnit.Years => true,
                _ => false
            };
        }

        public static bool SupportsAggregatedIntervals(TimeUnit timeUnit)
        {
            // Aggregated interval calculation is only supported for Months and Years,
            // for which the number of days is not consistent.
            return timeUnit switch
            {
                TimeUnit.Months or TimeUnit.Years => true,
                _ => false
            };
        }

        public static TimeScale GetTimeScale(Algorithm algorithm, bool isFixed)
        {
            if (isFixed && (algorithm == Algorithm.GMST || algorithm == Algorithm.GAST))
            {
                return TimeScale.SiderealTime;
            }

            if (isFixed && algorithm == Algorithm.ERA)
            {
                return TimeScale.StellarTime;
            }

            return TimeScale.MeanSolarTime;
        }

        public static IDateTimeAdder GetDateTimeAdder(TimeScale timeScale, TimeUnit timeUnit)
        {
            ITimeScalable scale = TimeScalableFactory.Create(timeScale);
            IDateTimeAdder dateTimeAdder = DateTimeAdderFactory.Create(scale, timeUnit);

            return dateTimeAdder;
        }

        public static int GetClientPeriod(int period, string algorithmName, string timeUnitName, bool isFixed)
        {
            int result;
            Algorithm algorithm;
            TimeUnit timeUnit;
            TimeScale timeScale;
            IDateTimeAdder dateTimeAdder;

            try
            {
                algorithm = GetAlgorithm(algorithmName);
                timeUnit = GetTimeUnit(timeUnitName);

                if (isFixed && !SupportsFixedTracking(timeUnit))
                {
                    isFixed = false;
                }

                timeScale = GetTimeScale(algorithm, isFixed);
                dateTimeAdder = GetDateTimeAdder(timeScale, timeUnit);
                result = int.Clamp(period, dateTimeAdder.Min, dateTimeAdder.Max);
            }
            catch
            {
                result = 0;
            }

            return result;
        }

        public static string ValidateClientPeriod(int period, string algorithmName, string timeUnitName, bool isFixed)
        {
            string message = string.Empty;
            Algorithm algorithm;
            TimeUnit timeUnit;
            TimeScale timeScale;
            IDateTimeAdder dateTimeAdder;

            try
            {
                algorithm = GetAlgorithm(algorithmName);
                timeUnit = GetTimeUnit(timeUnitName);

                if (isFixed && !SupportsFixedTracking(timeUnit))
                {
                    isFixed = false;
                }

                timeScale = GetTimeScale(algorithm, isFixed);
                dateTimeAdder = GetDateTimeAdder(timeScale, timeUnit);

                if (period < dateTimeAdder.Min || period > dateTimeAdder.Max)
                {
                    message = string.Format(
                        Resources.DisplayText.TrackValidationPeriodRange,
                        dateTimeAdder.Min, dateTimeAdder.Max);
                }
            }
            catch
            {
                message = Resources.DisplayText.TrackValidationPeriodUnknown;
            }

            return message;
        }

        public static string ValidateClientInterval(int interval, int period)
        {
            // Assumes the period length has already been validated.
            if (interval < 0 || interval > period)
            {
                return string.Format(Resources.DisplayText.TrackValidationIntervalRange, 0, period);
            }

            return string.Empty;
        }

        // Returns a message indicating whether the specified DateTime value is valid for the client's time zone.
        // Argument 'dateTime' is assumed to be represented in the client's local time.
        // If 'dateTime' is valid, this returns an empty string.
        public static string ValidateClientDateTime(DateTime dateTime, GeolocationModel geolocation)
        {
            string message = string.Empty;
            IDateTimeInfo dateTimeInfo;
            DateTime minLocal;
            DateTime maxLocal;

            try
            {
                dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId(geolocation.TimeZoneId);
                minLocal = dateTimeInfo.MinAstronomicalDateTime.ToLocalTime();
                maxLocal = dateTimeInfo.MaxAstronomicalDateTime.ToLocalTime();

                if (dateTime < minLocal || dateTime > maxLocal)
                {
                    message = string.Format(
                        Resources.DisplayText.StartDateValidationRange, 
                        minLocal.ToString(CultureInfo.CurrentCulture), 
                        maxLocal.ToString(CultureInfo.CurrentCulture));
                }
            }
            catch
            {
                message = Resources.DisplayText.StartDateValidationTimeZone;
            }

            return message;
        }

        // Returns the current local DateTime in the client's timezone.
        public static DateTime GetClientDateTime(GeolocationModel geolocation)
        {
            IDateTimeInfo dateTimeInfo;
            DateTime clientDateTime;

            try
            {
                // Get the date and time info for the client.
                dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId(geolocation.TimeZoneId);

                // Get the client's current date and time represented in local time.
                clientDateTime = DateTimeFactory.CreateLocal(dateTimeInfo);
            }
            catch
            {
                clientDateTime = DateTime.MinValue;
            }

            return clientDateTime;
        }

        // Returns a new DateTime value from the given number of ticks, represented in local time for the client's timezone.
        public static DateTime GetClientDateTime(GeolocationModel geolocation, long ticks)
        {
            if (ticks <= 0)
            {
                return GetClientDateTime(geolocation);
            }

            IDateTimeInfo dateTimeInfo;
            DateTime clientDateTime;

            try
            {
                // Get the date and time info for the client.
                dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId(geolocation.TimeZoneId);

                // Get the client's current date and time represented in local time.
                clientDateTime = DateTimeFactory.CreateLocal(ticks, dateTimeInfo);
            }
            catch
            {
                clientDateTime = DateTime.MinValue;
            }

            return clientDateTime;
        }

        public static ILocalObserver GetLocalObserver(DsoModel dso, GeolocationModel geolocation, Algorithm algorithm)
        {
            IEquatorialCoordinate target;
            IGeographicCoordinate location;
            IDateTimeInfo dateTimeInfo;
            ITimeKeeper timeKeeper;
            ILocalObserver localObserver;

            try
            {
                // Get the equatorial coordinate of the DsoModel.
                target = CoordinateFactory.CreateEquatorial(
                    rightAscension: new Angle(TimeSpan.FromHours(dso.RightAscension)), declination: new Angle(dso.Declination));

                // Get the client's geographic coordinate.
                location = CoordinateFactory.CreateGeographic(
                    latitude: new Angle(geolocation.Latitude), longitude: new Angle(geolocation.Longitude));

                // Get the client's date and time info.
                dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId(geolocation.TimeZoneId);

                // Create the timekeeping algorithm.
                timeKeeper = TimeKeeperFactory.Create(algorithm);

                // Create the local observer.
                localObserver = ObserverFactory.CreateLocal(dateTimeInfo, location, target, timeKeeper);
            }
            catch
            {
                localObserver = null;
            }

            return localObserver;
        }

        public static IObserver GetObserver(DsoModel dso, GeolocationModel geolocation, Algorithm algorithm)
        {
            IEquatorialCoordinate target;
            IGeographicCoordinate location;
            IDateTimeInfo dateTimeInfo;
            ITimeKeeper timeKeeper;
            IObserver observer;

            try
            {
                // Get the equatorial coordinate of the DsoModel.
                target = CoordinateFactory.CreateEquatorial(
                    rightAscension: new Angle(TimeSpan.FromHours(dso.RightAscension)), declination: new Angle(dso.Declination));

                // Get the client's geographic coordinate.
                location = CoordinateFactory.CreateGeographic(
                    latitude: new Angle(geolocation.Latitude), longitude: new Angle(geolocation.Longitude));

                // Get the client's date and time info.
                dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId(geolocation.TimeZoneId);
                
                // Create the timekeeping algorithm.
                timeKeeper = TimeKeeperFactory.Create(algorithm);

                // Create the observer.
                observer = ObserverFactory.Create(dateTimeInfo, location, target, timeKeeper);
            }
            catch
            {
                observer = null;
            }

            return observer;
        }

        // Calculates and returns the type of trajectory for the specified DsoModel and GeolocationModel objects.
        public static ITrajectory GetTrajectory(DsoModel dso, GeolocationModel geolocation, Algorithm algorithm = Algorithm.GMST)
        {
            IObserver observer;
            ITrajectory trajectory;

            try
            {
                // Create the observer.
                observer = GetObserver(dso, geolocation, algorithm);

                // Calculate the trajectory of the equatorial object relative to the observer's geographic location.
                trajectory = TrajectoryCalculator.Calculate(observer);
            }
            catch
            {
                trajectory = null;
            }

            return trajectory;
        }

        // Returns the trajectory name for the specified DsoModel and GeolocationModel objects.
        public static string GetTrajectoryName(DsoModel dso, GeolocationModel geolocation)
        {
            string result = string.Empty;
            ITrajectory trajectory;

            try
            {
                trajectory = GetTrajectory(dso, geolocation);

                if (trajectory is not null)
                {
                    result = GetTrajectory(dso, geolocation).ToString();
                }
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }

        // Returns the primary trajectory name for the specified DsoModel and GeolocationModel objects.
        public static string GetPrimaryTrajectoryName(DsoModel dso, GeolocationModel geolocation)
        {
            string result;
            ITrajectory trajectory;

            try
            {
                trajectory = GetTrajectory(dso, geolocation);

                result = trajectory switch
                {
                    null => string.Empty,
                    IMultipleNameTrajectory trajectoryName => trajectoryName.GetPrimaryName(),
                    _ => trajectory.ToString(),
                };
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }

        // Returns the primary trajectory name for the specified ITrajectory object.
        public static string GetPrimaryTrajectoryName(ITrajectory trajectory)
        {
            string result;

            try
            {
                result = trajectory switch
                {
                    null => string.Empty,
                    IMultipleNameTrajectory trajectoryName => trajectoryName.GetPrimaryName(),
                    _ => trajectory.ToString(),
                };
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }

        public static DsoDetailsInfo GetDetailsInfo(DsoModel dso)
        {
            DsoDetailsInfo info;
            IEquatorialCoordinate target;

            try
            {
                target = CoordinateFactory.CreateEquatorial(
                    rightAscension: new Angle(dso.RightAscension), declination: new Angle(dso.Declination));

                info = new();
                
                if (dso.HasMultipleNames)
                {
                    info.Add(DsoDetailsItem.OtherNames, new(Resources.DisplayText.TargetOtherNames, string.Join(", ", dso.GetOtherNames())));
                }

                info.Add(DsoDetailsItem.Catalog, new(Resources.DisplayText.TargetCatalog, dso.CatalogName));
                info.Add(DsoDetailsItem.Type, new(Resources.DisplayText.TargetType, dso.Type));
                info.Add(DsoDetailsItem.Description, new(Resources.DisplayText.TargetDescription, dso.Description));
                info.Add(DsoDetailsItem.Constellation, new(Resources.DisplayText.TargetConstellation, dso.ConstellationName));
                info.Add(DsoDetailsItem.RightAscension, new(Resources.DisplayText.TargetRightAscensionLong, target.Format(FormatType.Component, ComponentType.Rotation)));
                info.Add(DsoDetailsItem.Declination, new(Resources.DisplayText.TargetDeclinationLong, target.Format(FormatType.Component, ComponentType.Inclination)));
                info.Add(DsoDetailsItem.Distance, new(Resources.DisplayText.TargetDistance, string.Format(Resources.DisplayText.DistanceFormatDecimalKly, dso.Distance)));
                info.Add(DsoDetailsItem.Magnitude, new(Resources.DisplayText.TargetMagnitude, dso.Magnitude?.ToString(CultureInfo.CurrentCulture) ?? Resources.DisplayText.None));
            }
            catch
            {
                info = new();
            }

            return info;
        }

        public static TrackSummaryInfo GetSummaryInfo(
            DsoModel dso, SeasonModel season, ConstellationModel constellation, GeolocationModel geolocation, Algorithm algorithm)
        {
            TrackSummaryInfo info;
            ILocalObserver localObserver;
            ITrajectory trajectory;
            IMutableDateTime clientDateTime;
            DateTime clientLocalTime;
            IAstronomicalDateTime astronomicalDateTime;
            IEquatorialCoordinate nutation;
            ITracker tracker;
            IHorizontalCoordinate position;
            string visibility;

            try
            {
                localObserver = Utilities.GetLocalObserver(dso, geolocation, algorithm);
                trajectory = TrajectoryCalculator.Calculate(localObserver);
                clientDateTime = DateTimeFactory.CreateMutable(DateTime.UtcNow, localObserver.DateTimeInfo);
                clientLocalTime = clientDateTime.ToLocalTime();
                astronomicalDateTime = DateTimeFactory.ConvertToAstronomical(clientDateTime);
                nutation = localObserver.Target.GetNutation(astronomicalDateTime);
                tracker = TrackerFactory.Create(localObserver);
                position = tracker.Track(astronomicalDateTime) as IHorizontalCoordinate;

                if (trajectory is null or NeverRiseTrajectory)
                {
                    visibility = Resources.DisplayText.TargetVisibilityNeverRise;
                }
                else if (trajectory is ICircumpolarTrajectory)
                {
                    visibility = Resources.DisplayText.TargetVisibilityCircumpolar;
                }
                else if (localObserver.Location.Latitude > constellation.NorthernLatitude ||
                         localObserver.Location.Latitude < -constellation.SouthernLatitude)
                {
                    visibility = Resources.DisplayText.TargetVisibilityOutOfRange;
                }
                else if (!season.ContainsDate(clientLocalTime))
                {
                    visibility = Resources.DisplayText.TargetVisibilityOutOfSeason;
                }
                else
                {
                    visibility = Resources.DisplayText.TargetVisibilityInRange;
                }

                info = new()
                {
                    { TrackSummaryItem.RightAscension, new(Resources.DisplayText.TargetRightAscensionLong, localObserver.Target.Format(FormatType.Component, ComponentType.Rotation)) },
                    { TrackSummaryItem.Declination, new(Resources.DisplayText.TargetDeclinationLong, localObserver.Target.Format(FormatType.Component, ComponentType.Inclination)) },
                    { TrackSummaryItem.Catalog, new(Resources.DisplayText.TargetCatalog, dso.CatalogName) },
                    { TrackSummaryItem.Type, new(Resources.DisplayText.TargetType, dso.Type) },
                    { TrackSummaryItem.Description, new(Resources.DisplayText.TargetDescription, dso.Description) },
                    { TrackSummaryItem.Constellation, new(Resources.DisplayText.TargetConstellation, dso.ConstellationName) },
                    { TrackSummaryItem.Distance, new(Resources.DisplayText.TargetDistance, string.Format(Resources.DisplayText.DistanceFormatDecimalKly, dso.Distance)) },
                    { TrackSummaryItem.Magnitude, new(Resources.DisplayText.TargetMagnitude, dso.Magnitude?.ToString(CultureInfo.CurrentCulture) ?? Resources.DisplayText.None) },
                    {
                        TrackSummaryItem.Season,
                        new(Resources.DisplayText.TargetSeason,
                            string.Format(Resources.DisplayText.TargetSeasonDetailsFormat,
                                localObserver.Location.Latitude >= 0.0 ? season.North : season.South,
                                CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(season.StartMonth),
                                CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(season.EndMonth)))
                    },
                    { TrackSummaryItem.Latitude, new(Resources.DisplayText.ObserverLatitudeLong, localObserver.Location.Format(FormatType.Component, ComponentType.Inclination)) },
                    { TrackSummaryItem.Longitude, new(Resources.DisplayText.ObserverLongitudeLong, localObserver.Location.Format(FormatType.Component, ComponentType.Rotation)) },
                    { TrackSummaryItem.TimeZone, new(Resources.DisplayText.ObserverTimeZone, localObserver.DateTimeInfo.ClientTimeZoneInfo.DisplayName) },
                    { TrackSummaryItem.LocalTime, new(Resources.DisplayText.ObserverLocalTime, clientLocalTime.ToString(CultureInfo.CurrentCulture)) },
                    { TrackSummaryItem.UniversalTime, new(Resources.DisplayText.ObserverUniversalTimeLong, clientDateTime.Value.ToString(CultureInfo.CurrentCulture)) },
                    { TrackSummaryItem.Algorithm, new(Resources.DisplayText.ObserverAlgorithm, string.Empty) },
                    { TrackSummaryItem.TimeKeeper, new(localObserver.TimeKeeper.ToString(), localObserver.TimeKeeper.Calculate(astronomicalDateTime).ToString()) },
                    { TrackSummaryItem.LocalTimeKeeper, new(localObserver.LocalTimeKeeper.ToString(), localObserver.LocalTimeKeeper.Calculate(localObserver, astronomicalDateTime).ToString()) },
                    { TrackSummaryItem.LocalHourAngle, new(localObserver.LocalHourAngle.ToString(), localObserver.LocalHourAngle.Calculate(localObserver, astronomicalDateTime).ToString()) },
                    { TrackSummaryItem.EquationOfEquinoxes, new(Resources.DisplayText.ObserverEquationOfEquinoxes, astronomicalDateTime.GetEquationOfEquinoxes().ToString()) },
                    { TrackSummaryItem.EquationOfOrigins, new(Resources.DisplayText.ObserverEquationOfOrigins, astronomicalDateTime.GetEquationOfOrigins().ToString()) },
                    { TrackSummaryItem.RightAscensionIntermediate, new(Resources.DisplayText.TargetRightAscensionIntermediate,
                        localObserver.Target.GetIntermediateRightAscension(astronomicalDateTime).ToString(Angle.FormatType.ComponentHours)) },
                    { TrackSummaryItem.RightAscensionNutation, new(Resources.DisplayText.TargetRightAscensionNutation, nutation.Format(FormatType.Component, ComponentType.Rotation)) },
                    { TrackSummaryItem.DeclinationNutation, new(Resources.DisplayText.TargetDeclinationNutation, nutation.Format(FormatType.Component, ComponentType.Inclination)) },
                    { TrackSummaryItem.Altitude, new(Resources.DisplayText.TargetAltitudeLong, position.Format(FormatType.Component, ComponentType.Inclination)) },
                    { TrackSummaryItem.Azimuth, new(Resources.DisplayText.TargetAzimuthLong, position.Format(FormatType.Component, ComponentType.Rotation)) },
                    { TrackSummaryItem.Trajectory, new(Resources.DisplayText.TargetTrajectory, Utilities.GetPrimaryTrajectoryName(trajectory)) },
                    { TrackSummaryItem.Visibility, new(Resources.DisplayText.TargetVisibility, visibility) }
                };
            }
            catch
            {
                info = new();
            }

            return info;
        }

        public static PhaseResultsViewModel GetPhaseResults(ILocalObserver localObserver, FormatType format, TrackPhaseModel phaseModel)
        {
            PhaseResultsViewModel resultsModel = new();
            IVector[] results;
            ITimeKeeper timeKeeper;
            ITimeScalable timeScale;
            ITrajectory trajectory;
            Phase phase;
            IAstronomicalDateTime start;

            try
            {
                if (phaseModel is not null && localObserver is not null)
                {
                    timeKeeper = localObserver.TimeKeeper;
                    trajectory = TrajectoryCalculator.Calculate(localObserver);

                    if (trajectory is not null and IVariableTrajectory variableTrajectory)
                    {
                        timeScale = TimeScalableFactory.Create(trajectory.GetTimeScale());
                        phase = GetPhase(phaseModel.Phase);
                        start = DateTimeFactory.CreateAstronomical(phaseModel.Start, localObserver.DateTimeInfo);

                        resultsModel.TimeKeeper = timeKeeper.ToString();
                        resultsModel.TimeScale = timeScale.ToString();
                        resultsModel.Start = phaseModel.GetFullStart();
                        resultsModel.Cycles = phaseModel.Cycles.ToString();

                        results = Array.Empty<IVector>();

                        if (phase == Phase.Apex)
                        {
                            resultsModel.Phase = PhaseName.Apex;
                            results = variableTrajectory.GetApex(start, phaseModel.Cycles);
                        }
                        else if (trajectory is IRiseSetTrajectory riseSetTrajectory)
                        {
                            if (phase == Phase.Rise)
                            {
                                resultsModel.Phase = PhaseName.Rise;
                                results = riseSetTrajectory.GetRise(start, phaseModel.Cycles);
                            }
                            else if (phase == Phase.Set)
                            {
                                resultsModel.Phase = PhaseName.Set;
                                results = riseSetTrajectory.GetSet(start, phaseModel.Cycles);
                            }
                        }

                        resultsModel.Results = results
                            .OfType<ILocalVector>()
                            .Select(
                            result => new TrackResult(result, format));
                    }
                }
            }
            catch { }

            return resultsModel;
        }

        public static PeriodResultsViewModel GetPeriodResults(ILocalObserver localObserver, Algorithm algorithm, FormatType format, TrackPeriodModel periodModel)
        {
            PeriodResultsViewModel resultsModel = new();
            IVector[] results;
            ITimeKeeper timeKeeper;
            ITimeScalable timeScale;
            TimeScale timeScaleType;
            TimeUnit timeUnit;
            IDateTimeAdder dateTimeAdder;
            IDateTimesBuilder dateTimesBuilder;
            IAstronomicalDateTime start;
            IBaseDateTime[] baseDateTimes;
            IAstronomicalDateTime[] dateTimes;
            ITracker tracker;
            ICoordinate[] positions;
            IMutableDateTime mutableDateTime;
            int count;
            
            try
            {
                if (periodModel is not null && localObserver is not null)
                {
                    timeKeeper = localObserver.TimeKeeper;
                    timeScaleType = GetTimeScale(algorithm, periodModel.IsFixed);
                    timeScale = TimeScalableFactory.Create(timeScaleType);
                    timeUnit = GetTimeUnit(periodModel.TimeUnit);
                    dateTimeAdder = GetDateTimeAdder(timeScaleType, timeUnit);
                    dateTimesBuilder = DateTimesBuilderFactory.Create(dateTimeAdder, aggregate: periodModel.IsAggregated);
                    start = DateTimeFactory.CreateAstronomical(periodModel.Start, localObserver.DateTimeInfo);
                    baseDateTimes = dateTimesBuilder.Build(start, periodModel.Period, periodModel.Interval);
                    dateTimes = DateTimeFactory.ConvertToAstronomical(baseDateTimes);
                    tracker = TrackerFactory.Create(localObserver);
                    positions = tracker.Track(dateTimes);

                    resultsModel.TimeKeeper = timeKeeper.ToString();
                    resultsModel.TimeScale = timeScale.ToString();
                    resultsModel.Start = periodModel.GetFullStart();
                    resultsModel.TimeUnit = dateTimeAdder.ToString();
                    resultsModel.Period = periodModel.Period.ToString();
                    resultsModel.Interval = periodModel.Interval.ToString();
                    resultsModel.Fixed = periodModel.IsFixed ? Resources.DisplayText.FixedTrackingEnabled : Resources.DisplayText.FixedTrackingDisabled;
                    resultsModel.Aggregated = periodModel.IsAggregated ? Resources.DisplayText.AggregatedIntervalsEnabled : Resources.DisplayText.AggregatedIntervalsDisabled;

                    count = int.Min(positions.Length, dateTimes.Length);
                    results = new IVector[count];

                    for (int i = 0; i < count; i++)
                    {
                        if (positions[i] is not null)
                        {
                            mutableDateTime = DateTimeFactory.ConvertToMutable(dateTimes[i]);
                            results[i] = VectorFactory.Create(mutableDateTime, positions[i]);
                        }
                    }

                    resultsModel.Results = results
                        .OfType<ILocalVector>()
                        .Select(
                        result => new TrackResult(result, format));
                }
            }
            catch { }

            return resultsModel;
        }

        // Returns a value indicating whether the specified deep-sky object may be seen from the specified geolocation during the current season.
        // This does not consider the affects of light pollution, nor the magnitude of the object.
        public static bool IsLocal(DsoModel dso, GeolocationModel geolocation)
        {
            bool result = true;
            ITrajectory trajectory;
            IDateTimeInfo dateTimeInfo;
            IMutableDateTime clientDateTime;

            try
            {
                // The observer's latitude must be within the constellation's visible range.
                if (geolocation.Latitude > dso.Constellation.NorthernLatitude ||
                    geolocation.Latitude < -dso.Constellation.SouthernLatitude)
                {
                    return false;
                }

                // Get the object's type of trajectory relative to the observer's location.
                trajectory = GetTrajectory(dso, geolocation);

                // The trajectory of the object must be visible above the observer's horizon at some point in time.
                if (trajectory is null or NeverRiseTrajectory)
                {
                    return false;
                }

                // If the object's trajectory is circumpolar, then it is always visible from the observer's location.
                if (trajectory is ICircumpolarTrajectory)
                {
                    return true;
                }

                // Get the date and time info for the client.
                dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId(geolocation.TimeZoneId);

                // Get the client's current date and time represented in universal time.
                clientDateTime = DateTimeFactory.CreateMutable(DateTime.UtcNow, dateTimeInfo);

                // The observer's current month in the client's local timezone must fall within the constellation's seasonal timespan.
                if (!dso.Constellation.Season.ContainsDate(clientDateTime.ToLocalTime()))
                {
                    return false;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        // Returns a value indicating whether the specified deep-sky object is currently visible from the specified geolocation.
        // This does not consider the affects of light pollution, nor the magnitude of the object.
        public static bool IsVisible(DsoModel dso, GeolocationModel geolocation)
        {
            bool result;
            ITrajectory trajectory;
            IDateTimeInfo dateTimeInfo;
            IMutableDateTime clientDateTime;
            IAstronomicalDateTime astronomicalDateTime;

            try
            {
                // The observer's latitude must be within the constellation's visible range.
                if (geolocation.Latitude > dso.Constellation.NorthernLatitude ||
                    geolocation.Latitude < -dso.Constellation.SouthernLatitude)
                {
                    return false;
                }

                // Get the object's type of trajectory relative to the observer's location.
                trajectory = GetTrajectory(dso, geolocation);

                // The trajectory of the object must be visible above the observer's horizon at some point in time.
                if (trajectory is null or NeverRiseTrajectory)
                {
                    return false;
                }

                // If the object's trajectory is circumpolar, then it is always visible from the observer's location.
                if (trajectory is ICircumpolarTrajectory)
                {
                    return true;
                }

                // Get the date and time info for the client.
                dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId(geolocation.TimeZoneId);

                // Get the client's current date and time represented in universal time.
                clientDateTime = DateTimeFactory.CreateMutable(DateTime.UtcNow, dateTimeInfo);

                // The observer's current month in the client's local timezone must fall within the constellation's seasonal timespan.
                if (!dso.Constellation.Season.ContainsDate(clientDateTime.ToLocalTime()))
                {
                    return false;
                }

                // Get a new IAstronomicalDateTime object using the client's current date and time info.
                astronomicalDateTime = DateTimeFactory.ConvertToAstronomical(clientDateTime);

                // Determine whether the object is above the observer's horizon.
                result = trajectory.IsAboveHorizon(astronomicalDateTime);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        // Returns a value indicating whether the specified deep-sky object is currently rising from the observer's
        // horizon and is approaching it's apex altitude at the observer's meridian.
        public static bool IsRising(DsoModel dso, GeolocationModel geolocation)
        {
            bool result;
            ITrajectory trajectory;
            IDateTimeInfo dateTimeInfo;
            IMutableDateTime clientDateTime;
            IAstronomicalDateTime astronomicalDateTime;

            try
            {
                // The observer's latitude must be within the constellation's visible range.
                if (geolocation.Latitude > dso.Constellation.NorthernLatitude ||
                    geolocation.Latitude < -dso.Constellation.SouthernLatitude)
                {
                    return false;
                }

                // Get the object's type of trajectory relative to the observer's location.
                trajectory = GetTrajectory(dso, geolocation);

                // The trajectory of the object must be able to rise above the observer's horizon at some point in time.
                if (trajectory is null or not IRiseSetTrajectory)
                {
                    return false;
                }

                // Get the date and time info for the client.
                dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId(geolocation.TimeZoneId);

                // Get the client's current date and time represented in universal time.
                clientDateTime = DateTimeFactory.CreateMutable(DateTime.UtcNow, dateTimeInfo);

                // The observer's current month in the client's local timezone must fall within the constellation's seasonal timespan.
                if (!dso.Constellation.Season.ContainsDate(clientDateTime.ToLocalTime()))
                {
                    return false;
                }

                // Get a new IAstronomicalDateTime object using the client's current date and time info.
                astronomicalDateTime = DateTimeFactory.ConvertToAstronomical(clientDateTime);

                if (trajectory is IRiseSetTrajectory riseSet)
                {
                    // Determine whether the object is rising from the observer's horizon.
                    result = riseSet.IsRising(astronomicalDateTime);
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public static long GetRiseTime(DsoModel dso, GeolocationModel geolocation)
        {
            long result;
            ITrajectory trajectory;
            IDateTimeInfo dateTimeInfo;
            IMutableDateTime clientDateTime;
            IAstronomicalDateTime astronomicalDateTime;
            DateTime localTime;
            DateTime riseTime;
            TimeSpan timeSpan;

            try
            {
                // The observer's latitude must be within the constellation's visible range.
                if (geolocation.Latitude > dso.Constellation.NorthernLatitude ||
                    geolocation.Latitude < -dso.Constellation.SouthernLatitude)
                {
                    return long.MaxValue;
                }

                // Get the object's type of trajectory relative to the observer's location.
                trajectory = GetTrajectory(dso, geolocation);

                // The trajectory of the object must be able to rise above the observer's horizon at some point in time.
                if (trajectory is null or not IRiseSetTrajectory)
                {
                    return long.MaxValue;
                }

                // Get the date and time info for the client.
                dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId(geolocation.TimeZoneId);

                // Get the client's current date and time represented in universal time.
                clientDateTime = DateTimeFactory.CreateMutable(DateTime.UtcNow, dateTimeInfo);

                // The observer's current month in the client's local timezone must fall within the constellation's seasonal timespan.
                if (!dso.Constellation.Season.ContainsDate(clientDateTime.ToLocalTime()))
                {
                    return long.MaxValue;
                }

                // Get a new IAstronomicalDateTime object using the client's current date and time info.
                astronomicalDateTime = DateTimeFactory.ConvertToAstronomical(clientDateTime);

                if (trajectory is IRiseSetTrajectory riseSet)
                {
                    localTime = clientDateTime.ToLocalTime();
                    riseTime = DateTimeFactory.ConvertToMutable(riseSet.GetRise(astronomicalDateTime).DateTime).ToLocalTime();

                    timeSpan = riseTime - localTime;
                    result = timeSpan.Ticks;
                }
                else
                {
                    result = long.MaxValue;
                }
            }
            catch
            {
                result = long.MaxValue;
            }

            return result;
        }
    }
}
