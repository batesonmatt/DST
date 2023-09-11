using DST.Core.Coordinate;
using DST.Core.DateAndTime;
using DST.Core.DateTimeAdder;
using DST.Core.DateTimesBuilder;
using DST.Core.Observer;
using DST.Core.Physics;
using DST.Core.TimeKeeper;
using DST.Core.TimeScalable;
using DST.Core.Tracker;
using DST.Core.Trajectory;
using DST.Core.Vector;

namespace DST.Core.Tests
{
    internal class Program
    {
        private static void NullIsland_M17_ERA_Trajectory_AntiSetting_August19_2023_1300()
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
            IMutableDateTime riseDateTime = DateTimeFactory.ConvertToMutable(rise.DateTime);
            IMutableDateTime apexDateTime = DateTimeFactory.ConvertToMutable(apex.DateTime);
            IMutableDateTime setDateTime = DateTimeFactory.ConvertToMutable(set.DateTime);
            DateTime localRiseDateTime = riseDateTime.ToLocalTime();
            DateTime localApexDateTime = apexDateTime.ToLocalTime();
            DateTime localSetDateTime = setDateTime.ToLocalTime();
            
            bool isRiseSet = trajectory is IRiseSetTrajectory;
            bool isNotAboveHorizon = !riseSet.IsAboveHorizon(dateTime);
            bool isNotRising = !riseSet.IsRising(dateTime);
            bool isNotSetting = !riseSet.IsSetting(dateTime);

            // Assert
            Console.WriteLine("Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));");
            Console.WriteLine("Assert.IsFalse(riseSet.IsAboveHorizon(dateTime));");
            Console.WriteLine("Assert.IsFalse(riseSet.IsRising(dateTime));");
            Console.WriteLine("Assert.IsFalse(riseSet.IsSetting(dateTime));");
            Console.WriteLine($"Assert.AreEqual({riseCoordinate.Components.Inclination.TotalDegrees}, riseCoordinate.Components.Inclination);");
            Console.WriteLine($"Assert.AreEqual({riseCoordinate.Components.Rotation.TotalDegrees}, riseCoordinate.Components.Rotation);");
            Console.WriteLine($"Assert.AreEqual({apexCoordinate.Components.Inclination.TotalDegrees}, apexCoordinate.Components.Inclination);");
            Console.WriteLine($"Assert.AreEqual({apexCoordinate.Components.Rotation.TotalDegrees}, apexCoordinate.Components.Rotation);");
            Console.WriteLine($"Assert.AreEqual({setCoordinate.Components.Inclination.TotalDegrees}, setCoordinate.Components.Inclination);");
            Console.WriteLine($"Assert.AreEqual({setCoordinate.Components.Rotation.TotalDegrees}, setCoordinate.Components.Rotation);");
            Console.WriteLine($"Assert.AreEqual({riseDateTime.Ticks}, riseDateTime.Ticks);");
            Console.WriteLine($"Assert.AreEqual({apexDateTime.Ticks}, apexDateTime.Ticks);");
            Console.WriteLine($"Assert.AreEqual({setDateTime.Ticks}, setDateTime.Ticks);");
            Console.WriteLine($"Assert.AreEqual({localRiseDateTime.Ticks}, localRiseDateTime.Ticks);");
            Console.WriteLine($"Assert.AreEqual({localApexDateTime.Ticks}, localApexDateTime.Ticks);");
            Console.WriteLine($"Assert.AreEqual({localSetDateTime.Ticks}, localSetDateTime.Ticks);");
        }

