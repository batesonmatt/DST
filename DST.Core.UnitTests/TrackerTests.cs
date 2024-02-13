using DST.Core.Coordinate;
using DST.Core.DateAndTime;
using DST.Core.Observer;
using DST.Core.Physics;
using DST.Core.TimeKeeper;
using DST.Core.Tracker;
using DST.Core.Trajectory;
using DST.Core.Vector;

namespace DST.Core.UnitTests
{
    [TestClass]
    public class TrackerTests
    {
        [TestMethod]
        public void Birmingham_M13_GMST_August10_1998_2310()
        {
            // Arrange

            // LAT: 52.5, LON: -1.9166667
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-1.9166667), latitude: new Angle(52.5));

            // RA: 16.695hr, DEC: 36°28'
            IEquatorialCoordinate m13 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(16.695)), declination: new Angle(36, 28));

            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.GMST);
            IObserver observer = ObserverFactory.Create(dateTimeInfo, location, m13, timeKeeper);

            DateTime localDateTime = new(1998, 8, 10, 18, 10, 0, DateTimeKind.Unspecified);
            IAstronomicalDateTime dateTime = DateTimeFactory.CreateAstronomical(localDateTime, dateTimeInfo);

            IMutableDateTime mutable = DateTimeFactory.ConvertToMutable(dateTime);
            ITracker tracker = TrackerFactory.Create(observer);

            // Act
            ICoordinate position = tracker.Track(dateTime);
            ITrajectory trajectory = TrajectoryCalculator.Calculate(observer);

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsInstanceOfType(position, typeof(IHorizontalCoordinate));
            Assert.AreEqual(localDateTime, mutable.ToLocalTime());
            Assert.AreEqual(49.1688685443092, position.Components.Inclination);
            Assert.AreEqual(269.14666902043496, position.Components.Rotation);

            // Display results.
            //
            // Visibility: Rise and Set
            // Location: 52°30'00"N 1°55'00"W => 52.5°N 1.9167°W => 53°N 2°W
            // Target: 16h 41m 42s, +36°28'00" => 16.695h, +36.4667° => 17h, +36°
            // Period: 8 / 10 / 1998 6:10:00 PM
            // Position: +49°10'07.927", 269°08'48.008" => +49.1689°, 269.1467° => +49°, 269°
            //
            Console.WriteLine($"Visibility: {trajectory}");
            Console.WriteLine($"Location: {observer.Origin} => {observer.Origin.Format(FormatType.Decimal)} => {observer.Origin.Format(FormatType.Compact)}");
            Console.WriteLine($"Target: {observer.Destination} => {observer.Destination.Format(FormatType.Decimal)} => {observer.Destination.Format(FormatType.Compact)}");
            Console.WriteLine($"Period: {mutable.ToLocalTime()}");
            Console.WriteLine($"Position: {position} => {position.Format(FormatType.Decimal)} => {position.Format(FormatType.Compact)}");
        }

        [TestMethod]
        public void Manhattan_M42_GMST_MinDateTime_OutOfRange()
        {
            // Arrange

            // LAT: 40.783333°, LON: -73.966667° (Manhattan, New York)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-73.966667), latitude: new Angle(40.783333));

            // RA: 5.5881389hr, DEC: -5.3911111° (M42 - Orion Nebula)
            IEquatorialCoordinate m42 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(5.5881389)), declination: new Angle(-5.3911111));

            // Eastern Time - US & Canada (IANA: Eastern Standard Time)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Eastern Standard Time");

            // Greenwich Mean Sidereal Time (GMST)
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.GMST);
            IObserver observer = ObserverFactory.Create(dateTimeInfo, location, m42, timeKeeper);

            // January 2, 0001, 12:00 AM (UTC) - January 1, 0001, 7:00 PM (Local)
            IAstronomicalDateTime minDateTime = DateTimeFactory.ConvertToAstronomical(dateTimeInfo.MinAstronomicalDateTime);

            ITracker tracker = TrackerFactory.Create(observer);

            // Act
            ITrajectory trajectory = TrajectoryCalculator.Calculate(observer);
            IRiseSetTrajectory riseSet = (IRiseSetTrajectory)trajectory;
            ICoordinate minPosition = tracker.Track(minDateTime);
            IVector minApex = riseSet.GetApex(minDateTime);
            IVector[] minApexFuture = riseSet.GetApex(minDateTime, 1);

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsInstanceOfType(minPosition, typeof(NonTrackableCoordinate));
            Assert.AreEqual(0.0, minPosition.Components.Rotation);
            Assert.AreEqual(0.0, minPosition.Components.Inclination);
            Assert.IsInstanceOfType(minApex, typeof(NonTrackableVector));
            Assert.AreEqual(DateTimeConstants.MinUtcDateTime.Ticks, minApex.DateTime.Ticks);
            Assert.AreEqual(0.0, minApex.Coordinate.Components.Rotation);
            Assert.AreEqual(0.0, minApex.Coordinate.Components.Inclination);
            Assert.IsTrue(minApexFuture.Length == 1);
            Assert.IsInstanceOfType(minApexFuture[0], typeof(LocalVector));
            Assert.IsInstanceOfType(minApexFuture[0].Coordinate, typeof(HorizontalCoordinate));
            Assert.AreEqual(1861360678428, minApexFuture[0].DateTime.Ticks);
            Assert.AreEqual(179.99999879258172, minApexFuture[0].Coordinate.Components.Rotation);
            Assert.AreEqual(43.82555590000001, minApexFuture[0].Coordinate.Components.Inclination);
        }

        [TestMethod]
        public void Manhattan_M42_GMST_MaxDateTime_OutOfRange()
        {
            // Arrange

            // LAT: 40.783333°, LON: -73.966667° (Manhattan, New York)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-73.966667), latitude: new Angle(40.783333));

            // RA: 5.5881389hr, DEC: -5.3911111° (M42 - Orion Nebula)
            IEquatorialCoordinate m42 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(5.5881389)), declination: new Angle(-5.3911111));

            // Eastern Time - US & Canada (IANA: Eastern Standard Time)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Eastern Standard Time");

            // Greenwich Mean Sidereal Time (GMST)
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.GMST);
            IObserver observer = ObserverFactory.Create(dateTimeInfo, location, m42, timeKeeper);

            // December 30, 9999, 11:59:59 PM (UTC) - December 30, 9999, 6:59:59 PM (Local)
            IAstronomicalDateTime maxDateTime = DateTimeFactory.ConvertToAstronomical(dateTimeInfo.MaxAstronomicalDateTime);

            ITracker tracker = TrackerFactory.Create(observer);

            // Act
            ITrajectory trajectory = TrajectoryCalculator.Calculate(observer);
            IRiseSetTrajectory riseSet = (IRiseSetTrajectory)trajectory;
            ICoordinate maxPosition = tracker.Track(maxDateTime);
            IVector maxApex = riseSet.GetApex(maxDateTime);
            IVector[] maxApexPast = riseSet.GetApex(maxDateTime, -1);

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsInstanceOfType(maxPosition, typeof(NonTrackableCoordinate));
            Assert.AreEqual(0.0, maxPosition.Components.Rotation);
            Assert.AreEqual(0.0, maxPosition.Components.Inclination);
            Assert.IsInstanceOfType(maxApex, typeof(NonTrackableVector));
            Assert.AreEqual(DateTimeConstants.MaxUtcDateTime.Ticks, maxApex.DateTime.Ticks);
            Assert.AreEqual(0.0, maxApex.Coordinate.Components.Rotation);
            Assert.AreEqual(0.0, maxApex.Coordinate.Components.Inclination);
            Assert.IsTrue(maxApexPast.Length == 1);
            Assert.IsInstanceOfType(maxApexPast[0], typeof(LocalVector));
            Assert.IsInstanceOfType(maxApexPast[0].Coordinate, typeof(HorizontalCoordinate));
            Assert.AreEqual(3155377385208665045, maxApexPast[0].DateTime.Ticks);
            Assert.AreEqual(179.99999879258172, maxApexPast[0].Coordinate.Components.Rotation);
            Assert.AreEqual(43.82555590000001, maxApexPast[0].Coordinate.Components.Inclination);
        }
    }
}
