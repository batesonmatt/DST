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
            Assert.AreEqual(-3.207105692126788E-10, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76573956226808, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666670000001, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.9999982924527, apexCoordinate.Components.Rotation);
            Assert.AreEqual(7.213498017559256E-11, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23426043797144, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279278080725560, rise.DateTime.Ticks);
            Assert.AreEqual(638279516567085635, apex.DateTime.Ticks);
            Assert.AreEqual(638279755053445710, set.DateTime.Ticks);
            Assert.AreEqual(638279638080725560, localRiseDateTime.Ticks);
            Assert.AreEqual(638279876567085635, localApexDateTime.Ticks);
            Assert.AreEqual(638280115053445710, localSetDateTime.Ticks);
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
            Assert.AreEqual(2.0859667878378476E-11, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76573956205982, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666670000001, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.9999982924527, apexCoordinate.Components.Rotation);
            Assert.AreEqual(7.213498017559256E-11, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23426043797144, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279278080725561, rise.DateTime.Ticks);
            Assert.AreEqual(638279516567085635, apex.DateTime.Ticks);
            Assert.AreEqual(638279755053445710, set.DateTime.Ticks);
            Assert.AreEqual(638279638080725561, localRiseDateTime.Ticks);
            Assert.AreEqual(638279876567085635, localApexDateTime.Ticks);
            Assert.AreEqual(638280115053445710, localSetDateTime.Ticks);
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
            Assert.AreEqual(-9.100631359615363E-11, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76573956212803, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666670000001, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.9999982924527, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.8400940081873102E-10, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23426043803966, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280139721630869, rise.DateTime.Ticks);
            Assert.AreEqual(638280378207990943, apex.DateTime.Ticks);
            Assert.AreEqual(638280616694351018, set.DateTime.Ticks);
            Assert.AreEqual(638280499721630869, localRiseDateTime.Ticks);
            Assert.AreEqual(638280738207990943, localApexDateTime.Ticks);
            Assert.AreEqual(638280976694351018, localSetDateTime.Ticks);
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
            Assert.AreEqual(-9.100631359615363E-11, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76573956212803, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666670000001, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.9999982924527, apexCoordinate.Components.Rotation);
            Assert.AreEqual(1.8400940081873102E-10, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23426043803966, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280139721630869, rise.DateTime.Ticks);
            Assert.AreEqual(638280378207990943, apex.DateTime.Ticks);
            Assert.AreEqual(638280616694351018, set.DateTime.Ticks);
            Assert.AreEqual(638280499721630869, localRiseDateTime.Ticks);
            Assert.AreEqual(638280738207990943, localApexDateTime.Ticks);
            Assert.AreEqual(638280976694351018, localSetDateTime.Ticks);
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
            Assert.AreEqual(0.0012431457414946725, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76777140869434, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17905204420987, apexCoordinate.Components.Inclination);
            Assert.AreEqual(2.532701929461308E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(0.0012538969368542008, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23222773183613, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279280280392470, rise.DateTime.Ticks);
            Assert.AreEqual(638279518766743611, apex.DateTime.Ticks);
            Assert.AreEqual(638279757253103686, set.DateTime.Ticks);
            Assert.AreEqual(638279640280392470, localRiseDateTime.Ticks);
            Assert.AreEqual(638279878766743611, localApexDateTime.Ticks);
            Assert.AreEqual(638280117253103686, localSetDateTime.Ticks);
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
            Assert.AreEqual(0.001248745430614784, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76776799420475, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17905204420987, apexCoordinate.Components.Inclination);
            Assert.AreEqual(2.532701929461308E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(0.0012538969368542008, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23222773183613, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279280280408815, rise.DateTime.Ticks);
            Assert.AreEqual(638279518766743611, apex.DateTime.Ticks);
            Assert.AreEqual(638279757253103686, set.DateTime.Ticks);
            Assert.AreEqual(638279640280408815, localRiseDateTime.Ticks);
            Assert.AreEqual(638279878766743611, localApexDateTime.Ticks);
            Assert.AreEqual(638280117253103686, localSetDateTime.Ticks);
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
            Assert.AreEqual(0.0012386874444333244, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76778606547992, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17906067606485, apexCoordinate.Components.Inclination);
            Assert.AreEqual(2.765225093091718E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(0.0012582317592645482, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23222214251678, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280141921326195, rise.DateTime.Ticks);
            Assert.AreEqual(638280380407705839, apex.DateTime.Ticks);
            Assert.AreEqual(638280618894065913, set.DateTime.Ticks);
            Assert.AreEqual(638280501921326195, localRiseDateTime.Ticks);
            Assert.AreEqual(638280740407705839, localApexDateTime.Ticks);
            Assert.AreEqual(638280978894065913, localSetDateTime.Ticks);
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
            Assert.AreEqual(0.0012445913564537864, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76778246548544, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17906067606485, apexCoordinate.Components.Inclination);
            Assert.AreEqual(2.765225093091718E-05, apexCoordinate.Components.Rotation);
            Assert.AreEqual(0.0012582317592645482, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23222214251678, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280141921343428, rise.DateTime.Ticks);
            Assert.AreEqual(638280380407705839, apex.DateTime.Ticks);
            Assert.AreEqual(638280618894065913, set.DateTime.Ticks);
            Assert.AreEqual(638280501921343428, localRiseDateTime.Ticks);
            Assert.AreEqual(638280740407705839, localApexDateTime.Ticks);
            Assert.AreEqual(638280978894065913, localSetDateTime.Ticks);
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
            Assert.AreEqual(-1.354291612187808E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76574038787116, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666669999986, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.99999084429766, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-4.004512959454587E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23425799611857, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279278084743709, rise.DateTime.Ticks);
            Assert.AreEqual(638279516571130911, apex.DateTime.Ticks);
            Assert.AreEqual(638279755057514160, set.DateTime.Ticks);
            Assert.AreEqual(638279638084743709, localRiseDateTime.Ticks);
            Assert.AreEqual(638279876571130911, localApexDateTime.Ticks);
            Assert.AreEqual(638280115057514160, localSetDateTime.Ticks);
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
            Assert.AreEqual(-3.415700518871745E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76574164484464, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.17666669999986, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.99999084429766, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-4.004512959454587E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23425799611857, setCoordinate.Components.Rotation);
            Assert.AreEqual(638279278084737692, rise.DateTime.Ticks);
            Assert.AreEqual(638279516571130911, apex.DateTime.Ticks);
            Assert.AreEqual(638279755057514160, set.DateTime.Ticks);
            Assert.AreEqual(638279638084737692, localRiseDateTime.Ticks);
            Assert.AreEqual(638279876571130911, localApexDateTime.Ticks);
            Assert.AreEqual(638280115057514160, localSetDateTime.Ticks);
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
            Assert.AreEqual(1.899141678535996E-06, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76573840404387, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.1766666999999, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.99999250816666, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-3.148002008401818E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23425851838834, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280139725722906, rise.DateTime.Ticks);
            Assert.AreEqual(638280378212100611, apex.DateTime.Ticks);
            Assert.AreEqual(638280616698483860, set.DateTime.Ticks);
            Assert.AreEqual(638280499725722906, localRiseDateTime.Ticks);
            Assert.AreEqual(638280738212100611, localApexDateTime.Ticks);
            Assert.AreEqual(638280976698483860, localSetDateTime.Ticks);
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
            Assert.AreEqual(2.063667346466264E-07, riseCoordinate.Components.Inclination);
            Assert.AreEqual(108.76573943623748, riseCoordinate.Components.Rotation);
            Assert.AreEqual(76.1766666999999, apexCoordinate.Components.Inclination);
            Assert.AreEqual(359.99999250816666, apexCoordinate.Components.Rotation);
            Assert.AreEqual(-3.148002008401818E-06, setCoordinate.Components.Inclination);
            Assert.AreEqual(251.23425851838834, setCoordinate.Components.Rotation);
            Assert.AreEqual(638280139725717965, rise.DateTime.Ticks);
            Assert.AreEqual(638280378212100611, apex.DateTime.Ticks);
            Assert.AreEqual(638280616698483860, set.DateTime.Ticks);
            Assert.AreEqual(638280499725717965, localRiseDateTime.Ticks);
            Assert.AreEqual(638280738212100611, localApexDateTime.Ticks);
            Assert.AreEqual(638280976698483860, localSetDateTime.Ticks);
        }
    }
}