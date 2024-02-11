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
            Assert.AreEqual(-1.3534889831134933E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361429890583, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999966, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.9999973001307, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.818674639018307E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638224975324, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279915944970000, rise.DateTime.Ticks);
            Assert.AreEqual(638280090994350000, apex.DateTime.Ticks);
            Assert.AreEqual(638280266043730000, set.DateTime.Ticks);
            Assert.AreEqual(638279735944970000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279910994350000, localApexDateTime.Ticks);
            Assert.AreEqual(638280086043730000, localSetDateTime.Ticks);
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
            Assert.AreEqual(-1.3534889831134933E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361429890583, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999966, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.9999973001307, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.818674639018307E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638224975324, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279915944970000, rise.DateTime.Ticks);
            Assert.AreEqual(638280090994350000, apex.DateTime.Ticks);
            Assert.AreEqual(638280266043730000, set.DateTime.Ticks);
            Assert.AreEqual(638279735944970000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279910994350000, localApexDateTime.Ticks);
            Assert.AreEqual(638280086043730000, localSetDateTime.Ticks);
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
            Assert.AreEqual(-7.952661462695687E-08, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361568498768, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333300000005, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(5.447134944178376E-07, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638363583382, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280777585880000, rise.DateTime.Ticks);
            Assert.AreEqual(638280952635260000, apex.DateTime.Ticks);
            Assert.AreEqual(638281127684640000, set.DateTime.Ticks);
            Assert.AreEqual(638280597585880000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280772635260000, localApexDateTime.Ticks);
            Assert.AreEqual(638280947684640000, localSetDateTime.Ticks);
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
            Assert.AreEqual(-7.952661462695687E-08, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361568498768, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333300000005, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(5.447134944178376E-07, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638363583382, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280777585880000, rise.DateTime.Ticks);
            Assert.AreEqual(638280952635260000, apex.DateTime.Ticks);
            Assert.AreEqual(638281127684640000, set.DateTime.Ticks);
            Assert.AreEqual(638280597585880000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280772635260000, localApexDateTime.Ticks);
            Assert.AreEqual(638280947684640000, localSetDateTime.Ticks);
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
            Assert.AreEqual(-0.001760929803936051, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20523179008657, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.820941558589183, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.9999920365296, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-0.001753770217874262, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79475580753342, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279918144670000, rise.DateTime.Ticks);
            Assert.AreEqual(638280093194050000, apex.DateTime.Ticks);
            Assert.AreEqual(638280268243430000, set.DateTime.Ticks);
            Assert.AreEqual(638279738144670000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279913194050000, localApexDateTime.Ticks);
            Assert.AreEqual(638280088243430000, localSetDateTime.Ticks);
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
            Assert.AreEqual(-0.0017554991322867863, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.2052376987038, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.820941558589183, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.9999920365296, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-0.001753770217874262, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79475580753342, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279918144690000, rise.DateTime.Ticks);
            Assert.AreEqual(638280093194050000, apex.DateTime.Ticks);
            Assert.AreEqual(638280268243430000, set.DateTime.Ticks);
            Assert.AreEqual(638279738144690000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279913194050000, localApexDateTime.Ticks);
            Assert.AreEqual(638280088243430000, localSetDateTime.Ticks);
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
            Assert.AreEqual(-0.0017718321193456177, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20522870107384, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.820936809731055, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.9999904164146, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-0.0017584803977683805, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.7947556539748, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280779785610000, rise.DateTime.Ticks);
            Assert.AreEqual(638280954835010000, apex.DateTime.Ticks);
            Assert.AreEqual(638281129884400000, set.DateTime.Ticks);
            Assert.AreEqual(638280599785610000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280774835010000, localApexDateTime.Ticks);
            Assert.AreEqual(638280949884400000, localSetDateTime.Ticks);
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
            Assert.AreEqual(-0.0017691167839757327, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20523165538161, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.820936809731055, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.9999904164146, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-0.0017584803977683805, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.7947556539748, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280779785620000, rise.DateTime.Ticks);
            Assert.AreEqual(638280954835010000, apex.DateTime.Ticks);
            Assert.AreEqual(638281129884400000, set.DateTime.Ticks);
            Assert.AreEqual(638280599785620000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280774835010000, localApexDateTime.Ticks);
            Assert.AreEqual(638280949884400000, localSetDateTime.Ticks);
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
            Assert.AreEqual(-1.10436832301275E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361456995118, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999998, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(8.364977097077653E-07, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.7963833183701, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279915949040000, rise.DateTime.Ticks);
            Assert.AreEqual(638280090998440000, apex.DateTime.Ticks);
            Assert.AreEqual(638280266047830000, set.DateTime.Ticks);
            Assert.AreEqual(638279735949040000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279910998440000, localApexDateTime.Ticks);
            Assert.AreEqual(638280086047830000, localSetDateTime.Ticks);
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
            Assert.AreEqual(-3.8197376852622256E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361161560636, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999998, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(8.364977097077653E-07, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.7963833183701, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279915949030000, rise.DateTime.Ticks);
            Assert.AreEqual(638280090998440000, apex.DateTime.Ticks);
            Assert.AreEqual(638280266047830000, set.DateTime.Ticks);
            Assert.AreEqual(638279735949030000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279910998440000, localApexDateTime.Ticks);
            Assert.AreEqual(638280086047830000, localSetDateTime.Ticks);
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
            Assert.AreEqual(1.0357751708513037E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361689844549, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333300000005, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-1.7875167941383552E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638617332012, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280777590020000, rise.DateTime.Ticks);
            Assert.AreEqual(638280952639410000, apex.DateTime.Ticks);
            Assert.AreEqual(638281127688810000, set.DateTime.Ticks);
            Assert.AreEqual(638280597590020000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280772639410000, localApexDateTime.Ticks);
            Assert.AreEqual(638280947688810000, localSetDateTime.Ticks);
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
            Assert.AreEqual(-1.6795930264379422E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361394410183, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333300000005, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-1.7875167941383552E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638617332012, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280777590010000, rise.DateTime.Ticks);
            Assert.AreEqual(638280952639410000, apex.DateTime.Ticks);
            Assert.AreEqual(638281127688810000, set.DateTime.Ticks);
            Assert.AreEqual(638280597590010000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280772639410000, localApexDateTime.Ticks);
            Assert.AreEqual(638280947688810000, localSetDateTime.Ticks);
        }
    }
}