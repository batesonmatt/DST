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
    public class Couradda_M17_TrajectoryTests
    {
        [TestMethod]
        public void Couradda_M17_GMST_Trajectory_Rising_August18_2023_1800()
        {
            // Arrange

            // LAT: -30.0, LON: 150.0 (Couradda, New South Wales, Australia)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(150.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // AUS Eastern Standard Time (IANA: Australia/Sydney)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Australia/Sydney");

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
            Assert.AreEqual(-1.9051630602007208E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76574072377288, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666669999986, apexCoordinate.Components.Inclination);
            Assert.AreEqual(9.155702348202485E-06, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.9563044281388136E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23426163081194, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279278080720000, riseDateTime.Ticks);
            Assert.AreEqual(638279516567080000, apexDateTime.Ticks);
            Assert.AreEqual(638279755053440000, setDateTime.Ticks);
            Assert.AreEqual(638279638080720000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279876567080000, localApexDateTime.Ticks);
            Assert.AreEqual(638280115053440000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Couradda_M17_GMST_Trajectory_Setting_August19_2023_0100()
        {
            // Arrange

            // LAT: -30.0, LON: 150.0 (Couradda, New South Wales, Australia)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(150.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // AUS Eastern Standard Time (IANA: Australia/Sydney)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Australia/Sydney");

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
            Assert.AreEqual(-1.9051630602007208E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76574072377288, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666669999986, apexCoordinate.Components.Inclination);
            Assert.AreEqual(9.155702348202485E-06, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.9563044281388136E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23426163081194, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279278080720000, riseDateTime.Ticks);
            Assert.AreEqual(638279516567080000, apexDateTime.Ticks);
            Assert.AreEqual(638279755053440000, setDateTime.Ticks);
            Assert.AreEqual(638279638080720000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279876567080000, localApexDateTime.Ticks);
            Assert.AreEqual(638280115053440000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Couradda_M17_GMST_Trajectory_AntiRising_August19_2023_0600()
        {
            // Arrange

            // LAT: -30.0, LON: 150.0 (Couradda, New South Wales, Australia)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(150.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // AUS Eastern Standard Time (IANA: Australia/Sydney)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Australia/Sydney");

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
            Assert.AreEqual(-2.978091515615233E-07, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.7657397436659, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666670000001, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.9999982924527, apexCoordinate.Components.Rotation);
            Assert.AreEqual(3.4894899408663184E-07, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23426065070407, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280139721630000, riseDateTime.Ticks);
            Assert.AreEqual(638280378207990000, apexDateTime.Ticks);
            Assert.AreEqual(638280616694350000, setDateTime.Ticks);
            Assert.AreEqual(638280499721630000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280738207990000, localApexDateTime.Ticks);
            Assert.AreEqual(638280976694350000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Couradda_M17_GMST_Trajectory_AntiSetting_August19_2023_1300()
        {
            // Arrange

            // LAT: -30.0, LON: 150.0 (Couradda, New South Wales, Australia)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(150.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // AUS Eastern Standard Time (IANA: Australia/Sydney)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Australia/Sydney");

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
            Assert.AreEqual(-2.978091515615233E-07, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.7657397436659, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666670000001, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.9999982924527, apexCoordinate.Components.Rotation);
            Assert.AreEqual(3.4894899408663184E-07, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23426065070407, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280139721630000, riseDateTime.Ticks);
            Assert.AreEqual(638280378207990000, apexDateTime.Ticks);
            Assert.AreEqual(638280616694350000, setDateTime.Ticks);
            Assert.AreEqual(638280499721630000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280738207990000, localApexDateTime.Ticks);
            Assert.AreEqual(638280976694350000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Couradda_M17_GAST_Trajectory_Rising_August18_2023_1800()
        {
            // Arrange

            // LAT: -30.0, LON: 150.0 (Couradda, New South Wales, Australia)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(150.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // AUS Eastern Standard Time (IANA: Australia/Sydney)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Australia/Sydney");

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
            Assert.AreEqual(0.0012422995365394545, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76777192467986, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17905204420923, apexCoordinate.Components.Inclination);
            Assert.AreEqual(3.129985888245142E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(0.0012551597360380535, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23222850184587, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279280280390000, riseDateTime.Ticks);
            Assert.AreEqual(638279518766740000, apexDateTime.Ticks);
            Assert.AreEqual(638279757253100000, setDateTime.Ticks);
            Assert.AreEqual(638279640280390000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279878766740000, localApexDateTime.Ticks);
            Assert.AreEqual(638280117253100000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Couradda_M17_GAST_Trajectory_Setting_August19_2023_0100()
        {
            // Arrange

            // LAT: -30.0, LON: 150.0 (Couradda, New South Wales, Australia)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(150.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // AUS Eastern Standard Time (IANA: Australia/Sydney)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Australia/Sydney");

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
            Assert.AreEqual(0.0012457254693487895, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76776983566877, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17905204420923, apexCoordinate.Components.Inclination);
            Assert.AreEqual(3.129985888245142E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(0.0012551597360380535, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23222850184587, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279280280400000, riseDateTime.Ticks);
            Assert.AreEqual(638279518766740000, apexDateTime.Ticks);
            Assert.AreEqual(638279757253100000, setDateTime.Ticks);
            Assert.AreEqual(638279640280400000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279878766740000, localApexDateTime.Ticks);
            Assert.AreEqual(638280117253100000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Couradda_M17_GAST_Trajectory_AntiRising_August19_2023_0600()
        {
            // Arrange

            // LAT: -30.0, LON: 150.0 (Couradda, New South Wales, Australia)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(150.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // AUS Eastern Standard Time (IANA: Australia/Sydney)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Australia/Sydney");

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
            Assert.AreEqual(0.0012365650792293208, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.7677873596224, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17906067606359, apexCoordinate.Components.Inclination);
            Assert.AreEqual(3.757574115580966E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(0.0012602575149960546, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23222337775002, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280141921320000, riseDateTime.Ticks);
            Assert.AreEqual(638280380407700000, apexDateTime.Ticks);
            Assert.AreEqual(638280618894060000, setDateTime.Ticks);
            Assert.AreEqual(638280501921320000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280740407700000, localApexDateTime.Ticks);
            Assert.AreEqual(638280978894060000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Couradda_M17_GAST_Trajectory_AntiSetting_August19_2023_1300()
        {
            // Arrange

            // LAT: -30.0, LON: 150.0 (Couradda, New South Wales, Australia)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(150.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // AUS Eastern Standard Time (IANA: Australia/Sydney)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Australia/Sydney");

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
            Assert.AreEqual(0.001243416945563606, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.7677831815992, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17906067606359, apexCoordinate.Components.Inclination);
            Assert.AreEqual(3.757574115580966E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(0.0012602575149960546, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23222337775002, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280141921340000, riseDateTime.Ticks);
            Assert.AreEqual(638280380407700000, apexDateTime.Ticks);
            Assert.AreEqual(638280618894060000, setDateTime.Ticks);
            Assert.AreEqual(638280501921340000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280740407700000, localApexDateTime.Ticks);
            Assert.AreEqual(638280978894060000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Couradda_M17_ERA_Trajectory_Rising_August18_2023_1800()
        {
            // Arrange

            // LAT: -30.0, LON: 150.0 (Couradda, New South Wales, Australia)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(150.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // AUS Eastern Standard Time (IANA: Australia/Sydney)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Australia/Sydney");

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
            Assert.AreEqual(-2.624987416766089E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76574116269603, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666669999986, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.99999084429766, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-2.5793074200919364E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23425886515798, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279278084740000, riseDateTime.Ticks);
            Assert.AreEqual(638279516571130000, apexDateTime.Ticks);
            Assert.AreEqual(638279755057510000, setDateTime.Ticks);
            Assert.AreEqual(638279638084740000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279876571130000, localApexDateTime.Ticks);
            Assert.AreEqual(638280115057510000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Couradda_M17_ERA_Trajectory_Setting_August19_2023_0100()
        {
            // Arrange

            // LAT: -30.0, LON: 150.0 (Couradda, New South Wales, Australia)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(150.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // AUS Eastern Standard Time (IANA: Australia/Sydney)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Australia/Sydney");

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
            Assert.AreEqual(-6.0509613604153856E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76574325173257, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666669999986, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.99999084429766, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-2.5793074200919364E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23425886515798, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279278084730000, riseDateTime.Ticks);
            Assert.AreEqual(638279516571130000, apexDateTime.Ticks);
            Assert.AreEqual(638279755057510000, setDateTime.Ticks);
            Assert.AreEqual(638279638084730000, localRiseDateTime.Ticks);
            Assert.AreEqual(638279876571130000, localApexDateTime.Ticks);
            Assert.AreEqual(638280115057510000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Couradda_M17_ERA_Trajectory_AntiRising_August19_2023_0600()
        {
            // Arrange

            // LAT: -30.0, LON: 150.0 (Couradda, New South Wales, Australia)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(150.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // AUS Eastern Standard Time (IANA: Australia/Sydney)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Australia/Sydney");

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
            Assert.AreEqual(9.035521131823989E-07, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76573901111877, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666669999996, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.99999460026135, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-1.8255762483931903E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23425932475632, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280139725720000, riseDateTime.Ticks);
            Assert.AreEqual(638280378212100000, apexDateTime.Ticks);
            Assert.AreEqual(638280616698480000, setDateTime.Ticks);
            Assert.AreEqual(638280499725720000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280738212100000, localApexDateTime.Ticks);
            Assert.AreEqual(638280976698480000, localSetDateTime.Ticks);
        }

        [TestMethod]
        public void Couradda_M17_ERA_Trajectory_AntiSetting_August19_2023_1300()
        {
            // Arrange

            // LAT: -30.0, LON: 150.0 (Couradda, New South Wales, Australia)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(150.0), latitude: new Angle(-30.0));

            // RA: 18.3405556hr, DEC: -16.1766667° (M17)
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

            // AUS Eastern Standard Time (IANA: Australia/Sydney)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Australia/Sydney");

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
            Assert.AreEqual(-2.522423244499805E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76574110015609, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666669999996, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.99999460026135, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-1.8255762483931903E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23425932475632, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280139725710000, riseDateTime.Ticks);
            Assert.AreEqual(638280378212100000, apexDateTime.Ticks);
            Assert.AreEqual(638280616698480000, setDateTime.Ticks);
            Assert.AreEqual(638280499725710000, localRiseDateTime.Ticks);
            Assert.AreEqual(638280738212100000, localApexDateTime.Ticks);
            Assert.AreEqual(638280976698480000, localSetDateTime.Ticks);
        }
    }
}