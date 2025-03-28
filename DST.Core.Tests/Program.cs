﻿using DST.Core.Coordinate;
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
        private static void UnitTest_TrackVector_Harness()
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

            //bool isRiseSet = trajectory is IRiseSetTrajectory;
            //bool isNotAboveHorizon = !riseSet.IsAboveHorizon(dateTime);
            //bool isNotRising = !riseSet.IsRising(dateTime);
            //bool isNotSetting = !riseSet.IsSetting(dateTime);

            // Assert
            //Console.WriteLine("Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));");
            //Console.WriteLine("Assert.IsInstanceOfType(rise, typeof(LocalVector));");
            //Console.WriteLine("Assert.IsInstanceOfType(apex, typeof(LocalVector));");
            //Console.WriteLine("Assert.IsInstanceOfType(set, typeof(LocalVector));");
            //Console.WriteLine("Assert.IsFalse(riseSet.IsAboveHorizon(dateTime));");
            //Console.WriteLine("Assert.IsFalse(riseSet.IsRising(dateTime));");
            //Console.WriteLine("Assert.IsFalse(riseSet.IsSetting(dateTime));");
            Console.WriteLine($"Assert.AreEqual({riseCoordinate.Components.Inclination.TotalDegrees}, riseCoordinate.Components.Inclination);");
            Console.WriteLine($"Assert.AreEqual({riseCoordinate.Components.Rotation.TotalDegrees}, riseCoordinate.Components.Rotation);");
            Console.WriteLine($"Assert.AreEqual({apexCoordinate.Components.Inclination.TotalDegrees}, apexCoordinate.Components.Inclination);");
            Console.WriteLine($"Assert.AreEqual({apexCoordinate.Components.Rotation.TotalDegrees}, apexCoordinate.Components.Rotation);");
            Console.WriteLine($"Assert.AreEqual({setCoordinate.Components.Inclination.TotalDegrees}, setCoordinate.Components.Inclination);");
            Console.WriteLine($"Assert.AreEqual({setCoordinate.Components.Rotation.TotalDegrees}, setCoordinate.Components.Rotation);");
            Console.WriteLine($"Assert.AreEqual({rise.DateTime.Ticks}, rise.DateTime.Ticks);");
            Console.WriteLine($"Assert.AreEqual({apex.DateTime.Ticks}, apex.DateTime.Ticks);");
            Console.WriteLine($"Assert.AreEqual({set.DateTime.Ticks}, set.DateTime.Ticks);");
            Console.WriteLine($"Assert.AreEqual({localRiseDateTime.Ticks}, localRiseDateTime.Ticks);");
            Console.WriteLine($"Assert.AreEqual({localApexDateTime.Ticks}, localApexDateTime.Ticks);");
            Console.WriteLine($"Assert.AreEqual({localSetDateTime.Ticks}, localSetDateTime.Ticks);");
        }

        private static void UnitTest_TrackerDateRange_Harness()
        {
            // Arrange

            // LAT: 40.783333°, LON: -73.966667° (Manhattan, New York)
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-73.966667), latitude: new Angle(40.783333));

            // RA: 5.5881389hr, DEC: -5.3911111° (M42 - Orion Nebula)
            IEquatorialCoordinate m42 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(5.5881389)), declination: new Angle(-5.3911111));

            // Eastern Time - US & Canada (IANA: Eastern Standard Time)
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Eastern Standard Time");

            // Greenwich Mean Sidereal Time (GMST)
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.GMST);
            IObserver observer = ObserverFactory.Create(dateTimeInfo, location, m42, timeKeeper);

            // January 2, 0001, 12:00 AM (UTC) - January 1, 0001, 7:00 PM (Local)
            IAstronomicalDateTime minDateTime = DateTimeFactory.ConvertToAstronomical(dateTimeInfo.MinAstronomicalDateTime);

            // December 30, 9999, 11:59:59 PM (UTC) - December 30, 9999, 6:59:59 PM (Local)
            IAstronomicalDateTime maxDateTime = DateTimeFactory.ConvertToAstronomical(dateTimeInfo.MaxAstronomicalDateTime);

            bool t1 = maxDateTime.Value.Ticks == dateTimeInfo.MaxAstronomicalDateTime.Ticks;
            bool t2 = maxDateTime.Value.Ticks == DateTimeConstants.MaxUtcDateTime.Ticks;
            bool t3 = DateTimeConstants.MaxUtcDateTime.Ticks == dateTimeInfo.MaxAstronomicalDateTime.Ticks;

            ITracker tracker = TrackerFactory.Create(observer);

            // Act
            ITrajectory trajectory = TrajectoryCalculator.Calculate(observer);
            IRiseSetTrajectory riseSet = (IRiseSetTrajectory)trajectory;
            ICoordinate minPosition = tracker.Track(minDateTime);
            ICoordinate maxPosition = tracker.Track(minDateTime);
            IVector minApex = riseSet.GetApex(minDateTime);
            IVector maxApex = riseSet.GetApex(maxDateTime);
            IVector[] minApexFuture = riseSet.GetApex(minDateTime, 1);
            IVector[] maxApexPast = riseSet.GetApex(maxDateTime, -1);
            
            // Assert
            Console.WriteLine("Assert.IsInstanceOfType(trajectory, typeof(RiseSetTrajectory));");

            Console.WriteLine("Assert.IsInstanceOfType(minPosition, typeof(NonTrackableCoordinate));");
            Console.WriteLine("Assert.IsInstanceOfType(maxPosition, typeof(NonTrackableCoordinate));");

            Console.WriteLine("Assert.AreEqual(0.0, minPosition.Components.Rotation);");
            Console.WriteLine("Assert.AreEqual(0.0, minPosition.Components.Inclination);");
            Console.WriteLine("Assert.AreEqual(0.0, maxPosition.Components.Rotation);");
            Console.WriteLine("Assert.AreEqual(0.0, maxPosition.Components.Inclination);");

            Console.WriteLine("Assert.IsInstanceOfType(minApex, typeof(NonTrackableVector));");
            Console.WriteLine("Assert.IsInstanceOfType(maxApex, typeof(NonTrackableVector));");

            Console.WriteLine("Assert.AreEqual(DateTimeConstants.MinUtcDateTime.Ticks, minApex.DateTime.Ticks);");
            Console.WriteLine("Assert.AreEqual(DateTimeConstants.MaxUtcDateTime.Ticks, maxApex.DateTime.Ticks);");

            Console.WriteLine("Assert.AreEqual(0.0, minApex.Coordinate.Components.Rotation);");
            Console.WriteLine("Assert.AreEqual(0.0, minApex.Coordinate.Components.Inclination);");
            Console.WriteLine("Assert.AreEqual(0.0, maxApex.Coordinate.Components.Rotation);");
            Console.WriteLine("Assert.AreEqual(0.0, maxApex.Coordinate.Components.Inclination);");

            Console.WriteLine("Assert.IsTrue(minApexFuture.Length == 1);");
            Console.WriteLine("Assert.IsTrue(maxApexPast.Length == 1);");

            Console.WriteLine("Assert.IsInstanceOfType(minApexFuture[0], typeof(LocalVector));");
            Console.WriteLine("Assert.IsInstanceOfType(maxApexPast[0], typeof(LocalVector));");

            Console.WriteLine("Assert.IsInstanceOfType(minApexFuture[0].Coordinate, typeof(HorizontalCoordinate));");
            Console.WriteLine("Assert.IsInstanceOfType(maxApexPast[0].Coordinate, typeof(HorizontalCoordinate));");

            Console.WriteLine($"Assert.AreEqual({minApexFuture[0].DateTime.Ticks}, minApexFuture[0].DateTime.Ticks);");
            Console.WriteLine($"Assert.AreEqual({maxApexPast[0].DateTime.Ticks}, maxApexPast[0].DateTime.Ticks);");

            Console.WriteLine($"Assert.AreEqual({minApexFuture[0].Coordinate.Components.Rotation.TotalDegrees}, minApexFuture[0].Coordinate.Components.Rotation);");
            Console.WriteLine($"Assert.AreEqual({minApexFuture[0].Coordinate.Components.Inclination.TotalDegrees}, minApexFuture[0].Coordinate.Components.Inclination);");
            Console.WriteLine($"Assert.AreEqual({maxApexPast[0].Coordinate.Components.Rotation.TotalDegrees}, maxApexPast[0].Coordinate.Components.Rotation);");
            Console.WriteLine($"Assert.AreEqual({maxApexPast[0].Coordinate.Components.Inclination.TotalDegrees}, maxApexPast[0].Coordinate.Components.Inclination);");
        }

        private static void TestTrack()
        {
            // My location (LAT, LON): 29.4944768, -95.1123968
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-95.1123968), latitude: new Angle(29.4944768));
            //longitude: Angle.Zero, latitude: new Angle(90.0));
            //longitude: Angle.Zero, latitude: new Angle(-80.0));

            // LAT: -30.0, LON: 150.0 (Couradda, New South Wales, Australia)
            IGeographicCoordinate location2 = CoordinateFactory.CreateGeographic(
                longitude: new Angle(150.0), latitude: new Angle(-30.0));

            // M17
            IEquatorialCoordinate m17 = CoordinateFactory.CreateEquatorial(
                rightAscension: new Angle(TimeSpan.FromHours(18.3405556)), declination: new Angle(-16.1766667));

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
            // RightAscension = 0.8072222, Declination = 85.255
            IEquatorialCoordinate test = CoordinateFactory.CreateEquatorial(
                rightAscension: new(TimeSpan.FromHours(0.8072222)), declination: new(85.255));

            // My timezone: America/Chicago
            IDateTimeInfo dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId("Australia/Sydney");
            ITimeKeeper timeKeeper = TimeKeeperFactory.Create(Algorithm.GMST);
            IObserver observer = ObserverFactory.Create(dateTimeInfo, location2, m17, timeKeeper);
            ITrajectory trajectory = TrajectoryCalculator.Calculate(observer);

            Console.WriteLine($"Location: {observer.Origin}");
            Console.WriteLine($"Target: {observer.Destination}");
            Console.WriteLine($"Visibility: {trajectory}\n");

            // For spring DST testing.
            //AstronomicalDateTime start = 
            //    new AstronomicalDateTime(new DateTime(2022, 3, 12, 18, 0, 0, DateTimeKind.Local), AstronomicalDateTime.UnspecifiedKind.IsLocal);

            //IAstronomicalDateTime now = DateTimeFactory.ConvertToAstronomical(dateTimeInfo.Now);
            //IAstronomicalDateTime start = DateTimeFactory.ConvertToAstronomical(dateTimeInfo.Now);

            // August 19, 2023, 1:00 AM
            DateTime localDateTime = new(2023, 8, 19, 1, 0, 0, DateTimeConstants.StandardKind);

            //AstronomicalDateTime start = new(new DateTime(2023, 10, 1, 22, 0, 0), dateTimeInfo);
            IAstronomicalDateTime start = DateTimeFactory.CreateAstronomical(localDateTime, dateTimeInfo);

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

                    Console.WriteLine($"Rise: {rise.DateTime.ToLocalTime()}\tPosition: {rise.Coordinate}");
                    Console.WriteLine($"Apex: {apex.DateTime.ToLocalTime()}\tPosition: {apex.Coordinate}");
                    Console.WriteLine($"Set: {set.DateTime.ToLocalTime()}\tPosition: {set.Coordinate}");
                }
                else
                {
                    IVector apex = variableTrajectory.GetApex(start);
                    Console.WriteLine($"Apex: {apex.DateTime.ToLocalTime()}\tPosition: {apex.Coordinate}");
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

            ITimeScalable timeScalable = TimeScalableFactory.Create(TimeScale.SiderealTime);
            IDateTimeAdder dateTimeAdder = DateTimeAdderFactory.Create(timeScalable, TimeUnit.Years);
            IDateTimesBuilder dateTimesBuilder = DateTimesBuilderFactory.Create(dateTimeAdder, true);
            IAstronomicalDateTime[] dateTimes = DateTimeFactory.ConvertToAstronomical(
                dateTimesBuilder.Build(start, 10, 1));

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
            ITimeScalable timeScalable = TimeScalableFactory.Create(TimeScale.StellarTime);
            IDateTimeAdder dateTimeAdder = DateTimeAdderFactory.Create(timeScalable, TimeUnit.Years);
            IDateTimesBuilder dateTimesBuilder = DateTimesBuilderFactory.Create(dateTimeAdder, true);

            IBaseDateTime start = DateTimeFactory.CreateBase(
                new DateTime(2022, 11, 24, 12, 0, 0, DateTimeKind.Local), dateTimeInfo);

            IAstronomicalDateTime[] dateTimes = DateTimeFactory.ConvertToAstronomical(dateTimesBuilder.Build(start, 100, 1));

            foreach (IAstronomicalDateTime dateTime in dateTimes)
            {
                Console.WriteLine($"{dateTime.Value}: \t{dateTime.GetEarthRotationAngle().TotalDegrees}°");
            }
        }

        private static void TestTrackVectors()
        {
            // My location (LAT, LON): 29.4944768, -95.1123968
            IGeographicCoordinate location = CoordinateFactory.CreateGeographic(
                longitude: new Angle(-95.1123968), latitude: new Angle(29.4944768));
            //longitude: Angle.Zero, latitude: new Angle(90.0));
            //longitude: Angle.Zero, latitude: new Angle(-80.0));

            // C50: Satallite Cluster
            IEquatorialCoordinate c50 = CoordinateFactory.CreateEquatorial(
                rightAscension : new Angle(TimeSpan.FromHours(6.5316667)), declination: new Angle(4.9333333));

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

            //AstronomicalDateTime start =
            //    new(new DateTime(2022, 12, 13, 19, 32, 0, DateTimeKind.Local), AstronomicalDateTime.UnspecifiedKind.IsLocal);

            int cycles = 24;

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
                            Console.WriteLine($"Rise {i}:\t\tDateTime: {rise[i].DateTime.ToLocalTime()}\t\tPosition: {rise[i].Coordinate}");
                        }
                        if (apex[i] != null)
                        {
                            Console.WriteLine($"Apex {i}:\t\tDateTime: {apex[i].DateTime.ToLocalTime()}\t\tPosition: {apex[i].Coordinate}");
                        }
                        if (set[i] != null)
                        {
                            Console.WriteLine($"Set {i}:\t\tDateTime: {set[i].DateTime.ToLocalTime()}\t\tPosition: {set[i].Coordinate}");
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
                            Console.WriteLine($"Apex {i}:\t\tDateTime: {apex[i].DateTime.ToLocalTime()}\t\tPosition: {apex[i].Coordinate}");
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            //TestTrack();
            //TestTrackVectors();
            TestDateTimes();
            //ClientTimeZoneInfoTests.RunAmericaNewYorkTest();
            //ClientTimeZoneInfoTests.RunAustraliaSydneyTest();
            //UnitTest_TrackVector_Harness();
            //UnitTest_TrackerDateRange_Harness();

            Console.ReadLine();
        }
    }
}