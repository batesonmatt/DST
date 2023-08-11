using DST.Models.DomainModels;
using DST.Core.Coordinate;
using DST.Core.Physics;
using DST.Core.DateAndTime;
using DST.Core.TimeKeeper;
using DST.Core.Observer;
using DST.Core.Trajectory;

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
                IEquatorialCoordinate target = CoordinateFactory.CreateEquatorial(
                    rightAscension: new Angle(model.RightAscension), declination: new Angle(model.Declination));

                IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                    latitude: new Angle(geolocation.Latitude), longitude: new Angle(geolocation.Longitude));

                IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId(geolocation.TimeZoneId);

                ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.GMST);

                IObserver observer = ObserverFactory.Create(dateTimeInfo, location, target, timeKeeper);

                ITrajectory trajectory = TrajectoryCalculator.Calculate(observer);

                result = trajectory.ToString();
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }
    }
}