        private static void TestTrack()
        {
            // My location (LAT, LON): 29.4944768, -95.1123968
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-95.1123968), latitude: new Angle(29.4944768));
            //longitude: Angle.Zero, latitude: new Angle(90.0));
            //longitude: Angle.Zero, latitude: new Angle(-80.0));

            // M42: Orion Diffuse Nebula
            IEquatorialCoordinate m42 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(new TimeSpan(5, 35, 17)), declination: new Angle(-5, -23, -28));

            // M31: Andromeda Galaxy
            IEquatorialCoordinate m31 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromMinutes(42.7383)), declination: new Angle(41, 16, 9));

            // Polaris
            IEquatorialCoordinate polaris = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(new TimeSpan(2, 31, 49)), declination: new Angle(89, 15, 51));

            // Some point on the celestial equator
            IEquatorialCoordinate eq = CoordinateFactory.CreateEquatorial(
                rightAscension: Angle.Zero, declination: Angle.Zero);

            // Some northern point at RA 0
            IEquatorialCoordinate ra = CoordinateFactory.CreateEquatorial(
                rightAscension: Angle.Zero, declination: new Angle(41, 16, 9));

            // The North Celestial Pole, or Celestial Intermediate Pole (CIP)
            IEquatorialCoordinate north = CoordinateFactory.CreateEquatorial(
                rightAscension: Angle.Zero, declination: new Angle(90.0));

            // Test equatorial coordinate
            //RightAscension = 16.6947889, Declination = 36.4598611
            IEquatorialCoordinate test = CoordinateFactory.CreateEquatorial(
                rightAscension: new(TimeSpan.FromHours(16.6947889)), declination: new(36.4598611));

            // My timezone: America/Chicago
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.ERA);
            IObserver observer = ObserverFactory.Create(dateTimeInfo, location, m42, timeKeeper);
            ITrajectory trajectory = TrajectoryCalculator.Calculate(observer);

            Console.WriteLine($"Location: {observer.Origin}");
            Console.WriteLine($"Target: {observer.Destination}");
            Console.WriteLine($"Visibility: {trajectory}\n");

            // For spring DST testing.
            //AstronomicalDateTime start = 
            //    new AstronomicalDateTime(new DateTime(2022, 3, 12, 18, 0, 0, DateTimeKind.Local), AstronomicalDateTime.UnspecifiedKind.IsLocal);

            //IAstronomicalDateTime now = DateTimeFactory.ConvertToAstronomical(dateTimeInfo.Now);
            //IAstronomicalDateTime start = DateTimeFactory.ConvertToAstronomical(dateTimeInfo.Now);

            AstronomicalDateTime start = new(new DateTime(2023, 10, 1, 22, 0, 0), dateTimeInfo);

            IMutableDateTime mutable;

            if (trajectory is IVariableTrajectory variableTrajectory)
            {
                if (variableTrajectory.IsAboveHorizon(start))
                {
                    Console.WriteLine("This object is in your sky now!");
                }
                else
                {
                    Console.WriteLine("This object is not yet in your sky.");
                }

                Console.WriteLine();

                if (trajectory is IRiseSetTrajectory riseSetTrajectory)
                {
                    IVector rise = riseSetTrajectory.GetRise(start);
                    IVector apex = riseSetTrajectory.GetApex(DateTimeFactory.ConvertToAstronomical(rise.DateTime));
                    IVector set = riseSetTrajectory.GetSet(DateTimeFactory.ConvertToAstronomical(rise.DateTime));

                    mutable = DateTimeFactory.ConvertToMutable(rise.DateTime);
                    Console.WriteLine($"Rise: {mutable.ToLocalTime()}\tPosition: {rise.Coordinate}");

                    mutable = DateTimeFactory.ConvertToMutable(apex.DateTime);
                    Console.WriteLine($"Apex: {mutable.ToLocalTime()}\tPosition: {apex.Coordinate}");

                    mutable = DateTimeFactory.ConvertToMutable(set.DateTime);
                    Console.WriteLine($"Set: {mutable.ToLocalTime()}\tPosition: {set.Coordinate}");
                }
                else
                {
                    IVector apex = variableTrajectory.GetApex(start);

                    mutable = DateTimeFactory.ConvertToMutable(apex.DateTime);
                    Console.WriteLine($"Apex: {mutable.ToLocalTime()}\tPosition: {apex.Coordinate}");
                }
            }
            else
            {
                if (trajectory.IsAboveHorizon(start))
                {
                    Console.WriteLine("This object is in your sky now!");
                }
                else
                {
                    Console.WriteLine("This object is never visible at your location.");
                }
            }

            Console.WriteLine();

            ITimeScalable timeScalable = TimeScalableFactory.Create(TimeScale.StellarTime);
            IDateTimeAdder dateTimeAdder = DateTimeAdderFactory.Create(timeScalable, TimeUnit.Months);
            IDateTimesBuilder dateTimesBuilder = DateTimesBuilderFactory.Create(dateTimeAdder, true);
            IAstronomicalDateTime[] dateTimes = DateTimeFactory.ConvertToAstronomical(
                dateTimesBuilder.Build(start, 12, 1));

            ITracker tracker = TrackerFactory.Create(observer);

            ICoordinate[] positions = tracker.Track(dateTimes);
            int results = 0;

            for (int i = 0; i < positions.Length; i++)
            {
                if (positions[i] != null)
                {
                    mutable = DateTimeFactory.ConvertToMutable(dateTimes[i]);

                    if (observer is ILocalObserver localObserver)
                    {
                        Console.Write($"ROT: {localObserver.TimeKeeper.Calculate(dateTimes[i])}\t");
                        Console.Write($"LROT: {localObserver.LocalTimeKeeper.Calculate(localObserver, dateTimes[i])}\t");
                        Console.Write($"LHA: {localObserver.LocalHourAngle.Calculate(localObserver, dateTimes[i])}\t");
                    }
                    
                    Console.Write($"Period: {mutable.ToLocalTime()}\t");
                    Console.Write($"Position: {positions[i].Format(FormatType.Decimal)}\t");
                    Console.Write($"Visible: {trajectory.IsAboveHorizon(dateTimes[i])}\n");
                }
                else
                {
                    Console.WriteLine("null");
                }

                results++;
            }

            Console.WriteLine($"\n{results} results");
        }

        private static void TestAdds()
        {
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");

            IMutableDateTime start = DateTimeFactory.ConvertToMutable(dateTimeInfo.Now);
            Console.WriteLine($"Start: {start}");
            Console.WriteLine("--------------------");

            // GMST - Sidereal Time

            ITimeScalable timeScalable = TimeScalableFactory.Create(TimeScale.SiderealTime);
            IDateTimeAdder monthsAdder = DateTimeAdderFactory.Create(timeScalable, TimeUnit.Months);
            IDateTimeAdder yearsAdder = DateTimeAdderFactory.Create(timeScalable, TimeUnit.Years);

            IMutableDateTime next = monthsAdder.Add(start, 3);
            IAstronomicalDateTime astronomical = DateTimeFactory.ConvertToAstronomical(next);
            Console.WriteLine($"Next: {next}");
            Console.WriteLine($"GMST: {astronomical.GetMeanSiderealTime().TotalDegrees}");

            next = monthsAdder.Add(next, 3);
            astronomical = DateTimeFactory.ConvertToAstronomical(next);
            Console.WriteLine($"Next: {next}");
            Console.WriteLine($"GMST: {astronomical.GetMeanSiderealTime().TotalDegrees}");

            next = yearsAdder.Add(start, 3);
            astronomical = DateTimeFactory.ConvertToAstronomical(next);
            Console.WriteLine($"Next: {next}");
            Console.WriteLine($"GMST: {astronomical.GetMeanSiderealTime().TotalDegrees}");

            next = yearsAdder.Add(next, 3);
            astronomical = DateTimeFactory.ConvertToAstronomical(next);
            Console.WriteLine($"Next: {next}");
            Console.WriteLine($"GMST: {astronomical.GetMeanSiderealTime().TotalDegrees}");

            Console.WriteLine("--------------------");

            // ERA - Stellar Time

            timeScalable = TimeScalableFactory.Create(TimeScale.StellarTime);
            monthsAdder = DateTimeAdderFactory.Create(timeScalable, TimeUnit.Months);
            yearsAdder = DateTimeAdderFactory.Create(timeScalable, TimeUnit.Years);

            next = monthsAdder.Add(start, 3);
            astronomical = DateTimeFactory.ConvertToAstronomical(next);
            Console.WriteLine($"Next: {next}");
            Console.WriteLine($"ERA: {astronomical.GetEarthRotationAngle().TotalDegrees}");

            next = monthsAdder.Add(next, 3);
            astronomical = DateTimeFactory.ConvertToAstronomical(next);
            Console.WriteLine($"Next: {next}");
            Console.WriteLine($"ERA: {astronomical.GetEarthRotationAngle().TotalDegrees}");

            next = yearsAdder.Add(start, 3);
            astronomical = DateTimeFactory.ConvertToAstronomical(next);
            Console.WriteLine($"Next: {next}");
            Console.WriteLine($"ERA: {astronomical.GetEarthRotationAngle().TotalDegrees}");

            next = yearsAdder.Add(next, 3);
            astronomical = DateTimeFactory.ConvertToAstronomical(next);
            Console.WriteLine($"Next: {next}");
            Console.WriteLine($"ERA: {astronomical.GetEarthRotationAngle().TotalDegrees}");
        }

        private static void TestDateTimes()
        {
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");
            ITimeScalable timeScalable = TimeScalableFactory.Create(TimeScale.SiderealTime);
            IDateTimeAdder dateTimeAdder = DateTimeAdderFactory.Create(timeScalable, TimeUnit.Months);
            IDateTimesBuilder dateTimesBuilder = DateTimesBuilderFactory.Create(dateTimeAdder, false);

            IBaseDateTime start = DateTimeFactory.CreateBase(
                new DateTime(2022, 11, 24, 12, 0, 0, DateTimeKind.Local), dateTimeInfo);

            IAstronomicalDateTime[] dateTimes = DateTimeFactory.ConvertToAstronomical(dateTimesBuilder.Build(start, -10, 1));

            foreach (IAstronomicalDateTime dateTime in dateTimes)
            {
                Console.WriteLine($"{dateTime.Value}: \t{dateTime.GetMeanSiderealTime().TotalDegrees}°");
            }
        }

        private static void TestTrackVectors()
        {
            // My location (LAT, LON): 29.4944768, -95.1123968
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-95.1123968), latitude: new Angle(29.4944768));
            //longitude: Angle.Zero, latitude: new Angle(90.0));
            //longitude: Angle.Zero, latitude: new Angle(-80.0));

            // M42: Orion Diffuse Nebula
            IEquatorialCoordinate m42 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(new TimeSpan(5, 35, 17)), declination: new Angle(-5, -23, -28));

            // M31: Andromeda Galaxy
            IEquatorialCoordinate m31 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromMinutes(42.7383)), declination: new Angle(41, 16, 9));

            // Polaris
            IEquatorialCoordinate polaris = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(new TimeSpan(2, 31, 49)), declination: new Angle(89, 15, 51));

            // Some point on the celestial equator
            IEquatorialCoordinate eq = CoordinateFactory.CreateEquatorial(
                rightAscension: Angle.Zero, declination: Angle.Zero);

            // Some northern point at RA 0
            IEquatorialCoordinate ra = CoordinateFactory.CreateEquatorial(
                rightAscension: Angle.Zero, declination: new Angle(41, 16, 9));

            // The North Celestial Pole, or Celestial Intermediate Pole (CIP)
            IEquatorialCoordinate north = CoordinateFactory.CreateEquatorial(
                rightAscension: Angle.Zero, declination: new Angle(90.0));

            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("America/Chicago");
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.GMST);
            IObserver observer = ObserverFactory.Create(dateTimeInfo, location, m31, timeKeeper);
            ITrajectory trajectory = TrajectoryCalculator.Calculate(observer);

            Console.WriteLine($"Location: {observer.Origin}");
            Console.WriteLine($"Target: {observer.Destination}");
            Console.WriteLine($"Visibility: {trajectory}\n");

            IAstronomicalDateTime start = DateTimeFactory.ConvertToAstronomical(dateTimeInfo.Now);
            IMutableDateTime mutable;

            //AstronomicalDateTime start =
            //    new(new DateTime(2022, 12, 13, 19, 32, 0, DateTimeKind.Local), AstronomicalDateTime.UnspecifiedKind.IsLocal);

            int cycles = 10;

            if (trajectory is IVariableTrajectory variableTrajectory)
            {
                if (trajectory is IRiseSetTrajectory riseSetTrajectory)
                {
                    IVector[] rise = riseSetTrajectory.GetRise(start, cycles);
                    IVector[] apex = riseSetTrajectory.GetApex(start, cycles);
                    IVector[] set = riseSetTrajectory.GetSet(start, cycles);

                    for (int i = 0; i < cycles; i++)
                    {
                        if (rise[i] != null)
                        {
                            mutable = DateTimeFactory.ConvertToMutable(rise[i].DateTime);
                            Console.WriteLine($"Rise {i}:\t\tDateTime: {mutable.ToLocalTime()}\t\tPosition: {rise[i].Coordinate}");
                        }
                        if (apex[i] != null)
                        {
                            mutable = DateTimeFactory.ConvertToMutable(apex[i].DateTime);
                            Console.WriteLine($"Apex {i}:\t\tDateTime: {mutable.ToLocalTime()}\t\tPosition: {apex[i].Coordinate}");
                        }
                        if (set[i] != null)
                        {
                            mutable = DateTimeFactory.ConvertToMutable(set[i].DateTime);
                            Console.WriteLine($"Set {i}:\t\tDateTime: {mutable.ToLocalTime()}\t\tPosition: {set[i].Coordinate}");
                        }
                        Console.WriteLine("--------------------");
                    }
                }
                else
                {
                    IVector[] apex = variableTrajectory.GetApex(start, cycles);

                    for (int i = 0; i < cycles; i++)
                    {
                        if (apex[i] != null)
                        {
                            mutable = DateTimeFactory.ConvertToMutable(apex[i].DateTime);
                            Console.WriteLine($"Apex {i}:\t\tDateTime: {mutable.ToLocalTime()}\t\tPosition: {apex[i].Coordinate}");
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            TestTrack();
            //ClientTimeZoneInfoTests.RunAmericaNewYorkTest();
            //ClientTimeZoneInfoTests.RunAustraliaSydneyTest();
            //NullIsland_M17_ERA_Trajectory_AntiSetting_August19_2023_1300();
        }
    }
}