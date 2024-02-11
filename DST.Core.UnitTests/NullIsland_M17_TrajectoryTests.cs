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
    public class NullIsland_M17_TrajectoryTests
    {
        [TestMethod]
        public void NullIsland_M17_GMST_Trajectory_Rising_August18_2023_1800()
        {
            // Arrange

            // LAT: 0.0, LON: 0.0 (Null Island)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(0.0), latitude: new Angle(0.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Universal Coordinated Time (IANA: UTC)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("UTC");

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
            DateTime localRiseDateTime = rise.DateTime.ToLocalTime();
            DateTime localApexDateTime = apex.DateTime.ToLocalTime();
            DateTime localSetDateTime = set.DateTime.ToLocalTime();

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsInstanceOfType(rise, typeof(LocalVector));
            Assert.IsInstanceOfType(apex, typeof(LocalVector));
            Assert.IsInstanceOfType(set, typeof(LocalVector));
            Assert.IsTrue(riseSet.IsAboveHorizon(dateTime));
            Assert.IsTrue(riseSet.IsRising(dateTime));
            Assert.IsFalse(riseSet.IsSetting(dateTime));
            Assert.AreEqual(-1.2788185586032341E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17666670000001, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.82333329999952, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99998623331362, apexCoordinate.Components.Rotation);
            Assert.AreEqual(2.3438417208062966E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.8233333, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279660173900000, rise.DateTime.Ticks);
            Assert.AreEqual(638279875584120000, apex.DateTime.Ticks);
            Assert.AreEqual(638280090994350000, set.DateTime.Ticks);
            Assert.AreEqual(638279660173900000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279875584120000, localApexDateTime.Ticks);
            Assert.AreEqual(638280090994350000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void NullIsland_M17_GMST_Trajectory_Setting_August19_2023_0100()
        {
            // Arrange

            // LAT: 0.0, LON: 0.0 (Null Island)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(0.0), latitude: new Angle(0.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Universal Coordinated Time (IANA: UTC)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("UTC");

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
            DateTime localRiseDateTime = rise.DateTime.ToLocalTime();
            DateTime localApexDateTime = apex.DateTime.ToLocalTime();
            DateTime localSetDateTime = set.DateTime.ToLocalTime();

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsInstanceOfType(rise, typeof(LocalVector));
            Assert.IsInstanceOfType(apex, typeof(LocalVector));
            Assert.IsInstanceOfType(set, typeof(LocalVector));
            Assert.IsTrue(riseSet.IsAboveHorizon(dateTime));
            Assert.IsFalse(riseSet.IsRising(dateTime));
            Assert.IsTrue(riseSet.IsSetting(dateTime));
            Assert.AreEqual(-1.2788185586032341E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17666670000001, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.82333329999952, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99998623331362, apexCoordinate.Components.Rotation);
            Assert.AreEqual(2.3438417208062966E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.8233333, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279660173900000, rise.DateTime.Ticks);
            Assert.AreEqual(638279875584120000, apex.DateTime.Ticks);
            Assert.AreEqual(638280090994350000, set.DateTime.Ticks);
            Assert.AreEqual(638279660173900000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279875584120000, localApexDateTime.Ticks);
            Assert.AreEqual(638280090994350000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void NullIsland_M17_GMST_Trajectory_AntiRising_August19_2023_0600()
        {
            // Arrange

            // LAT: 0.0, LON: 0.0 (Null Island)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(0.0), latitude: new Angle(0.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Universal Coordinated Time (IANA: UTC)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("UTC");

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
            DateTime localRiseDateTime = rise.DateTime.ToLocalTime();
            DateTime localApexDateTime = apex.DateTime.ToLocalTime();
            DateTime localSetDateTime = set.DateTime.ToLocalTime();

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsInstanceOfType(rise, typeof(LocalVector));
            Assert.IsInstanceOfType(apex, typeof(LocalVector));
            Assert.IsInstanceOfType(set, typeof(LocalVector));
            Assert.IsFalse(riseSet.IsAboveHorizon(dateTime));
            Assert.IsFalse(riseSet.IsRising(dateTime));
            Assert.IsFalse(riseSet.IsSetting(dateTime));
            Assert.AreEqual(-3.4088648703800573E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17666670000003, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.82333329999987, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99999306391013, apexCoordinate.Components.Rotation);
            Assert.AreEqual(4.612381177750648E-07, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.8233333, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280521814800000, rise.DateTime.Ticks);
            Assert.AreEqual(638280737225030000, apex.DateTime.Ticks);
            Assert.AreEqual(638280952635260000, set.DateTime.Ticks);
            Assert.AreEqual(638280521814800000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280737225030000, localApexDateTime.Ticks);
            Assert.AreEqual(638280952635260000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void NullIsland_M17_GMST_Trajectory_AntiSetting_August19_2023_1300()
        {
            // Arrange

            // LAT: 0.0, LON: 0.0 (Null Island)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(0.0), latitude: new Angle(0.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Universal Coordinated Time (IANA: UTC)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("UTC");

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
            DateTime localRiseDateTime = rise.DateTime.ToLocalTime();
            DateTime localApexDateTime = apex.DateTime.ToLocalTime();
            DateTime localSetDateTime = set.DateTime.ToLocalTime();

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsInstanceOfType(rise, typeof(LocalVector));
            Assert.IsInstanceOfType(apex, typeof(LocalVector));
            Assert.IsInstanceOfType(set, typeof(LocalVector));
            Assert.IsFalse(riseSet.IsAboveHorizon(dateTime));
            Assert.IsFalse(riseSet.IsRising(dateTime));
            Assert.IsFalse(riseSet.IsSetting(dateTime));
            Assert.AreEqual(-3.4088648703800573E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17666670000003, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.82333329999987, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99999306391013, apexCoordinate.Components.Rotation);
            Assert.AreEqual(4.612381177750648E-07, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.8233333, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280521814800000, rise.DateTime.Ticks);
            Assert.AreEqual(638280737225030000, apex.DateTime.Ticks);
            Assert.AreEqual(638280952635260000, set.DateTime.Ticks);
            Assert.AreEqual(638280521814800000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280737225030000, localApexDateTime.Ticks);
            Assert.AreEqual(638280952635260000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void NullIsland_M17_GAST_Trajectory_Rising_August18_2023_1800()
        {
            // Arrange

            // LAT: 0.0, LON: 0.0 (Null Island)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(0.0), latitude: new Angle(0.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Universal Coordinated Time (IANA: UTC)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("UTC");

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
            DateTime localRiseDateTime = rise.DateTime.ToLocalTime();
            DateTime localApexDateTime = apex.DateTime.ToLocalTime();
            DateTime localSetDateTime = set.DateTime.ToLocalTime();

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsInstanceOfType(rise, typeof(LocalVector));
            Assert.IsInstanceOfType(apex, typeof(LocalVector));
            Assert.IsInstanceOfType(set, typeof(LocalVector));
            Assert.IsTrue(riseSet.IsAboveHorizon(dateTime));
            Assert.IsTrue(riseSet.IsRising(dateTime));
            Assert.IsFalse(riseSet.IsSetting(dateTime));
            Assert.AreEqual(1.7903214445005047E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17905387753025, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.8209436616456, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.9999765406367, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.499989932962527E-05, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.82094155858914, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279662373590000, rise.DateTime.Ticks);
            Assert.AreEqual(638279877783810000, apex.DateTime.Ticks);
            Assert.AreEqual(638280093194030000, set.DateTime.Ticks);
            Assert.AreEqual(638279662373590000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279877783810000, localApexDateTime.Ticks);
            Assert.AreEqual(638280093194030000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void NullIsland_M17_GAST_Trajectory_Setting_August19_2023_0100()
        {
            // Arrange

            // LAT: 0.0, LON: 0.0 (Null Island)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(0.0), latitude: new Angle(0.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Universal Coordinated Time (IANA: UTC)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("UTC");

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
            DateTime localRiseDateTime = rise.DateTime.ToLocalTime();
            DateTime localApexDateTime = apex.DateTime.ToLocalTime();
            DateTime localSetDateTime = set.DateTime.ToLocalTime();

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsInstanceOfType(rise, typeof(LocalVector));
            Assert.IsInstanceOfType(apex, typeof(LocalVector));
            Assert.IsInstanceOfType(set, typeof(LocalVector));
            Assert.IsTrue(riseSet.IsAboveHorizon(dateTime));
            Assert.IsFalse(riseSet.IsRising(dateTime));
            Assert.IsTrue(riseSet.IsSetting(dateTime));
            Assert.AreEqual(9.815527150936435E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17905387753076, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.8209436616456, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.9999765406367, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.499989932962527E-05, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.82094155858914, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279662373610000, rise.DateTime.Ticks);
            Assert.AreEqual(638279877783810000, apex.DateTime.Ticks);
            Assert.AreEqual(638280093194030000, set.DateTime.Ticks);
            Assert.AreEqual(638279662373610000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279877783810000, localApexDateTime.Ticks);
            Assert.AreEqual(638280093194030000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void NullIsland_M17_GAST_Trajectory_AntiRising_August19_2023_0600()
        {
            // Arrange

            // LAT: 0.0, LON: 0.0 (Null Island)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(0.0), latitude: new Angle(0.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Universal Coordinated Time (IANA: UTC)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("UTC");

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
            DateTime localRiseDateTime = rise.DateTime.ToLocalTime();
            DateTime localApexDateTime = apex.DateTime.ToLocalTime();
            DateTime localSetDateTime = set.DateTime.ToLocalTime();

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsInstanceOfType(rise, typeof(LocalVector));
            Assert.IsInstanceOfType(apex, typeof(LocalVector));
            Assert.IsInstanceOfType(set, typeof(LocalVector));
            Assert.IsFalse(riseSet.IsAboveHorizon(dateTime));
            Assert.IsFalse(riseSet.IsRising(dateTime));
            Assert.IsFalse(riseSet.IsSetting(dateTime));
            Assert.AreEqual(-1.1889497613992717E-05, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17906154755153, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.82093745078836, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99997057266282, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.2365199067357612E-05, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.82093680973117, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280524014520000, rise.DateTime.Ticks);
            Assert.AreEqual(638280739424770000, apex.DateTime.Ticks);
            Assert.AreEqual(638280954835000000, set.DateTime.Ticks);
            Assert.AreEqual(638280524014520000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280739424770000, localApexDateTime.Ticks);
            Assert.AreEqual(638280954835000000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void NullIsland_M17_GAST_Trajectory_AntiSetting_August19_2023_1300()
        {
            // Arrange

            // LAT: 0.0, LON: 0.0 (Null Island)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(0.0), latitude: new Angle(0.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Universal Coordinated Time (IANA: UTC)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("UTC");

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
            DateTime localRiseDateTime = rise.DateTime.ToLocalTime();
            DateTime localApexDateTime = apex.DateTime.ToLocalTime();
            DateTime localSetDateTime = set.DateTime.ToLocalTime();

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsInstanceOfType(rise, typeof(LocalVector));
            Assert.IsInstanceOfType(apex, typeof(LocalVector));
            Assert.IsInstanceOfType(set, typeof(LocalVector));
            Assert.IsFalse(riseSet.IsAboveHorizon(dateTime));
            Assert.IsFalse(riseSet.IsRising(dateTime));
            Assert.IsFalse(riseSet.IsSetting(dateTime));
            Assert.AreEqual(-3.864288771637803E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.1790615475513, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.82093745078836, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99997057266282, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.2365199067357612E-05, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.82093680973117, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280524014540000, rise.DateTime.Ticks);
            Assert.AreEqual(638280739424770000, apex.DateTime.Ticks);
            Assert.AreEqual(638280954835000000, set.DateTime.Ticks);
            Assert.AreEqual(638280524014540000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280739424770000, localApexDateTime.Ticks);
            Assert.AreEqual(638280954835000000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void NullIsland_M17_ERA_Trajectory_Rising_August18_2023_1800()
        {
            // Arrange

            // LAT: 0.0, LON: 0.0 (Null Island)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(0.0), latitude: new Angle(0.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Universal Coordinated Time (IANA: UTC)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("UTC");

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
            DateTime localRiseDateTime = rise.DateTime.ToLocalTime();
            DateTime localApexDateTime = apex.DateTime.ToLocalTime();
            DateTime localSetDateTime = set.DateTime.ToLocalTime();

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsInstanceOfType(rise, typeof(LocalVector));
            Assert.IsInstanceOfType(apex, typeof(LocalVector));
            Assert.IsInstanceOfType(set, typeof(LocalVector));
            Assert.IsTrue(riseSet.IsAboveHorizon(dateTime));
            Assert.IsTrue(riseSet.IsRising(dateTime));
            Assert.IsFalse(riseSet.IsSetting(dateTime));
            Assert.AreEqual(-1.1233640293539793E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17666670000001, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.8233332999999, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000627392936, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-5.913634026910586E-07, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.8233333, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279660177950000, rise.DateTime.Ticks);
            Assert.AreEqual(638279875588200000, apex.DateTime.Ticks);
            Assert.AreEqual(638280090998440000, set.DateTime.Ticks);
            Assert.AreEqual(638279660177950000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279875588200000, localApexDateTime.Ticks);
            Assert.AreEqual(638280090998440000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void NullIsland_M17_ERA_Trajectory_Setting_August19_2023_0100()
        {
            // Arrange

            // LAT: 0.0, LON: 0.0 (Null Island)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(0.0), latitude: new Angle(0.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Universal Coordinated Time (IANA: UTC)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("UTC");

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
            DateTime localRiseDateTime = rise.DateTime.ToLocalTime();
            DateTime localApexDateTime = apex.DateTime.ToLocalTime();
            DateTime localSetDateTime = set.DateTime.ToLocalTime();

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsInstanceOfType(rise, typeof(LocalVector));
            Assert.IsInstanceOfType(apex, typeof(LocalVector));
            Assert.IsInstanceOfType(set, typeof(LocalVector));
            Assert.IsTrue(riseSet.IsAboveHorizon(dateTime));
            Assert.IsFalse(riseSet.IsRising(dateTime));
            Assert.IsTrue(riseSet.IsSetting(dateTime));
            Assert.AreEqual(-5.136015317930287E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17666670000007, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.8233332999999, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000627392936, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-5.913634026910586E-07, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.8233333, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279660177940000, rise.DateTime.Ticks);
            Assert.AreEqual(638279875588200000, apex.DateTime.Ticks);
            Assert.AreEqual(638280090998440000, set.DateTime.Ticks);
            Assert.AreEqual(638279660177940000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279875588200000, localApexDateTime.Ticks);
            Assert.AreEqual(638280090998440000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void NullIsland_M17_ERA_Trajectory_AntiRising_August19_2023_0600()
        {
            // Arrange

            // LAT: 0.0, LON: 0.0 (Null Island)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(0.0), latitude: new Angle(0.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Universal Coordinated Time (IANA: UTC)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("UTC");

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
            DateTime localRiseDateTime = rise.DateTime.ToLocalTime();
            DateTime localApexDateTime = apex.DateTime.ToLocalTime();
            DateTime localSetDateTime = set.DateTime.ToLocalTime();

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsInstanceOfType(rise, typeof(LocalVector));
            Assert.IsInstanceOfType(apex, typeof(LocalVector));
            Assert.IsInstanceOfType(set, typeof(LocalVector));
            Assert.IsFalse(riseSet.IsAboveHorizon(dateTime));
            Assert.IsFalse(riseSet.IsRising(dateTime));
            Assert.IsFalse(riseSet.IsSetting(dateTime));
            Assert.AreEqual(2.0752263623342483E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17666670000001, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.82333329999999, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000307832465, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.0529158394190949E-08, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.8233333, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280521818930000, rise.DateTime.Ticks);
            Assert.AreEqual(638280737229170000, apex.DateTime.Ticks);
            Assert.AreEqual(638280952639410000, set.DateTime.Ticks);
            Assert.AreEqual(638280521818930000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280737229170000, localApexDateTime.Ticks);
            Assert.AreEqual(638280952639410000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void NullIsland_M17_ERA_Trajectory_AntiSetting_August19_2023_1300()
        {
            // Arrange

            // LAT: 0.0, LON: 0.0 (Null Island)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(0.0), latitude: new Angle(0.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Universal Coordinated Time (IANA: UTC)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("UTC");

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
            DateTime localRiseDateTime = rise.DateTime.ToLocalTime();
            DateTime localApexDateTime = apex.DateTime.ToLocalTime();
            DateTime localSetDateTime = set.DateTime.ToLocalTime();

            // Assert
            Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));
            Assert.IsInstanceOfType(rise, typeof(LocalVector));
            Assert.IsInstanceOfType(apex, typeof(LocalVector));
            Assert.IsInstanceOfType(set, typeof(LocalVector));
            Assert.IsFalse(riseSet.IsAboveHorizon(dateTime));
            Assert.IsFalse(riseSet.IsRising(dateTime));
            Assert.IsFalse(riseSet.IsSetting(dateTime));
            Assert.AreEqual(-1.9374266457816702E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17666670000001, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.82333329999999, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000307832465, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.0529158394190949E-08, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.8233333, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280521818920000, rise.DateTime.Ticks);
            Assert.AreEqual(638280737229170000, apex.DateTime.Ticks);
            Assert.AreEqual(638280952639410000, set.DateTime.Ticks);
            Assert.AreEqual(638280521818920000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280737229170000, localApexDateTime.Ticks);
            Assert.AreEqual(638280952639410000, localSetDateTime.Ticks);
        }
    }
}