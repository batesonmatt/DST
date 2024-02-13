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
            Assert.AreEqual(-1.0436451702844352E-10, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361577139977, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333300000005, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.4672534856527496E-10, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638422832707, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279485124522330, rise.DateTime.Ticks);
            Assert.AreEqual(638279660173903186, apex.DateTime.Ticks);
            Assert.AreEqual(638279835223284043, set.DateTime.Ticks);
            Assert.AreEqual(638279773124522330, localRiseDateTime.Ticks);
            Assert.AreEqual(638279948173903186, localApexDateTime.Ticks);
            Assert.AreEqual(638280123223284043, localSetDateTime.Ticks);
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
            Assert.AreEqual(1.6755162061949806E-10, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.2036157716956, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333300000005, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.4672534856527496E-10, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638422832707, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279485124522331, rise.DateTime.Ticks);
            Assert.AreEqual(638279660173903186, apex.DateTime.Ticks);
            Assert.AreEqual(638279835223284043, set.DateTime.Ticks);
            Assert.AreEqual(638279773124522331, localRiseDateTime.Ticks);
            Assert.AreEqual(638279948173903186, localApexDateTime.Ticks);
            Assert.AreEqual(638280123223284043, localSetDateTime.Ticks);
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
            Assert.AreEqual(-1.9417711882852018E-10, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.203615771302, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333300000005, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(2.3656806692991567E-10, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638422822933, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280346765427638, rise.DateTime.Ticks);
            Assert.AreEqual(638280521814808495, apex.DateTime.Ticks);
            Assert.AreEqual(638280696864189351, set.DateTime.Ticks);
            Assert.AreEqual(638280634765427638, localRiseDateTime.Ticks);
            Assert.AreEqual(638280809814808495, localApexDateTime.Ticks);
            Assert.AreEqual(638280984864189351, localSetDateTime.Ticks);
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
            Assert.AreEqual(-1.9417711882852018E-10, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.203615771302, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333300000005, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(2.3656806692991567E-10, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638422822933, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280346765427638, rise.DateTime.Ticks);
            Assert.AreEqual(638280521814808495, apex.DateTime.Ticks);
            Assert.AreEqual(638280696864189351, set.DateTime.Ticks);
            Assert.AreEqual(638280634765427638, localRiseDateTime.Ticks);
            Assert.AreEqual(638280809814808495, localApexDateTime.Ticks);
            Assert.AreEqual(638280984864189351, localSetDateTime.Ticks);
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
            Assert.AreEqual(-0.0017557657139377625, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20522981719566, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.820946122469792, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99999487735812, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-0.0017527894954696421, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79476061775966, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279487324193829, rise.DateTime.Ticks);
            Assert.AreEqual(638279662373574129, apex.DateTime.Ticks);
            Assert.AreEqual(638279837422954986, set.DateTime.Ticks);
            Assert.AreEqual(638279775324193829, localRiseDateTime.Ticks);
            Assert.AreEqual(638279950373574129, localApexDateTime.Ticks);
            Assert.AreEqual(638280125422954986, localSetDateTime.Ticks);
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
            Assert.AreEqual(-0.0017512712897769234, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20523470716783, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.820946122469792, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99999487735812, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-0.0017527894954696421, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79476061775966, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279487324210381, rise.DateTime.Ticks);
            Assert.AreEqual(638279662373574129, apex.DateTime.Ticks);
            Assert.AreEqual(638279837422954986, set.DateTime.Ticks);
            Assert.AreEqual(638279775324210381, localRiseDateTime.Ticks);
            Assert.AreEqual(638279950373574129, localApexDateTime.Ticks);
            Assert.AreEqual(638280125422954986, localSetDateTime.Ticks);
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
            Assert.AreEqual(-0.0017702830230064137, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20522708649627, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.820938452448566, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99999460026135, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-0.001757386099143332, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79475603558978, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280348965127858, rise.DateTime.Ticks);
            Assert.AreEqual(638280524014537651, apex.DateTime.Ticks);
            Assert.AreEqual(638280699063918508, set.DateTime.Ticks);
            Assert.AreEqual(638280636965127858, localRiseDateTime.Ticks);
            Assert.AreEqual(638280812014537651, localApexDateTime.Ticks);
            Assert.AreEqual(638280987063918508, localSetDateTime.Ticks);
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
            Assert.AreEqual(-0.0017655857631098115, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20523219715469, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.820938452448566, apexCoordinate.Components.Inclination);
            Assert.AreEqual(179.99999460026135, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-0.001757386099143332, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79475603558978, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280348965145157, rise.DateTime.Ticks);
            Assert.AreEqual(638280524014537651, apex.DateTime.Ticks);
            Assert.AreEqual(638280699063918508, set.DateTime.Ticks);
            Assert.AreEqual(638280636965145157, localRiseDateTime.Ticks);
            Assert.AreEqual(638280812014537651, localApexDateTime.Ticks);
            Assert.AreEqual(638280987063918508, localSetDateTime.Ticks);
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
            Assert.AreEqual(-7.321887096622959E-08, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361569185052, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999987, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000190909591, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-2.136777368377807E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638655331854, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279485128558783, rise.DateTime.Ticks);
            Assert.AreEqual(638279660177956919, apex.DateTime.Ticks);
            Assert.AreEqual(638279835227354786, set.DateTime.Ticks);
            Assert.AreEqual(638279773128558783, localRiseDateTime.Ticks);
            Assert.AreEqual(638279948177956919, localApexDateTime.Ticks);
            Assert.AreEqual(638280123227354786, localSetDateTime.Ticks);
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
            Assert.AreEqual(-1.6399865216953913E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361398719406, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999987, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000190909591, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-2.136777368377807E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638655331854, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279485128553013, rise.DateTime.Ticks);
            Assert.AreEqual(638279660177956919, apex.DateTime.Ticks);
            Assert.AreEqual(638279835227354786, set.DateTime.Ticks);
            Assert.AreEqual(638279773128553013, localRiseDateTime.Ticks);
            Assert.AreEqual(638279948177956919, localApexDateTime.Ticks);
            Assert.AreEqual(638280123227354786, localSetDateTime.Ticks);
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
            Assert.AreEqual(2.173965905632765E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361813680668, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999994, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000147877932, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-1.8837450852515758E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638627801728, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280346769538345, rise.DateTime.Ticks);
            Assert.AreEqual(638280521818928205, apex.DateTime.Ticks);
            Assert.AreEqual(638280696868326072, set.DateTime.Ticks);
            Assert.AreEqual(638280634769538345, localRiseDateTime.Ticks);
            Assert.AreEqual(638280809818928205, localApexDateTime.Ticks);
            Assert.AreEqual(638280984868326072, localSetDateTime.Ticks);
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
            Assert.AreEqual(8.553811462959258E-07, riseCoordinate.Components.Inclination);
            Assert.AreEqual(113.20361670217525, riseCoordinate.Components.Rotation);
            Assert.AreEqual(28.823333299999994, apexCoordinate.Components.Inclination);
            Assert.AreEqual(180.00000147877932, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-1.8837450852515758E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(246.79638627801728, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280346769533489, rise.DateTime.Ticks);
            Assert.AreEqual(638280521818928205, apex.DateTime.Ticks);
            Assert.AreEqual(638280696868326072, set.DateTime.Ticks);
            Assert.AreEqual(638280634769533489, localRiseDateTime.Ticks);
            Assert.AreEqual(638280809818928205, localApexDateTime.Ticks);
            Assert.AreEqual(638280984868326072, localSetDateTime.Ticks);
        }
    }
}