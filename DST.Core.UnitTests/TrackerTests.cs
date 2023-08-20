using DST.Core.Coordinate;
using DST.Core.DateAndTime;
using DST.Core.Observer;
using DST.Core.Physics;
using DST.Core.TimeKeeper;
using DST.Core.Tracker;
using DST.Core.Trajectory;

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
    }
}
