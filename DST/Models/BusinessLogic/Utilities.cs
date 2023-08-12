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
        // Calculates and returns the trajectory name for the specified DsoModel and GeolocationModel objects.
        public static string GetTrajectoryName(DsoModel model, GeolocationModel geolocation)
        {
            string result;

            try
            {
                // Get the equatorial coordinate of the DsoModel.
                IEquatorialCoordinate target = CoordinateFactory.CreateEquatorial(
                    rightAscension: new Angle(model.RightAscension), declination: new Angle(model.Declination));

                // Get the client's geographic coordinate.
                IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                    latitude: new Angle(geolocation.Latitude), longitude: new Angle(geolocation.Longitude));

                // Get the client's date and time info.
                IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId(geolocation.TimeZoneId);

                // Use the basic Greenwich Mean Sidereal Time (GMST) timekeeping algorithm.
                ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.GMST);

                // Create the observer.
                IObserver observer = ObserverFactory.Create(dateTimeInfo, location, target, timeKeeper);

                // Calculate the trajectory of the equatorial object relative to the observer's geographic location.
                ITrajectory trajectory = TrajectoryCalculator.Calculate(observer);

                // Set the name of the trajectory.
                result = trajectory.ToString();
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }

        // Returns a value indicating whether the specified DsoModel may been seen from the specified geolocation.
        public static bool IsLocal(DsoModel model, GeolocationModel geolocation)
        {
            bool result = true;

            try
            {
                // The observer's latitude must be within the constellation's visible range.
                if (geolocation.Latitude > model.Constellation.NorthernLatitude ||
                    Math.Abs(geolocation.Latitude) > model.Constellation.SouthernLatitude)
                {
                    return false;
                }

                // The trajectory of the object must be visible above the observer's horizon at some point in time.
                if (GetTrajectoryName(model, geolocation) == Core.Resources.DisplayText.TrajectoryNeverRise)
                {
                    return false;
                }

                // Get the date and time info for the client.
                IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId(geolocation.TimeZoneId);

                // Get the current month in the client's local timezone.
                int clientMonth = dateTimeInfo.Now.Value.Month;

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
    }
}
