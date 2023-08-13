using DST.Models.DomainModels;
using DST.Core.Coordinate;
using DST.Core.Physics;
using DST.Core.DateAndTime;
using DST.Core.TimeKeeper;
using DST.Core.Observer;
using DST.Core.Trajectory;
using System;

namespace DST.Models.BusinessLogic
{
    public class Utilities
    {
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
                    rightAscension: new Angle(model.RightAscension), declination: new Angle(model.Declination));

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
#if DEBUG
                    System.Diagnostics.Debug.WriteLine($"{model.CompoundId}: Circumpolar");
#endif
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
                    System.Diagnostics.Debug.WriteLine(
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
    }
}
