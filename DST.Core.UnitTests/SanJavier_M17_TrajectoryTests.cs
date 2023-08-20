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
            Assert.AreEqual(-2.395028786850162E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.7657410224755, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666669999974, apexCoordinate.Components.Inclination);
            Assert.AreEqual(1.1768457563181822E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(2.4461701857967803E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23426192951453, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279780704580000, riseDateTime.Ticks);
            Assert.AreEqual(638280019190940000, apexDateTime.Ticks);
            Assert.AreEqual(638280257677300000, setDateTime.Ticks);
            Assert.AreEqual(638279672704580000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279911190940000, localApexDateTime.Ticks);
            Assert.AreEqual(638280149677300000, localSetDateTime.Ticks);
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
            Assert.AreEqual(-2.395028786850162E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.7657410224755, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666669999974, apexCoordinate.Components.Inclination);
            Assert.AreEqual(1.1768457563181822E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(2.4461701857967803E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23426192951453, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279780704580000, riseDateTime.Ticks);
            Assert.AreEqual(638280019190940000, apexDateTime.Ticks);
            Assert.AreEqual(638280257677300000, setDateTime.Ticks);
            Assert.AreEqual(638279672704580000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279911190940000, localApexDateTime.Ticks);
            Assert.AreEqual(638280149677300000, localSetDateTime.Ticks);
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
            Assert.AreEqual(-7.876748782109644E-07, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76574004236852, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666670000001, apexCoordinate.Components.Inclination);
            Assert.AreEqual(0.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(8.388147374321021E-07, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.2342609494067, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280642345490000, riseDateTime.Ticks);
            Assert.AreEqual(638280880831850000, apexDateTime.Ticks);
            Assert.AreEqual(638281119318210000, setDateTime.Ticks);
            Assert.AreEqual(638280534345490000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280772831850000, localApexDateTime.Ticks);
            Assert.AreEqual(638281011318210000, localSetDateTime.Ticks);
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
            Assert.AreEqual(-7.876748782109644E-07, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76574004236852, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666670000001, apexCoordinate.Components.Inclination);
            Assert.AreEqual(0.0, apexCoordinate.Components.Rotation);
            Assert.AreEqual(8.388147374321021E-07, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.2342609494067, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280642345490000, riseDateTime.Ticks);
            Assert.AreEqual(638280880831850000, apexDateTime.Ticks);
            Assert.AreEqual(638281119318210000, setDateTime.Ticks);
            Assert.AreEqual(638280534345490000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280772831850000, localApexDateTime.Ticks);
            Assert.AreEqual(638281011318210000, localSetDateTime.Ticks);
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
            Assert.AreEqual(0.0012444306329878634, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76777838366566, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17905778072202, apexCoordinate.Components.Inclination);
            Assert.AreEqual(3.966143882204574E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(0.0012596470301719398, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.2322256005511, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279782904280000, riseDateTime.Ticks);
            Assert.AreEqual(638280021390630000, apexDateTime.Ticks);
            Assert.AreEqual(638280259876990000, setDateTime.Ticks);
            Assert.AreEqual(638279674904280000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279913390630000, localApexDateTime.Ticks);
            Assert.AreEqual(638280151876990000, localSetDateTime.Ticks);
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
            Assert.AreEqual(0.001251282501300474, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76777420564187, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17905778072202, apexCoordinate.Components.Inclination);
            Assert.AreEqual(3.966143882204574E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(0.0012596470301719398, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.2322256005511, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279782904300000, riseDateTime.Ticks);
            Assert.AreEqual(638280021390630000, apexDateTime.Ticks);
            Assert.AreEqual(638280259876990000, setDateTime.Ticks);
            Assert.AreEqual(638279674904300000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279913390630000, localApexDateTime.Ticks);
            Assert.AreEqual(638280151876990000, localSetDateTime.Ticks);
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
            Assert.AreEqual(0.001236281703370043, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76779138150897, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17906301605508, apexCoordinate.Components.Inclination);
            Assert.AreEqual(2.9489198374443362E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(0.0012589324035871148, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23222091589793, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280644545210000, riseDateTime.Ticks);
            Assert.AreEqual(638280883031600000, apexDateTime.Ticks);
            Assert.AreEqual(638281121517960000, setDateTime.Ticks);
            Assert.AreEqual(638280536545210000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280775031600000, localApexDateTime.Ticks);
            Assert.AreEqual(638281013517960000, localSetDateTime.Ticks);
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
            Assert.AreEqual(0.0012431335710671948, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76778720348469, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17906301605508, apexCoordinate.Components.Inclination);
            Assert.AreEqual(2.9489198374443362E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(0.0012589324035871148, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23222091589793, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280644545230000, riseDateTime.Ticks);
            Assert.AreEqual(638280883031600000, apexDateTime.Ticks);
            Assert.AreEqual(638281121517960000, setDateTime.Ticks);
            Assert.AreEqual(638280536545230000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280775031600000, localApexDateTime.Ticks);
            Assert.AreEqual(638281013517960000, localSetDateTime.Ticks);
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
            Assert.AreEqual(-1.953136347765394E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76574075302528, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666669999996, apexCoordinate.Components.Inclination);
            Assert.AreEqual(5.399738656763834E-06, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-2.4434723968624894E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23425894798532, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279780708640000, riseDateTime.Ticks);
            Assert.AreEqual(638280019195020000, apexDateTime.Ticks);
            Assert.AreEqual(638280257681410000, setDateTime.Ticks);
            Assert.AreEqual(638279672708640000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279911195020000, localApexDateTime.Ticks);
            Assert.AreEqual(638280149681410000, localSetDateTime.Ticks);
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
            Assert.AreEqual(-5.379111712500162E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76574284206265, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666669999996, apexCoordinate.Components.Inclination);
            Assert.AreEqual(5.399738656763834E-06, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-2.4434723968624894E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23425894798532, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279780708630000, riseDateTime.Ticks);
            Assert.AreEqual(638280019195020000, apexDateTime.Ticks);
            Assert.AreEqual(638280257681410000, setDateTime.Ticks);
            Assert.AreEqual(638279672708630000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279911195020000, localApexDateTime.Ticks);
            Assert.AreEqual(638280149681410000, localSetDateTime.Ticks);
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
            Assert.AreEqual(7.128225613146918E-07, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76573912741884, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666669999996, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.99999460026135, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-2.304960617038887E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23425903244487, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280642349620000, riseDateTime.Ticks);
            Assert.AreEqual(638280880836000000, apexDateTime.Ticks);
            Assert.AreEqual(638281119322380000, setDateTime.Ticks);
            Assert.AreEqual(638280534349620000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280772836000000, localApexDateTime.Ticks);
            Assert.AreEqual(638281011322380000, localSetDateTime.Ticks);
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
            Assert.AreEqual(-2.713152923661255E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.7657412164562, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666669999996, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.99999460026135, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-2.304960617038887E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23425903244487, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280642349610000, riseDateTime.Ticks);
            Assert.AreEqual(638280880836000000, apexDateTime.Ticks);
            Assert.AreEqual(638281119322380000, setDateTime.Ticks);
            Assert.AreEqual(638280534349610000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280772836000000, localApexDateTime.Ticks);
            Assert.AreEqual(638281011322380000, localSetDateTime.Ticks);
        }
    }
}