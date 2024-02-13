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
    public class Poniatowski_M17_TrajectoryTests
    {
        [TestMethod]
        public void Poniatowski_M17_GMST_Trajectory_Rising_August18_2023_1800()
        {
            // Arrange

            // LAT: 45.0, LON: -90.0 (Poniatowski, Wisconsin)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-90.0), latitude: new Angle(45.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Central Standard Time (IANA: America/Chicago)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");

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
            Assert.AreEqual(-1.4927081792848185E-10, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.2036157713509, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333300000005, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.9282907895042935E-10, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.7963842282769, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279915944974984, rise.DateTime.Ticks);
            Assert.AreEqual(638280090994355841, apex.DateTime.Ticks);
            Assert.AreEqual(638280266043736697, set.DateTime.Ticks);
            Assert.AreEqual(638279735944974984, localRiseDateTime.Ticks);
            Assert.AreEqual(638279910994355841, localApexDateTime.Ticks);
            Assert.AreEqual(638280086043736697, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Poniatowski_M17_GMST_Trajectory_Setting_August19_2023_0100()
        {
            // Arrange

            // LAT: 45.0, LON: -90.0 (Poniatowski, Wisconsin)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-90.0), latitude: new Angle(45.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Central Standard Time (IANA: America/Chicago)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");

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
            Assert.AreEqual(1.2263741768521101E-10, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361577164672, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333300000005, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.9282907895042935E-10, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.7963842282769, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279915944974985, rise.DateTime.Ticks);
            Assert.AreEqual(638280090994355841, apex.DateTime.Ticks);
            Assert.AreEqual(638280266043736697, set.DateTime.Ticks);
            Assert.AreEqual(638279735944974985, localRiseDateTime.Ticks);
            Assert.AreEqual(638279910994355841, localApexDateTime.Ticks);
            Assert.AreEqual(638280086043736697, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Poniatowski_M17_GMST_Trajectory_AntiRising_August19_2023_0600()
        {
            // Arrange

            // LAT: 45.0, LON: -90.0 (Poniatowski, Wisconsin)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-90.0), latitude: new Angle(45.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Central Standard Time (IANA: America/Chicago)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");

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
            Assert.AreEqual(-2.3794655135134235E-10, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361577125443, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333300000005, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(9.594143196634698E-12, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638422847626, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280777585880292, rise.DateTime.Ticks);
            Assert.AreEqual(638280952635261149, apex.DateTime.Ticks);
            Assert.AreEqual(638281127684642006, set.DateTime.Ticks);
            Assert.AreEqual(638280597585880292, localRiseDateTime.Ticks);
            Assert.AreEqual(638280772635261149, localApexDateTime.Ticks);
            Assert.AreEqual(638280947684642006, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Poniatowski_M17_GMST_Trajectory_AntiSetting_August19_2023_1300()
        {
            // Arrange

            // LAT: 45.0, LON: -90.0 (Poniatowski, Wisconsin)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-90.0), latitude: new Angle(45.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Central Standard Time (IANA: America/Chicago)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");

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
            Assert.AreEqual(-2.3794655135134235E-10, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361577125443, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333300000005, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(9.594143196634698E-12, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638422847626, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280777585880292, rise.DateTime.Ticks);
            Assert.AreEqual(638280952635261149, apex.DateTime.Ticks);
            Assert.AreEqual(638281127684642006, set.DateTime.Ticks);
            Assert.AreEqual(638280597585880292, localRiseDateTime.Ticks);
            Assert.AreEqual(638280772635261149, localApexDateTime.Ticks);
            Assert.AreEqual(638280947684642006, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Poniatowski_M17_GAST_Trajectory_Rising_August18_2023_1800()
        {
            // Arrange

            // LAT: 45.0, LON: -90.0 (Poniatowski, Wisconsin)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-90.0), latitude: new Angle(45.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Central Standard Time (IANA: America/Chicago)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");

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
            Assert.AreEqual(-0.0017588493141715844, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20523405367713, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.820941558589325, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.9999946681853, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-0.0017554662183556502, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79475765279642, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279918144677662, rise.DateTime.Ticks);
            Assert.AreEqual(638280093194055389, apex.DateTime.Ticks);
            Assert.AreEqual(638280268243436246, set.DateTime.Ticks);
            Assert.AreEqual(638279738144677662, localRiseDateTime.Ticks);
            Assert.AreEqual(638279913194055389, localApexDateTime.Ticks);
            Assert.AreEqual(638280088243436246, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Poniatowski_M17_GAST_Trajectory_Setting_August19_2023_0100()
        {
            // Arrange

            // LAT: 45.0, LON: -90.0 (Poniatowski, Wisconsin)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-90.0), latitude: new Angle(45.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Central Standard Time (IANA: America/Chicago)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");

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
            Assert.AreEqual(-0.0017541455360969849, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20523917142812, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.820941558589325, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.9999946681853, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-0.0017554662183556502, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79475765279642, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279918144694985, rise.DateTime.Ticks);
            Assert.AreEqual(638280093194055389, apex.DateTime.Ticks);
            Assert.AreEqual(638280268243436246, set.DateTime.Ticks);
            Assert.AreEqual(638279738144694985, localRiseDateTime.Ticks);
            Assert.AreEqual(638279913194055389, localApexDateTime.Ticks);
            Assert.AreEqual(638280088243436246, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Poniatowski_M17_GAST_Trajectory_AntiRising_August19_2023_0600()
        {
            // Arrange

            // LAT: 45.0, LON: -90.0 (Poniatowski, Wisconsin)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-90.0), latitude: new Angle(45.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Central Standard Time (IANA: America/Chicago)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");

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
            Assert.AreEqual(-0.0017711046808699393, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20522949253298, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.82093680973136, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99999502168694, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-0.0017585618582529605, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.7947557426045, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280779785612679, rise.DateTime.Ticks);
            Assert.AreEqual(638280954835019443, apex.DateTime.Ticks);
            Assert.AreEqual(638281129884400300, set.DateTime.Ticks);
            Assert.AreEqual(638280599785612679, localRiseDateTime.Ticks);
            Assert.AreEqual(638280774835019443, localApexDateTime.Ticks);
            Assert.AreEqual(638280949884400300, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Poniatowski_M17_GAST_Trajectory_AntiSetting_August19_2023_1300()
        {
            // Arrange

            // LAT: 45.0, LON: -90.0 (Poniatowski, Wisconsin)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-90.0), latitude: new Angle(45.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Central Standard Time (IANA: America/Chicago)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");

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
            Assert.AreEqual(-0.0017664527663896479, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20523455385516, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.82093680973136, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99999502168694, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-0.0017585618582529605, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.7947557426045, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280779785629811, rise.DateTime.Ticks);
            Assert.AreEqual(638280954835019443, apex.DateTime.Ticks);
            Assert.AreEqual(638281129884400300, set.DateTime.Ticks);
            Assert.AreEqual(638280599785629811, localRiseDateTime.Ticks);
            Assert.AreEqual(638280774835019443, localApexDateTime.Ticks);
            Assert.AreEqual(638280949884400300, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Poniatowski_M17_ERA_Trajectory_Rising_August18_2023_1800()
        {
            // Arrange

            // LAT: 45.0, LON: -90.0 (Poniatowski, Wisconsin)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-90.0), latitude: new Angle(45.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Central Standard Time (IANA: America/Chicago)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");

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
            Assert.AreEqual(-2.5853182705759536E-07, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361549022847, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999994, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000147877932, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-1.824563582886185E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638621362733, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279915949043115, rise.DateTime.Ticks);
            Assert.AreEqual(638280090998441933, apex.DateTime.Ticks);
            Assert.AreEqual(638280266047839800, set.DateTime.Ticks);
            Assert.AreEqual(638279735949043115, localRiseDateTime.Ticks);
            Assert.AreEqual(638279910998441933, localApexDateTime.Ticks);
            Assert.AreEqual(638280086047839800, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Poniatowski_M17_ERA_Trajectory_Setting_August19_2023_0100()
        {
            // Arrange

            // LAT: 45.0, LON: -90.0 (Poniatowski, Wisconsin)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-90.0), latitude: new Angle(45.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Central Standard Time (IANA: America/Chicago)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");

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
            Assert.AreEqual(-1.5681544596191088E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361406534792, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999994, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000147877932, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-1.824563582886185E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638621362733, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279915949038292, rise.DateTime.Ticks);
            Assert.AreEqual(638280090998441933, apex.DateTime.Ticks);
            Assert.AreEqual(638280266047839800, set.DateTime.Ticks);
            Assert.AreEqual(638279735949038292, localRiseDateTime.Ticks);
            Assert.AreEqual(638279910998441933, localApexDateTime.Ticks);
            Assert.AreEqual(638280086047839800, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Poniatowski_M17_ERA_Trajectory_AntiRising_August19_2023_0600()
        {
            // Arrange

            // LAT: 45.0, LON: -90.0 (Poniatowski, Wisconsin)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-90.0), latitude: new Angle(45.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Central Standard Time (IANA: America/Chicago)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");

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
            Assert.AreEqual(2.0999280186745806E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361805625284, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999987, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000190909591, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-2.3083229621079226E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.7963867399616, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280777590023919, rise.DateTime.Ticks);
            Assert.AreEqual(638280952639414052, apex.DateTime.Ticks);
            Assert.AreEqual(638281127688811918, set.DateTime.Ticks);
            Assert.AreEqual(638280597590023919, localRiseDateTime.Ticks);
            Assert.AreEqual(638280772639414052, localApexDateTime.Ticks);
            Assert.AreEqual(638280947688811918, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Poniatowski_M17_ERA_Trajectory_AntiSetting_August19_2023_1300()
        {
            // Arrange

            // LAT: 45.0, LON: -90.0 (Poniatowski, Wisconsin)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-90.0), latitude: new Angle(45.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Central Standard Time (IANA: America/Chicago)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");

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
            Assert.AreEqual(7.474029655681191E-07, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361658469407, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999987, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000190909591, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-2.3083229621079226E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.7963867399616, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280777590018938, rise.DateTime.Ticks);
            Assert.AreEqual(638280952639414052, apex.DateTime.Ticks);
            Assert.AreEqual(638281127688811918, set.DateTime.Ticks);
            Assert.AreEqual(638280597590018938, localRiseDateTime.Ticks);
            Assert.AreEqual(638280772639414052, localApexDateTime.Ticks);
            Assert.AreEqual(638280947688811918, localSetDateTime.Ticks);
        }
    }
}