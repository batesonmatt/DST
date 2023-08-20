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

        [TestMethod]
        public void Xinjiang_M17_GAST_Trajectory_Rising_August18_2023_1800()
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

            // Greenwich Apparent Sidereal Time (GAST)
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.GAST);
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
            Assert.AreEqual(-0.001756805416221141, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20522868599068, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.82094612246971, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99999290802347, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-0.0017514356272840814, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.7947591447395, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279487324190000, riseDateTime.Ticks);
            Assert.AreEqual(638279662373570000, apexDateTime.Ticks);
            Assert.AreEqual(638279837422950000, setDateTime.Ticks);
            Assert.AreEqual(638279775324190000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279950373570000, localApexDateTime.Ticks);
            Assert.AreEqual(638280125422950000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Xinjiang_M17_GAST_Trajectory_Setting_August19_2023_0100()
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

            // Greenwich Apparent Sidereal Time (GAST)
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.GAST);
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
            Assert.AreEqual(-0.0017513747444581895, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20523459460823, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.82094612246971, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99999290802347, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-0.0017514356272840814, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.7947591447395, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279487324210000, riseDateTime.Ticks);
            Assert.AreEqual(638279662373570000, apexDateTime.Ticks);
            Assert.AreEqual(638279837422950000, setDateTime.Ticks);
            Assert.AreEqual(638279775324210000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279950373570000, localApexDateTime.Ticks);
            Assert.AreEqual(638280125422950000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Xinjiang_M17_GAST_Trajectory_AntiRising_August19_2023_0600()
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

            // Greenwich Apparent Sidereal Time (GAST)
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.GAST);
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
            Assert.AreEqual(-0.0017724167350934295, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20522476499966, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.82093845244833, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99999104554647, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-0.0017550758926745402, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79475352206535, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280348965120000, riseDateTime.Ticks);
            Assert.AreEqual(638280524014530000, apexDateTime.Ticks);
            Assert.AreEqual(638280699063910000, setDateTime.Ticks);
            Assert.AreEqual(638280636965120000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280812014530000, localApexDateTime.Ticks);
            Assert.AreEqual(638280987063910000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Xinjiang_M17_GAST_Trajectory_AntiSetting_August19_2023_1300()
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

            // Greenwich Apparent Sidereal Time (GAST)
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.GAST);
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
            Assert.AreEqual(-0.0017669860619662359, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20523067361772, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.82093845244833, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99999104554647, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-0.0017550758926745402, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79475352206535, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280348965140000, riseDateTime.Ticks);
            Assert.AreEqual(638280524014530000, apexDateTime.Ticks);
            Assert.AreEqual(638280699063910000, setDateTime.Ticks);
            Assert.AreEqual(638280636965140000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280812014530000, localApexDateTime.Ticks);
            Assert.AreEqual(638280987063910000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Xinjiang_M17_ERA_Trajectory_Rising_August18_2023_1800()
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

            // Earth Rotation Angle (ERA)
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.ERA);
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
            Assert.AreEqual(-2.458126971305319E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361309705034, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999998, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-8.372019806301978E-07, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638513936956, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279485128550000, riseDateTime.Ticks);
            Assert.AreEqual(638279660177950000, apexDateTime.Ticks);
            Assert.AreEqual(638279835227350000, setDateTime.Ticks);
            Assert.AreEqual(638279773128550000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279948177950000, localApexDateTime.Ticks);
            Assert.AreEqual(638280123227350000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Xinjiang_M17_ERA_Trajectory_Setting_August19_2023_0100()
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

            // Earth Rotation Angle (ERA)
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.ERA);
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
            Assert.AreEqual(-2.458126971305319E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361309705034, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999998, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-8.372019806301978E-07, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638513936956, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279485128550000, riseDateTime.Ticks);
            Assert.AreEqual(638279660177950000, apexDateTime.Ticks);
            Assert.AreEqual(638279835227350000, setDateTime.Ticks);
            Assert.AreEqual(638279773128550000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279948177950000, localApexDateTime.Ticks);
            Assert.AreEqual(638280123227350000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Xinjiang_M17_ERA_Trajectory_AntiRising_August19_2023_0600()
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

            // Earth Rotation Angle (ERA)
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.ERA);
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
            Assert.AreEqual(-9.201033890349208E-08, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361567140527, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.82333329999998, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99999809090409, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-2.3497210577261285E-07, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638448413834, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280346769530000, riseDateTime.Ticks);
            Assert.AreEqual(638280521818920000, apexDateTime.Ticks);
            Assert.AreEqual(638280696868320000, setDateTime.Ticks);
            Assert.AreEqual(638280634769530000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280809818920000, localApexDateTime.Ticks);
            Assert.AreEqual(638280984868320000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Xinjiang_M17_ERA_Trajectory_AntiSetting_August19_2023_1300()
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

            // Earth Rotation Angle (ERA)
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.ERA);
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
            Assert.AreEqual(-9.201033890349208E-08, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361567140527, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.82333329999998, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99999809090409, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-2.3497210577261285E-07, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638448413834, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280346769530000, riseDateTime.Ticks);
            Assert.AreEqual(638280521818920000, apexDateTime.Ticks);
            Assert.AreEqual(638280696868320000, setDateTime.Ticks);
            Assert.AreEqual(638280634769530000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280809818920000, localApexDateTime.Ticks);
            Assert.AreEqual(638280984868320000, localSetDateTime.Ticks);
        }
    }
}