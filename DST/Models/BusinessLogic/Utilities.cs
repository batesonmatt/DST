﻿using DST.Models.DomainModels;
using DST.Core.Coordinate;
using DST.Core.Physics;
using DST.Core.DateAndTime;
using DST.Core.TimeKeeper;
using DST.Core.Observer;
using DST.Core.Trajectory;
using System;
using System.Diagnostics;
using DST.Models.DTOs;
using DST.Models.DataLayer.Query;
using DST.Models.Extensions;

namespace DST.Models.BusinessLogic
{
    public class Utilities
    {
        /* Consider optional Algorithm algorithm = Algorithm.GMST */
        // Calculates and returns the type of trajectory for the specified DsoModel and GeolocationModel objects.
        public static ITrajectory GetTrajectory(DsoModel model, GeolocationModel geolocation)
        {
            IEquatorialCoordinate target;
            IGeographicCoordinate location;
            IDateTimeInfo dateTimeInfo;
            ITimeKeeper timeKeeper;
            IObserver observer;
            ITrajectory trajectory;

            try
            {
                // Get the equatorial coordinate of the DsoModel.
                target = CoordinateFactory.CreateEquatorial(
                    rightAscension: new Angle(TimeSpan.FromHours(model.RightAscension)), declination: new Angle(model.Declination));

                // Get the client's geographic coordinate.
                location = CoordinateFactory.CreateGeographic(
                    latitude: new Angle(geolocation.Latitude), longitude: new Angle(geolocation.Longitude));

                // Get the client's date and time info.
                dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId(geolocation.TimeZoneId);

                // Use the basic Greenwich Mean Sidereal Time (GMST) timekeeping algorithm.
                timeKeeper = TimeKeeperFactory.Create(Algorithm.GMST);

                // Create the observer.
                observer = ObserverFactory.Create(dateTimeInfo, location, target, timeKeeper);

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
        public static string GetTrajectoryName(DsoModel model, GeolocationModel geolocation)
        {
            string result = string.Empty;
            ITrajectory trajectory;

            try
            {
                trajectory = GetTrajectory(model, geolocation);

                if (trajectory is not null)
                {
                    result = GetTrajectory(model, geolocation).ToString();
                }
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }

        // Returns the primary trajectory name for the specified DsoModel and GeolocationModel objects.
        public static string GetPrimaryTrajectoryName(DsoModel model, GeolocationModel geolocation)
        {
            string result;
            ITrajectory trajectory;

            try
            {
                trajectory = GetTrajectory(model, geolocation);

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

        // Returns a value indicating whether the specified deep-sky object may be seen from the specified geolocation.
        public static bool IsLocal(DsoModel model, GeolocationModel geolocation)
        {
            bool result = true;
            ITrajectory trajectory;
            IDateTimeInfo dateTimeInfo;
            IMutableDateTime clientDateTime;
            int clientMonth;

            try
            {
                // The observer's latitude must be within the constellation's visible range.
                if (geolocation.Latitude > model.Constellation.NorthernLatitude ||
                    geolocation.Latitude < -model.Constellation.SouthernLatitude)
                {
                    return false;
                }

                // Get the object's type of trajectory relative to the observer's location.
                trajectory = GetTrajectory(model, geolocation);

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
                if (clientMonth < model.Constellation.Season.StartMonth ||
                    clientMonth > model.Constellation.Season.EndMonth)
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
        // This does not consider the affects of light pollution.
        public static bool IsVisible(DsoModel model, GeolocationModel geolocation)
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
                if (geolocation.Latitude > model.Constellation.NorthernLatitude ||
                    geolocation.Latitude < -model.Constellation.SouthernLatitude)
                {
                    return false;
                }

                // Get the object's type of trajectory relative to the observer's location.
                trajectory = GetTrajectory(model, geolocation);

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
                if (clientMonth < model.Constellation.Season.StartMonth ||
                    clientMonth > model.Constellation.Season.EndMonth)
                {
                    return false;
                }

                // Get a new IAstronomicalDateTime object using the client's current date and time info.
                astronomicalDateTime = DateTimeFactory.ConvertToAstronomical(clientDateTime);

                // Determine whether the object is above the observer's horizon.
                result = trajectory.IsAboveHorizon(astronomicalDateTime);

#if DEBUG
                if (trajectory is IRiseSetTrajectory riseSet)
                {
                    Debug.WriteLine(
                        $"{model.CompoundId} ... " +
                        $"Rise: {DateTimeFactory.ConvertToMutable(riseSet.GetRise(astronomicalDateTime).DateTime).ToLocalTime()} ... " +
                        $"Set: {DateTimeFactory.ConvertToMutable(riseSet.GetSet(astronomicalDateTime).DateTime).ToLocalTime()}");
                }
#endif
            }
            catch
            {
                result = false;
            }

            return result;
        }

        // Returns a value indicating whether the specified deep-sky object is currently rising from the observer's
        // horizon and is approaching it's apex altitude at the observer's meridian.
        public static bool IsRising(DsoModel model, GeolocationModel geolocation)
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
                if (geolocation.Latitude > model.Constellation.NorthernLatitude ||
                    geolocation.Latitude < -model.Constellation.SouthernLatitude)
                {
                    return false;
                }

                // Get the object's type of trajectory relative to the observer's location.
                trajectory = GetTrajectory(model, geolocation);

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
                if (clientMonth < model.Constellation.Season.StartMonth ||
                    clientMonth > model.Constellation.Season.EndMonth)
                {
                    return false;
                }

                // Get a new IAstronomicalDateTime object using the client's current date and time info.
                astronomicalDateTime = DateTimeFactory.ConvertToAstronomical(clientDateTime);

                if (trajectory is IRiseSetTrajectory riseSet)
                {
                    // Determine whether the object is rising from the observer's horizon.
                    result = riseSet.IsRising(astronomicalDateTime);
#if DEBUG
                    if (result)
                    {
                        DateTime localTime = clientDateTime.ToLocalTime();
                        DateTime riseTime = DateTimeFactory.ConvertToMutable(riseSet.GetRise(astronomicalDateTime).DateTime).ToLocalTime();
                        Debug.Assert(localTime > riseTime);
                        Debug.WriteLine($"{model.CompoundId}: {riseTime}");
                    }
#endif
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

        public static long GetRiseTime(DsoModel model, GeolocationModel geolocation)
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
                if (geolocation.Latitude > model.Constellation.NorthernLatitude ||
                    geolocation.Latitude < -model.Constellation.SouthernLatitude)
                {
                    return long.MaxValue;
                }

                // Get the object's type of trajectory relative to the observer's location.
                trajectory = GetTrajectory(model, geolocation);

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
                if (clientMonth < model.Constellation.Season.StartMonth ||
                    clientMonth > model.Constellation.Season.EndMonth)
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
#if DEBUG
                    string tag = timeSpan.ToString();
                    if (timeSpan.Days > 0 || timeSpan.Hours > 0) tag = string.Format("{0:F0} hr", timeSpan.TotalHours);
                    else if (timeSpan.Minutes > 0) tag = string.Format("{0} min", timeSpan.Minutes);
                    else if (timeSpan.Ticks >= 0) tag = string.Format("{0} sec", timeSpan.Seconds);
                    else if (timeSpan.Days < 0 || timeSpan.Hours < 0) tag = string.Format("{0:F0} hr ago", timeSpan.Duration().TotalHours);
                    else if (timeSpan.Minutes < 0) tag = string.Format("{0} min ago", timeSpan.Duration().Minutes);
                    else tag = string.Format("{0} sec ago", timeSpan.Duration().Seconds);
                    Debug.WriteLine($"{model.CompoundId}: {tag}");
#endif
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

            return string.Format("{0:0.0#} kly", options.Dso.Distance);
        }

        public static string GetBrightnessInfo(DsoObserverOptions options)
        {
            if (options is null || options.Dso is null)
            {
                return string.Empty;
            }

            return options.Dso.Magnitude switch
            {
                null => "Apparent Magnitude: none",
                _ => string.Format("Apparent Magnitude: {0:0.0#}", options.Dso.Magnitude)
            };
        }

        /* In Progress */
        public static string GetRiseTimeInfo(DsoObserverOptions options)
        {
            if (options is null || options.Dso is null || options.Geolocation is null)
            {
                return string.Empty;
            }

            return string.Empty;
        }

        public static Func<DsoObserverOptions, string> GetInfoFunc(string sortField)
        {
            if (sortField.EqualsIgnoreCase(Sort.Type)) return GetTypeInfo;
            if (sortField.EqualsIgnoreCase(Sort.Constellation)) return GetConstellationInfo;
            if (sortField.EqualsIgnoreCase(Sort.Distance)) return GetDistanceInfo;
            if (sortField.EqualsIgnoreCase(Sort.Brightness)) return GetBrightnessInfo;
            if (sortField.EqualsIgnoreCase(Sort.RiseTime)) return GetRiseTimeInfo;
            return x => string.Empty;
        }
    }
}
