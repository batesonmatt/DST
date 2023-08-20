using DST.Core.Coordinate;
using DST.Core.DateAndTime;
using DST.Core.Observer;
using DST.Core.Physics;
using DST.Core.TimeKeeper;
using DST.Core.Trajectory;
using DST.Core.Vector;

namespace DST.Core.UnitTests
{
    [TestClass]
    public class Xinjiang_M17_TrajectoryTests
    {
        [TestMethod]
        public void Xinjiang_M17_GMST_Trajectory_Rising_August18_2023_1800()
        {
            // Arrange

            // LAT: 45.0, LON: 90.0 (Xinjiang, China)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(90.0), latitude: new Angle(45.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // China Standard Time (IANA: Asia/Shanghai)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Asia/Shanghai");

            // Greenwich Mean Sidereal Time (GMST)
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.GMST);
            IObserver observer = ObserverFactory.Create(dateTimeInfo, location, m17, timeKeeper);

            // August 18, 2023, 6:00 PM
            DateTime localDateTime = new(2023, 8, 18, 18, 0, 0, DateTimeKind.Unspecified);
            IAstronomicalDateTime dateTime = DateTimeFactory.CreateAstronomical(localDateTime, dateTimeInfo);

            // Act
            ITrajectory trajectory = TrajectoryCalculator.Calculate(observer);
            IRiseSetTrajectory riseSet = (IRiseSetTrajectory)trajectory;
            IVector rise = riseSet.GetRise(dateTime);
            IVector apex = riseSet.GetApex(DateTimeFactory.ConvertToAstronomical(rise.DateTime));
            IVector set = riseSet.GetSet(DateTimeFactory.ConvertToAstronomical(rise.DateTime));
            ICoordinate riseCoordinate = rise.Coordinate;
            ICoordinate apexCoordinate = apex.Coordinate;
            ICoordinate setCoordinate = set.Coordinate;
            IMutableDateTime riseDateTime = DateTimeFactory.ConvertToMutable(rise.DateTime);
            IMutableDateTime apexDateTime = DateTimeFactory.ConvertToMutable(apex.DateTime);
            IMutableDateTime setDateTime = DateTimeFactory.ConvertToMutable(set.DateTime);
            DateTime localRiseDateTime = riseDateTime.ToLocalTime();
            DateTime localApexDateTime = apexDateTime.ToLocalTime();
            DateTime localSetDateTime = setDateTime.ToLocalTime();

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsTrue(riseSet.IsAboveHorizon(dateTime));
            Assert.IsTrue(riseSet.IsRising(dateTime));
            Assert.IsFalse(riseSet.IsSetting(dateTime));
            Assert.AreEqual(-6.3278594097937457E-07, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361508303682, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999998, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.0979728116676357E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638303388296, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279485124520000, riseDateTime.Ticks);
            Assert.AreEqual(638279660173900000, apexDateTime.Ticks);
            Assert.AreEqual(638279835223280000, setDateTime.Ticks);
            Assert.AreEqual(638279773124520000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279948173900000, localApexDateTime.Ticks);
            Assert.AreEqual(638280123223280000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Xinjiang_M17_GMST_Trajectory_Setting_August19_2023_0100()
        {
            // Arrange

            // LAT: 45.0, LON: 90.0 (Xinjiang, China)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(90.0), latitude: new Angle(45.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // China Standard Time (IANA: Asia/Shanghai)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Asia/Shanghai");

            // Greenwich Mean Sidereal Time (GMST)
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.GMST);
            IObserver observer = ObserverFactory.Create(dateTimeInfo, location, m17, timeKeeper);

            // August 19, 2023, 1:00 AM
            DateTime localDateTime = new(2023, 8, 19, 1, 0, 0, DateTimeKind.Unspecified);
            IAstronomicalDateTime dateTime = DateTimeFactory.CreateAstronomical(localDateTime, dateTimeInfo);

            // Act
            ITrajectory trajectory = TrajectoryCalculator.Calculate(observer);
            IRiseSetTrajectory riseSet = (IRiseSetTrajectory)trajectory;
            IVector rise = riseSet.GetRise(dateTime);
            IVector apex = riseSet.GetApex(DateTimeFactory.ConvertToAstronomical(rise.DateTime));
            IVector set = riseSet.GetSet(DateTimeFactory.ConvertToAstronomical(rise.DateTime));
            ICoordinate riseCoordinate = rise.Coordinate;
            ICoordinate apexCoordinate = apex.Coordinate;
            ICoordinate setCoordinate = set.Coordinate;
            IMutableDateTime riseDateTime = DateTimeFactory.ConvertToMutable(rise.DateTime);
            IMutableDateTime apexDateTime = DateTimeFactory.ConvertToMutable(apex.DateTime);
            IMutableDateTime setDateTime = DateTimeFactory.ConvertToMutable(set.DateTime);
            DateTime localRiseDateTime = riseDateTime.ToLocalTime();
            DateTime localApexDateTime = apexDateTime.ToLocalTime();
            DateTime localSetDateTime = setDateTime.ToLocalTime();

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsTrue(riseSet.IsAboveHorizon(dateTime));
            Assert.IsFalse(riseSet.IsRising(dateTime));
            Assert.IsTrue(riseSet.IsSetting(dateTime));
            Assert.AreEqual(-6.3278594097937457E-07, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361508303682, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999998, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.0979728116676357E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638303388296, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279485124520000, riseDateTime.Ticks);
            Assert.AreEqual(638279660173900000, apexDateTime.Ticks);
            Assert.AreEqual(638279835223280000, setDateTime.Ticks);
            Assert.AreEqual(638279773124520000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279948173900000, localApexDateTime.Ticks);
            Assert.AreEqual(638280123223280000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Xinjiang_M17_GMST_Trajectory_AntiRising_August19_2023_0600()
        {
            // Arrange

            // LAT: 45.0, LON: 90.0 (Xinjiang, China)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(90.0), latitude: new Angle(45.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // China Standard Time (IANA: Asia/Shanghai)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Asia/Shanghai");

            // Greenwich Mean Sidereal Time (GMST)
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.GMST);
            IObserver observer = ObserverFactory.Create(dateTimeInfo, location, m17, timeKeeper);

            // August 19, 2023, 6:00 AM
            DateTime localDateTime = new(2023, 8, 19, 6, 0, 0, DateTimeKind.Unspecified);
            IAstronomicalDateTime dateTime = DateTimeFactory.CreateAstronomical(localDateTime, dateTimeInfo);

            // Act
            ITrajectory trajectory = TrajectoryCalculator.Calculate(observer);
            IRiseSetTrajectory riseSet = (IRiseSetTrajectory)trajectory;
            IVector rise = riseSet.GetRise(dateTime);
            IVector apex = riseSet.GetApex(DateTimeFactory.ConvertToAstronomical(rise.DateTime));
            IVector set = riseSet.GetSet(DateTimeFactory.ConvertToAstronomical(rise.DateTime));
            ICoordinate riseCoordinate = rise.Coordinate;
            ICoordinate apexCoordinate = apex.Coordinate;
            ICoordinate setCoordinate = set.Coordinate;
            IMutableDateTime riseDateTime = DateTimeFactory.ConvertToMutable(rise.DateTime);
            IMutableDateTime apexDateTime = DateTimeFactory.ConvertToMutable(apex.DateTime);
            IMutableDateTime setDateTime = DateTimeFactory.ConvertToMutable(set.DateTime);
            DateTime localRiseDateTime = riseDateTime.ToLocalTime();
            DateTime localApexDateTime = apexDateTime.ToLocalTime();
            DateTime localSetDateTime = setDateTime.ToLocalTime();

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsFalse(riseSet.IsAboveHorizon(dateTime));
            Assert.IsFalse(riseSet.IsRising(dateTime));
            Assert.IsFalse(riseSet.IsSetting(dateTime));
            Assert.AreEqual(-2.074191968404193E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361351477486, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999916, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99999627848695, apexCoordinate.Components.Rotation);
            Assert.AreEqual(2.539377644764488E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638146562223, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280346765420000, riseDateTime.Ticks);
            Assert.AreEqual(638280521814800000, apexDateTime.Ticks);
            Assert.AreEqual(638280696864180000, setDateTime.Ticks);
            Assert.AreEqual(638280634765420000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280809814800000, localApexDateTime.Ticks);
            Assert.AreEqual(638280984864180000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Xinjiang_M17_GMST_Trajectory_AntiSetting_August19_2023_1300()
        {
            // Arrange

            // LAT: 45.0, LON: 90.0 (Xinjiang, China)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(90.0), latitude: new Angle(45.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // China Standard Time (IANA: Asia/Shanghai)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Asia/Shanghai");

            // Greenwich Mean Sidereal Time (GMST)
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.GMST);
            IObserver observer = ObserverFactory.Create(dateTimeInfo, location, m17, timeKeeper);

            // August 19, 2023, 1:00 PM
            DateTime localDateTime = new(2023, 8, 19, 13, 0, 0, DateTimeKind.Unspecified);
            IAstronomicalDateTime dateTime = DateTimeFactory.CreateAstronomical(localDateTime, dateTimeInfo);

            // Act
            ITrajectory trajectory = TrajectoryCalculator.Calculate(observer);
            IRiseSetTrajectory riseSet = (IRiseSetTrajectory)trajectory;
            IVector rise = riseSet.GetRise(dateTime);
            IVector apex = riseSet.GetApex(DateTimeFactory.ConvertToAstronomical(rise.DateTime));
            IVector set = riseSet.GetSet(DateTimeFactory.ConvertToAstronomical(rise.DateTime));
            ICoordinate riseCoordinate = rise.Coordinate;
            ICoordinate apexCoordinate = apex.Coordinate;
            ICoordinate setCoordinate = set.Coordinate;
            IMutableDateTime riseDateTime = DateTimeFactory.ConvertToMutable(rise.DateTime);
            IMutableDateTime apexDateTime = DateTimeFactory.ConvertToMutable(apex.DateTime);
            IMutableDateTime setDateTime = DateTimeFactory.ConvertToMutable(set.DateTime);
            DateTime localRiseDateTime = riseDateTime.ToLocalTime();
            DateTime localApexDateTime = apexDateTime.ToLocalTime();
            DateTime localSetDateTime = setDateTime.ToLocalTime();

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsFalse(riseSet.IsAboveHorizon(dateTime));
            Assert.IsFalse(riseSet.IsRising(dateTime));
            Assert.IsFalse(riseSet.IsSetting(dateTime));
            Assert.AreEqual(-2.074191968404193E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361351477486, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999916, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99999627848695, apexCoordinate.Components.Rotation);
            Assert.AreEqual(2.539377644764488E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638146562223, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280346765420000, riseDateTime.Ticks);
            Assert.AreEqual(638280521814800000, apexDateTime.Ticks);
            Assert.AreEqual(638280696864180000, setDateTime.Ticks);
            Assert.AreEqual(638280634765420000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280809814800000, localApexDateTime.Ticks);
            Assert.AreEqual(638280984864180000, localSetDateTime.Ticks);
        }
    }
}
