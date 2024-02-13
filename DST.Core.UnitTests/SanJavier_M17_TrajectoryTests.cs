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
    public class SanJavier_M17_TrajectoryTests
    {
        [TestMethod]
        public void SanJavier_M17_GMST_Trajectory_Rising_August18_2023_1800()
        {
            // Arrange

            // LAT: -30.0, LON: -60.0 (San Javier, Santa Fe, Argentina)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-60.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Argentina Standard Time (IANA: America/Buenos_Aires)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Buenos_Aires");

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
            Assert.AreEqual(-2.729620973696001E-10, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.765739562239, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666670000001, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.9999982924527, apexCoordinate.Components.Rotation);
            Assert.AreEqual(2.291748675728537E-11, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23426043794143, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279780704586990, rise.DateTime.Ticks);
            Assert.AreEqual(638280019190947065, apex.DateTime.Ticks);
            Assert.AreEqual(638280257677307140, set.DateTime.Ticks);
            Assert.AreEqual(638279672704586990, localRiseDateTime.Ticks);
            Assert.AreEqual(638279911190947065, localApexDateTime.Ticks);
            Assert.AreEqual(638280149677307140, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void SanJavier_M17_GMST_Trajectory_Setting_August19_2023_0100()
        {
            // Arrange

            // LAT: -30.0, LON: -60.0 (San Javier, Santa Fe, Argentina)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-60.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Argentina Standard Time (IANA: America/Buenos_Aires)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Buenos_Aires");

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
            Assert.AreEqual(7.009942517945591E-11, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.7657395620298, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666670000001, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.9999982924527, apexCoordinate.Components.Rotation);
            Assert.AreEqual(2.291748675728537E-11, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23426043794143, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279780704586991, rise.DateTime.Ticks);
            Assert.AreEqual(638280019190947065, apex.DateTime.Ticks);
            Assert.AreEqual(638280257677307140, set.DateTime.Ticks);
            Assert.AreEqual(638279672704586991, localRiseDateTime.Ticks);
            Assert.AreEqual(638279911190947065, localApexDateTime.Ticks);
            Assert.AreEqual(638280149677307140, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void SanJavier_M17_GMST_Trajectory_AntiRising_August19_2023_0600()
        {
            // Arrange

            // LAT: -30.0, LON: -60.0 (San Javier, Santa Fe, Argentina)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-60.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Argentina Standard Time (IANA: America/Buenos_Aires)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Buenos_Aires");

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
            Assert.AreEqual(-4.177991286269389E-11, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76573956209802, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666670000001, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.9999982924527, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.3776413575025148E-10, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23426043801146, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280642345492299, rise.DateTime.Ticks);
            Assert.AreEqual(638280880831852373, apex.DateTime.Ticks);
            Assert.AreEqual(638281119318212448, set.DateTime.Ticks);
            Assert.AreEqual(638280534345492299, localRiseDateTime.Ticks);
            Assert.AreEqual(638280772831852373, localApexDateTime.Ticks);
            Assert.AreEqual(638281011318212448, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void SanJavier_M17_GMST_Trajectory_AntiSetting_August19_2023_1300()
        {
            // Arrange

            // LAT: -30.0, LON: -60.0 (San Javier, Santa Fe, Argentina)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-60.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Argentina Standard Time (IANA: America/Buenos_Aires)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Buenos_Aires");

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
            Assert.AreEqual(-4.177991286269389E-11, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76573956209802, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666670000001, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.9999982924527, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.3776413575025148E-10, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23426043801146, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280642345492299, rise.DateTime.Ticks);
            Assert.AreEqual(638280880831852373, apex.DateTime.Ticks);
            Assert.AreEqual(638281119318212448, set.DateTime.Ticks);
            Assert.AreEqual(638280534345492299, localRiseDateTime.Ticks);
            Assert.AreEqual(638280772831852373, localApexDateTime.Ticks);
            Assert.AreEqual(638281011318212448, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void SanJavier_M17_GAST_Trajectory_Rising_August18_2023_1800()
        {
            // Arrange

            // LAT: -30.0, LON: -60.0 (San Javier, Santa Fe, Argentina)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-60.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Argentina Standard Time (IANA: America/Buenos_Aires)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Buenos_Aires");

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
            Assert.AreEqual(0.0012460610339931756, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76777738950553, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17905778072368, apexCoordinate.Components.Inclination);
            Assert.AreEqual(2.6904031909000533E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(0.0012570669595875974, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.2322240273165, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279782904284759, rise.DateTime.Ticks);
            Assert.AreEqual(638280021390637457, apex.DateTime.Ticks);
            Assert.AreEqual(638280259876997531, set.DateTime.Ticks);
            Assert.AreEqual(638279674904284759, localRiseDateTime.Ticks);
            Assert.AreEqual(638279913390637457, localApexDateTime.Ticks);
            Assert.AreEqual(638280151876997531, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void SanJavier_M17_GAST_Trajectory_Setting_August19_2023_0100()
        {
            // Arrange

            // LAT: -30.0, LON: -60.0 (San Javier, Santa Fe, Argentina)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-60.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Argentina Standard Time (IANA: America/Buenos_Aires)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Buenos_Aires");

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
            Assert.AreEqual(0.001251977965965697, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76777378157243, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17905778072368, apexCoordinate.Components.Inclination);
            Assert.AreEqual(2.6904031909000533E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(0.0012570669595875974, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.2322240273165, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279782904302030, rise.DateTime.Ticks);
            Assert.AreEqual(638280021390637457, apex.DateTime.Ticks);
            Assert.AreEqual(638280259876997531, set.DateTime.Ticks);
            Assert.AreEqual(638279674904302030, localRiseDateTime.Ticks);
            Assert.AreEqual(638279913390637457, localApexDateTime.Ticks);
            Assert.AreEqual(638280151876997531, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void SanJavier_M17_GAST_Trajectory_AntiRising_August19_2023_0600()
        {
            // Arrange

            // LAT: -30.0, LON: -60.0 (San Javier, Santa Fe, Argentina)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-60.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Argentina Standard Time (IANA: America/Buenos_Aires)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Buenos_Aires");

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
            Assert.AreEqual(0.0012396178782243573, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76778934722864, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17906301605542, apexCoordinate.Components.Inclination);
            Assert.AreEqual(2.634275661277943E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(0.0012583383482807945, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23222055366432, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280644545219738, rise.DateTime.Ticks);
            Assert.AreEqual(638280883031601660, apex.DateTime.Ticks);
            Assert.AreEqual(638281121517961734, set.DateTime.Ticks);
            Assert.AreEqual(638280536545219738, localRiseDateTime.Ticks);
            Assert.AreEqual(638280775031601660, localApexDateTime.Ticks);
            Assert.AreEqual(638281013517961734, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void SanJavier_M17_GAST_Trajectory_AntiSetting_August19_2023_1300()
        {
            // Arrange

            // LAT: -30.0, LON: -60.0 (San Javier, Santa Fe, Argentina)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-60.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Argentina Standard Time (IANA: America/Buenos_Aires)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Buenos_Aires");

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
            Assert.AreEqual(0.0012455187071677793, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76778574911401, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17906301605542, apexCoordinate.Components.Inclination);
            Assert.AreEqual(2.634275661277943E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(0.0012583383482807945, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23222055366432, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280644545236962, rise.DateTime.Ticks);
            Assert.AreEqual(638280883031601660, apex.DateTime.Ticks);
            Assert.AreEqual(638281121517961734, set.DateTime.Ticks);
            Assert.AreEqual(638280536545236962, localRiseDateTime.Ticks);
            Assert.AreEqual(638280775031601660, localApexDateTime.Ticks);
            Assert.AreEqual(638281013517961734, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void SanJavier_M17_ERA_Trajectory_Rising_August18_2023_1800()
        {
            // Arrange

            // LAT: -30.0, LON: -60.0 (San Javier, Santa Fe, Argentina)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-60.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Argentina Standard Time (IANA: America/Buenos_Aires)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Buenos_Aires");

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
            Assert.AreEqual(-7.845372920201044E-07, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76574004045534, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.1766666999999, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.99999250816666, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-3.1968444886842917E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23425848860595, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279780708643411, rise.DateTime.Ticks);
            Assert.AreEqual(638280019195028950, apex.DateTime.Ticks);
            Assert.AreEqual(638280257681412199, set.DateTime.Ticks);
            Assert.AreEqual(638279672708643411, localRiseDateTime.Ticks);
            Assert.AreEqual(638279911195028950, localApexDateTime.Ticks);
            Assert.AreEqual(638280149681412199, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void SanJavier_M17_ERA_Trajectory_Setting_August19_2023_0100()
        {
            // Arrange

            // LAT: -30.0, LON: -60.0 (San Javier, Santa Fe, Argentina)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-60.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Argentina Standard Time (IANA: America/Buenos_Aires)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Buenos_Aires");

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
            Assert.AreEqual(-2.4612078846075747E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76574106282914, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.1766666999999, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.99999250816666, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-3.1968444886842917E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23425848860595, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279780708638517, rise.DateTime.Ticks);
            Assert.AreEqual(638280019195028950, apex.DateTime.Ticks);
            Assert.AreEqual(638280257681412199, set.DateTime.Ticks);
            Assert.AreEqual(638279672708638517, localRiseDateTime.Ticks);
            Assert.AreEqual(638279911195028950, localApexDateTime.Ticks);
            Assert.AreEqual(638280149681412199, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void SanJavier_M17_ERA_Trajectory_AntiRising_August19_2023_0600()
        {
            // Arrange

            // LAT: -30.0, LON: -60.0 (San Javier, Santa Fe, Argentina)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-60.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Argentina Standard Time (IANA: America/Buenos_Aires)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Buenos_Aires");

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
            Assert.AreEqual(2.135629671821251E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76573825984195, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.1766666999999, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.99999250816666, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-3.818213940576243E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23425810971708, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280642349624153, rise.DateTime.Ticks);
            Assert.AreEqual(638280880836001168, apex.DateTime.Ticks);
            Assert.AreEqual(638281119322384417, set.DateTime.Ticks);
            Assert.AreEqual(638280534349624153, localRiseDateTime.Ticks);
            Assert.AreEqual(638280772836001168, localApexDateTime.Ticks);
            Assert.AreEqual(638281011322384417, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void SanJavier_M17_ERA_Trajectory_AntiSetting_August19_2023_1300()
        {
            // Arrange

            // LAT: -30.0, LON: -60.0 (San Javier, Santa Fe, Argentina)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-60.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // Argentina Standard Time (IANA: America/Buenos_Aires)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Buenos_Aires");

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
            Assert.AreEqual(4.6169748363538873E-07, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76573928054593, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.1766666999999, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.99999250816666, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-3.818213940576243E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23425810971708, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280642349619267, rise.DateTime.Ticks);
            Assert.AreEqual(638280880836001168, apex.DateTime.Ticks);
            Assert.AreEqual(638281119322384417, set.DateTime.Ticks);
            Assert.AreEqual(638280534349619267, localRiseDateTime.Ticks);
            Assert.AreEqual(638280772836001168, localApexDateTime.Ticks);
            Assert.AreEqual(638281011322384417, localSetDateTime.Ticks);
        }
    }
}