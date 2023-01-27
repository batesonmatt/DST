using DST.Core.Observer;

namespace DST.Core.Trajectory
{
    // Provides the means to calculate the celestial trajectory of an object relative to an observer's position.
    public class TrajectoryCalculator
    {
        // Calculates the observable trajectory, or path, of the targeting object with respect to the IObserver's Origin.
        public static ITrajectory Calculate(IObserver observer)
        {
            _ = observer ?? throw new ArgumentNullException(nameof(observer));

            return observer switch
            {
                ILocalObserver localObserver => Calculate(localObserver),
                _ => throw new NotSupportedException($"{nameof(IObserver)} type '{observer.GetType()}' is not supported.")
            };
        }

        // Calculates the observable trajectory, or path, of the targeting object with respect to the ILocalObserver's Latitude.
        // Assumes the observer's height, or distance from the surface, is negligible.
        private static ITrajectory Calculate(ILocalObserver localObserver)
        {
            _ = localObserver ?? throw new ArgumentNullException(nameof(localObserver));

            // Get the latitude and declination components, in degrees.
            double phi = localObserver.Location.Latitude;
            double delta = localObserver.Target.Declination;

            // Get the DiurnalArc value based on the values of phi and delta.
            DiurnalArc diurnalArc = GetDiurnalArc(phi, delta);

            // Create a new ITrajectory.
            return TrajectoryFactory.Create(localObserver, diurnalArc);
        }

        // Returns the DiurnalArc value given an observer's latitudinal angle (phi) and the target's declination (delta).
        private static DiurnalArc GetDiurnalArc(double phi, double delta)
        {
            // A particular deep-space object may only be visible or periodically visible for observers in 
            // certain parts of the world at different points in time.
            // Thus, the relationship between the observer's latitude and the targeting object's declination has 
            // various ranges that determine the object's trajectory.

            if (IsRiseSet(phi, delta)) return DiurnalArc.RiseSet;
            if (IsNeverRise(phi, delta)) return DiurnalArc.NeverRise;
            if (IsCircumpolar(phi, delta)) return DiurnalArc.Circumpolar;
            if (IsCircumpolarOffset(phi, delta)) return DiurnalArc.CircumpolarOffset;
            if (IsHorizontal(phi, delta)) return DiurnalArc.NeverRise;
            return DiurnalArc.NeverRise;
        }

        // Returns a value that indicates whether the given latitude (phi) and declination (delta)
        // form a rise and set trajectory.
        private static bool IsRiseSet(double phi, double delta)
        {
            // If LAT - 90° < DEC < 90° - LAT, the target's declination is within 90° North and South of 
            // the observer's latitude.
            return -90.0 + Math.Abs(phi) < delta && delta < 90.0 - Math.Abs(phi);
        }

        // Returns a value that indicates whether the given latitude (phi) and declination (delta)
        // form a never rise trajectory.
        private static bool IsNeverRise(double phi, double delta)
        {
            // If DEC - LAT < -90° (Northern Hemisphere), or DEC - LAT > 90° (Southern Hemisphere),
            // the target is always more than 90° from the observer's zenith, and below their horizon.
            return Math.Abs(delta - phi) > 90.0;
        }

        // Returns a value that indicates whether the given latitude (phi) and declination (delta)
        // form a circumpolar trajectory.
        private static bool IsCircumpolar(double phi, double delta)
        {
            // If LAT + DEC > 90° (Northern Hemisphere), or LAT + DEC < -90° (Southern Hemisphere),
            // the target is always within 90° of the observer's zenith, and above their horizon.
            //
            // The circumpolar trajectory will not have an offset if the observer is located at 
            // either of the geographic poles.
            return Math.Abs(phi + delta) > 90.0 && Math.Abs(phi) == 90.0;
        }

        // Returns a value that indicates whether the given latitude (phi) and declination (delta)
        // form an offset circumpolar trajectory.
        private static bool IsCircumpolarOffset(double phi, double delta)
        {
            // If LAT + DEC > 90° (Northern Hemisphere), or LAT + DEC < -90° (Southern Hemisphere),
            // the target is always within 90° of the observer's zenith, and above their horizon.
            //
            // The circumpolar trajectory will have an offset if the observer is not located at 
            // either of the geographic poles.
            return Math.Abs(phi + delta) > 90.0 && Math.Abs(phi) != 90.0;
        }

        // Returns a value that indicates whether the given latitude (phi) and declination (delta)
        // form a horizontal trajectory.
        private static bool IsHorizontal(double phi, double delta)
        {
            // If the target's declination is exactly 90° from the observer's zenith, it is
            // at their horizon, and thus will never rise.
            return Math.Abs(phi + delta) == 90.0;
        }
    }
}
