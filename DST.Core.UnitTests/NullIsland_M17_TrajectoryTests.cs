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
            DateTime localDateTime = new(2023, 8, 18, 18, 0, 0, DateTimeConstants.StandardKind);
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
            Assert.AreEqual(-3.87274212698685E-10, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17666670000001, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.8233333, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000120741828, apexCoordinate.Components.Rotation);
            Assert.AreEqual(5.185872911183326E-11, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.8233333, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279660173903186, rise.DateTime.Ticks);
            Assert.AreEqual(638279875584129514, apex.DateTime.Ticks);
            Assert.AreEqual(638280090994355841, set.DateTime.Ticks);
            Assert.AreEqual(638279660173903186, localRiseDateTime.Ticks);
            Assert.AreEqual(638279875584129514, localApexDateTime.Ticks);
            Assert.AreEqual(638280090994355841, localSetDateTime.Ticks);
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
            DateTime localDateTime = new(2023, 8, 19, 1, 0, 0, DateTimeConstants.StandardKind);
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
            Assert.AreEqual(1.4505482434669215E-11, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17666670000001, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.8233333, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000120741828, apexCoordinate.Components.Rotation);
            Assert.AreEqual(5.185872911183326E-11, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.8233333, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279660173903187, rise.DateTime.Ticks);
            Assert.AreEqual(638279875584129514, apex.DateTime.Ticks);
            Assert.AreEqual(638280090994355841, set.DateTime.Ticks);
            Assert.AreEqual(638279660173903187, localRiseDateTime.Ticks);
            Assert.AreEqual(638279875584129514, localApexDateTime.Ticks);
            Assert.AreEqual(638280090994355841, localSetDateTime.Ticks);
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
            DateTime localDateTime = new(2023, 8, 19, 6, 0, 0, DateTimeConstants.StandardKind);
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
            Assert.AreEqual(-1.1652900866465643E-10, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17666670000001, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.8233333, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000120741828, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.8289003849511576E-10, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.8233333, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280521814808495, rise.DateTime.Ticks);
            Assert.AreEqual(638280737225034822, apex.DateTime.Ticks);
            Assert.AreEqual(638280952635261149, set.DateTime.Ticks);
            Assert.AreEqual(638280521814808495, localRiseDateTime.Ticks);
            Assert.AreEqual(638280737225034822, localApexDateTime.Ticks);
            Assert.AreEqual(638280952635261149, localSetDateTime.Ticks);
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
            DateTime localDateTime = new(2023, 8, 19, 13, 0, 0, DateTimeConstants.StandardKind);
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
            Assert.AreEqual(-1.1652900866465643E-10, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17666670000001, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.8233333, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000120741828, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.8289003849511576E-10, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.8233333, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280521814808495, rise.DateTime.Ticks);
            Assert.AreEqual(638280737225034822, apex.DateTime.Ticks);
            Assert.AreEqual(638280952635261149, set.DateTime.Ticks);
            Assert.AreEqual(638280521814808495, localRiseDateTime.Ticks);
            Assert.AreEqual(638280737225034822, localApexDateTime.Ticks);
            Assert.AreEqual(638280952635261149, localSetDateTime.Ticks);
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
            DateTime localDateTime = new(2023, 8, 18, 18, 0, 0, DateTimeConstants.StandardKind);
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
            Assert.AreEqual(3.255723684263262E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17905387753032, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.82094366164591, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.9999792794923, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.171277490910741E-05, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.8209415585893, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279662373593652, rise.DateTime.Ticks);
            Assert.AreEqual(638279877783811865, apex.DateTime.Ticks);
            Assert.AreEqual(638280093194038192, set.DateTime.Ticks);
            Assert.AreEqual(638279662373593652, localRiseDateTime.Ticks);
            Assert.AreEqual(638279877783811865, localApexDateTime.Ticks);
            Assert.AreEqual(638280093194038192, localSetDateTime.Ticks);
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
            DateTime localDateTime = new(2023, 8, 19, 1, 0, 0, DateTimeConstants.StandardKind);
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
            Assert.AreEqual(1.0135734231879264E-05, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17905387753079, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.82094366164591, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.9999792794923, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.171277490910741E-05, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.8209415585893, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279662373610798, rise.DateTime.Ticks);
            Assert.AreEqual(638279877783811865, apex.DateTime.Ticks);
            Assert.AreEqual(638280093194038192, set.DateTime.Ticks);
            Assert.AreEqual(638279662373610798, localRiseDateTime.Ticks);
            Assert.AreEqual(638279877783811865, localApexDateTime.Ticks);
            Assert.AreEqual(638280093194038192, localSetDateTime.Ticks);
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
            DateTime localDateTime = new(2023, 8, 19, 6, 0, 0, DateTimeConstants.StandardKind);
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
            Assert.AreEqual(-8.480790825160511E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17906154755141, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.8209374507894, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.9999792619102, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.1448721591697908E-05, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.82093680973122, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280524014528495, rise.DateTime.Ticks);
            Assert.AreEqual(638280739424775957, apex.DateTime.Ticks);
            Assert.AreEqual(638280954835002284, set.DateTime.Ticks);
            Assert.AreEqual(638280524014528495, localRiseDateTime.Ticks);
            Assert.AreEqual(638280739424775957, localApexDateTime.Ticks);
            Assert.AreEqual(638280954835002284, localSetDateTime.Ticks);
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
            DateTime localDateTime = new(2023, 8, 19, 13, 0, 0, DateTimeConstants.StandardKind);
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
            Assert.AreEqual(-1.5325656477216398E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17906154755129, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.8209374507894, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.9999792619102, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.1448721591697908E-05, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.82093680973122, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280524014545811, rise.DateTime.Ticks);
            Assert.AreEqual(638280739424775957, apex.DateTime.Ticks);
            Assert.AreEqual(638280954835002284, set.DateTime.Ticks);
            Assert.AreEqual(638280524014545811, localRiseDateTime.Ticks);
            Assert.AreEqual(638280739424775957, localApexDateTime.Ticks);
            Assert.AreEqual(638280954835002284, localSetDateTime.Ticks);
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
            DateTime localDateTime = new(2023, 8, 18, 18, 0, 0, DateTimeConstants.StandardKind);
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
            Assert.AreEqual(-1.0772178029583301E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17666670000001, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.8233332999999, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000627392936, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-3.527821547777421E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.82333329999997, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279660177950115, rise.DateTime.Ticks);
            Assert.AreEqual(638279875588200058, apex.DateTime.Ticks);
            Assert.AreEqual(638280090998447318, set.DateTime.Ticks);
            Assert.AreEqual(638279660177950115, localRiseDateTime.Ticks);
            Assert.AreEqual(638279875588200058, localApexDateTime.Ticks);
            Assert.AreEqual(638280090998447318, localSetDateTime.Ticks);
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
            DateTime localDateTime = new(2023, 8, 19, 1, 0, 0, DateTimeConstants.StandardKind);
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
            Assert.AreEqual(-3.104409302068234E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17666670000003, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.8233332999999, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000627392936, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-3.527821547777421E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.82333329999997, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279660177945063, rise.DateTime.Ticks);
            Assert.AreEqual(638279875588200058, apex.DateTime.Ticks);
            Assert.AreEqual(638280090998447318, set.DateTime.Ticks);
            Assert.AreEqual(638279660177945063, localRiseDateTime.Ticks);
            Assert.AreEqual(638279875588200058, localApexDateTime.Ticks);
            Assert.AreEqual(638280090998447318, localSetDateTime.Ticks);
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
            DateTime localDateTime = new(2023, 8, 19, 6, 0, 0, DateTimeConstants.StandardKind);
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
            Assert.AreEqual(2.346481993099977E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17666670000001, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.8233332999999, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000627392936, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-3.7396940228973108E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.82333329999994, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280521818930676, rise.DateTime.Ticks);
            Assert.AreEqual(638280737229172087, apex.DateTime.Ticks);
            Assert.AreEqual(638280952639419346, set.DateTime.Ticks);
            Assert.AreEqual(638280521818930676, localRiseDateTime.Ticks);
            Assert.AreEqual(638280737229172087, localApexDateTime.Ticks);
            Assert.AreEqual(638280952639419346, localSetDateTime.Ticks);
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
            DateTime localDateTime = new(2023, 8, 19, 13, 0, 0, DateTimeConstants.StandardKind);
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
            Assert.AreEqual(4.220158172454869E-07, riseCoordinate.Components.Inclination);
            Assert.AreEqual(106.17666670000001, riseCoordinate.Components.Rotation);
            Assert.AreEqual(73.8233332999999, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000627392936, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-3.7396940228973108E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(253.82333329999994, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280521818925880, rise.DateTime.Ticks);
            Assert.AreEqual(638280737229172087, apex.DateTime.Ticks);
            Assert.AreEqual(638280952639419346, set.DateTime.Ticks);
            Assert.AreEqual(638280521818925880, localRiseDateTime.Ticks);
            Assert.AreEqual(638280737229172087, localApexDateTime.Ticks);
            Assert.AreEqual(638280952639419346, localSetDateTime.Ticks);
        }
    }
}