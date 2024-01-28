using DST.Models.DomainModels;
using DST.Core.Coordinate;
using DST.Core.Physics;
using DST.Core.DateAndTime;
using DST.Core.TimeKeeper;
using DST.Core.Observer;
using DST.Core.Trajectory;
using System;
using DST.Models.DataLayer.Query;
using DST.Models.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace DST.Models.BusinessLogic
{
    public class Utilities
    {
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

        // Returns all the displayable Trajectory names.
        public static IEnumerable<string> GetTrajectoryNames()
        {
            return new string[]
            {
                Resources.DisplayText.TrajectoryCircumpolar,
                Resources.DisplayText.TrajectoryNeverRise,
                Resources.DisplayText.TrajectoryRiseAndSet
            };
        }

        // Returns all the displayable time zones.
        public static IEnumerable<TimeZoneItem> GetTimeZoneItems()
        {
            return TimeZoneInfo.GetSystemTimeZones()
                .OrderByDescending(t => t.Id == GeolocationModel.DefaultId)
                .ThenBy(t => t.BaseUtcOffset.TotalHours)
                .Select(t => new TimeZoneItem(t.Id, t.DisplayName));
        }

        // Returns all the allowable page sizes and the display text for the Search controller, List view.
        public static IEnumerable<PageSizeItem> GetSearchListPageSizeItems()
        {
            return Enumerable.Range(1, 5).Select(
                    i => new PageSizeItem(
                        i * 10,
                        string.Format(Resources.DisplayText.PageSizeFormat, i * 10)));
        }

        // Returns a value indicating whether the specified deep-sky object may be seen from the specified geolocation during the current season.
        // This does not consider the affects of light pollution, nor the magnitude of the object.
        public static bool IsLocal(DsoModel dso, GeolocationModel geolocation)
        {
            bool result = true;
            ITrajectory trajectory;
            IDateTimeInfo dateTimeInfo;
            IMutableDateTime clientDateTime;
            int clientMonth;

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

                // Get the current month in the client's local timezone.
                clientMonth = clientDateTime.ToLocalTime().Month;

                // The observer's current month must fall within the constellation's seasonal timespan.
                if (clientMonth < dso.Constellation.Season.StartMonth ||
                    clientMonth > dso.Constellation.Season.EndMonth)
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
            int clientMonth;

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

                // Get the current month in the client's local timezone.
                clientMonth = clientDateTime.ToLocalTime().Month;

                // The observer's current month must fall within the constellation's seasonal timespan.
                if (clientMonth < dso.Constellation.Season.StartMonth ||
                    clientMonth > dso.Constellation.Season.EndMonth)
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
            int clientMonth;

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

                // Get the current month in the client's local timezone.
                clientMonth = clientDateTime.ToLocalTime().Month;

                // The observer's current month must fall within the constellation's seasonal timespan.
                if (clientMonth < dso.Constellation.Season.StartMonth ||
                    clientMonth > dso.Constellation.Season.EndMonth)
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
            int clientMonth;

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

                // Get the current month in the client's local timezone.
                clientMonth = clientDateTime.ToLocalTime().Month;

                // The observer's current month must fall within the constellation's seasonal timespan.
                if (clientMonth < dso.Constellation.Season.StartMonth ||
                    clientMonth > dso.Constellation.Season.EndMonth)
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

        public static string GetTypeInfo(DsoObserverOptions options)
        {
            if (options is null || options.Dso is null)
            {
                return string.Empty;
            }

            return options.Dso.Type ?? string.Empty;
        }

        public static string GetConstellationInfo(DsoObserverOptions options)
        {
            if (options is null || options.Dso is null)
            {
                return string.Empty;
            }

            return options.Dso.ConstellationName ?? string.Empty;
        }

        public static string GetDistanceInfo(DsoObserverOptions options)
        {
            if (options is null || options.Dso is null)
            {
                return string.Empty;
            }

            return string.Format(Resources.DisplayText.DistanceFormatDecimalKly, options.Dso.Distance);
        }

        public static string GetBrightnessInfo(DsoObserverOptions options)
        {
            if (options is null || options.Dso is null)
            {
                return string.Empty;
            }

            return options.Dso.Magnitude switch
            {
                null => Resources.DisplayText.ApparentMagnitudeDefault,
                _ => string.Format(Resources.DisplayText.ApparentMagnitudeFormatDecimal, options.Dso.Magnitude)
            };
        }

        public static string GetRiseTimeInfo(DsoObserverOptions options)
        {
            if (options is null || options.Dso is null || options.Geolocation is null)
            {
                return string.Empty;
            }

            string result;
            long ticks;
            TimeSpan timeSpan;

            try
            {
                // Calculate the object's recent/next rise time relative to the observer's geolocation on the
                // current date and time for the client.
                ticks = GetRiseTime(options.Dso, options.Geolocation);

                // Get the rise time duration as a timespan.
                timeSpan = TimeSpan.FromTicks(ticks);

                // The rise time could not be calculated.
                if (timeSpan == TimeSpan.MaxValue)
                {
                    return Resources.DisplayText.RiseTimeDefault;
                }

                // Format the result.
                if (timeSpan.Days > 0 || timeSpan.Hours > 0)
                {
                    result = string.Format(Resources.DisplayText.RiseTimeFormatHoursFuture, timeSpan.TotalHours);
                }
                else if (timeSpan.Minutes > 0)
                {
                    result = string.Format(Resources.DisplayText.RiseTimeFormatMinutesFuture, timeSpan.Minutes);
                }
                else if (timeSpan.Ticks >= 0)
                {
                    result = string.Format(Resources.DisplayText.RiseTimeFormatSecondsFuture, timeSpan.Seconds);
                }
                else if (timeSpan.Days < 0 || timeSpan.Hours < 0)
                {
                    result = string.Format(Resources.DisplayText.RiseTimeFormatHoursPast, timeSpan.Duration().TotalHours);
                }
                else if (timeSpan.Minutes < 0)
                {
                    result = string.Format(Resources.DisplayText.RiseTimeFormatMinutesPast, timeSpan.Duration().Minutes);
                }
                else
                {
                    result = string.Format(Resources.DisplayText.RiseTimeFormatSecondsPast, timeSpan.Duration().Seconds);
                }
            }
            catch
            {
                result = Resources.DisplayText.RiseTimeDefault;
            }

            return result;
        }

        public static string GetSortTag(DsoObserverOptions options, string sortField)
        {
            if (sortField.EqualsSeo(Sort.Type)) return GetTypeInfo(options);
            if (sortField.EqualsSeo(Sort.Constellation)) return GetConstellationInfo(options);
            if (sortField.EqualsSeo(Sort.Distance)) return GetDistanceInfo(options);
            if (sortField.EqualsSeo(Sort.Brightness)) return GetBrightnessInfo(options);
            if (sortField.EqualsSeo(Sort.RiseTime)) return GetRiseTimeInfo(options);
            return string.Empty;
        }
    }
}
