using DST.Core.Coordinate;
using DST.Core.Observer;
using DST.Core.Physics;

namespace DST.Core.Tracker
{
    public class LocalTracker : ITracker
    {
        protected readonly ILocalObserver _localObserver;

        public LocalTracker(ILocalObserver localObserver)
        {
            _localObserver = localObserver ?? throw new ArgumentNullException(nameof(localObserver));
        }

        // Returns a new HorizontalCoordinate resembling the position of the targeting object relative to the observer's location 
        // at the current date and time.
        public ICoordinate Track()
        {
            return Track(_localObserver.DateTimeInfo.Now);
        }

        // Tracks the targeting object over a period of time at every specified interval given by 'dateTimes'.
        // Returns an array of HorizontalCoordinate positions in the order they were tracked for each element in 'dateTimes'.
        public ICoordinate[] Track(AstronomicalDateTime[] dateTimes)
        {
            if (dateTimes is null)
            {
                return Array.Empty<IHorizontalCoordinate>();
            }

            ICoordinate[] positions = new ICoordinate[dateTimes.Length];

            for (int i = 0; i < dateTimes.Length; i++)
            {
                positions[i] = Track(dateTimes[i]);
            }

            return positions;
        }

        // Returns a new HorizontalCoordinate resembling the position of the targeting object relative to the observer's location 
        // at the specified AstronomicalDateTime value.
        // Assumes the observer's height, or distance from the surface of the Earth, is negligible.
        public ICoordinate Track(AstronomicalDateTime dateTime)
        {
            // Calculate the angles of altitude and azimuth using the spherical triangle.
            //
            // The spherical triangle contains the following points and their angles:
            // 1. Zenith - The azimuth's explementary angle (360° - azimuth)
            // 2. Celestial Body - The parallactic angle(angle between the observer's zenith and the North Celestial Pole)
            // 3. North Celestial Pole, or Celestial Intermediate Pole(CIP) - The local hour angle
            //
            // And the following opposite sides(great circle arc lengths):
            // 1. The North polar distance(90° -declination)
            // 2. The observer's co-latitude (90° - latitude)
            // 3. The zenith distance(90° -altitude)

            // Calculate the local hour angle (LHA) of the IObserver.Target relative to the IObserver.Location at the specified date/time.
            Angle lha = _localObserver.LocalHourAngle.Calculate(_localObserver, dateTime);

            // If the IObserver.Target has a nutatable declination, use it instead of the original.
            Angle declination = _localObserver is IVariableDeclination variableObserver ?
                variableObserver.GetDeclination(dateTime) : _localObserver.Target.Declination;

            // Get the observer's geographic latitude (phi) the target's declination (delta), and the local hour angle (omega) in radians.
            double phi = _localObserver.Location.Latitude.TotalRadians;
            double delta = declination.TotalRadians;
            double omega = lha.TotalRadians;

            // To find the altitude, use the spherical law of cosines:
            // cos(90° - altitude) = cos(90° - declination) * cos(90° - latitude) + sin(90° - declination) * sin(90° - latitude) * cos(hour angle)
            //
            // Apply the complementary angle theorem and rearrange to solve for altitude:
            // altitude = arcsin(sin(declination) * sin(latitude) + cos(declination) * cos(latitude) * cos(hour angle))
            //
            // This angle is measured geocentrically from the celestial horizon at the center of the Earth.
            // The Earth's radius may be negligible since any object observed on the celestial sphere is at an extreme distance.
            //
            // This returns the true (geocentric) altitude in radians:
            double altitudeRadians = Math.Asin(
                Math.Sin(delta) * Math.Sin(phi) +
                Math.Cos(delta) * Math.Cos(phi) *
                Math.Cos(omega));

            // Evaluate the altitude in degrees.
            // [Important] - this will round Math.PI / 2 to 90°.
            // This is required, as Math.PI does not have infinite precision, thus Math.Cos(Math.PI / 2) does not equal 0.
            // Therefore, attempting to calculate the azimuth with an altitude of Math.PI / 2 will produce
            // unexpected results.
            Angle altitude = Angle.FromRadians(altitudeRadians);

            Angle azimuth;

            // By default, the initial azimuthal angle (0°) references the true North Pole.
            // However, it may also reference the Prime Meridian (IRM), as seen later.
            bool referencesIRM = false;

            // If the altitude is at or extremely close to the observer's zenith (+90°) or nadir (-90°), 
            // then the azimuth is negligible.
            if (Math.Abs(altitude) == 90.0)
            {
                azimuth = Angle.Zero;
            }
            // If the observer's latitude is at either of the geographic poles, the azimuth may still
            // be represented as the rotational angular distance East or West from the Prime Meridian.
            else if (_localObserver.Location.IsAxial())
            {
                // If the object's declination is positive (North celestial hemisphere), it is revolving counter-clockwise
                // relative to the observer's zenith (from the North geographic pole) or nadir (from the South geographic pole).
                // Thus, the observer's azimuth would extend Westward from the Prime Meridian over time.
                if (declination >= 0.0)
                {
                    // Start with the LHA.
                    // If the LST was adjusted by some longitude other than 0°, this would be equivalent to LHA - longitude at the North Pole.
                    // The azimuth is now set to reference the 180th Meridian at the North Pole.
                    // Finish by rotating 180° to reference the Prime Meridian.
                    azimuth = lha.Flipped().Coterminal();
                }
                // If the object's declination is negative (South celestial hemisphere), it is revolving clockwise
                // relative to the observer's zenith (from the South geographic pole) or nadir (from the North geographic pole).
                // Thus, the observer's azimuth would extend Eastward from the Prime Meridian over time.
                else
                {
                    // Start with the LHA.
                    // If the LST was adjusted by some longitude other than 0°, this would be equivalent to LHA + longitude at the South Pole.
                    // Since the observer would normally be referencing the North Pole, even in the Southern Hemisphere,
                    // the azimuth is already set to reference the Prime Meridian at the South Pole.
                    azimuth = lha;
                }

                // The azimuth is now an angle that measures some distance from the Prime Meridian.
                // So, set the reference point for the initial azimuthal angle (0°) as the Prime Meridian (IRM).
                referencesIRM = true;
            }
            else
            {
                // To find the azimuth, use the spherical law of cosines:
                // cos(90° - declination) = cos(90° - latitude) * cos(90° - altitude) + sin(90° - latitude) * sin(90° - altitude) * cos(360° - azimuth)
                //
                // Apply the complementary angle theorem and rearrange to solve for azimuth:
                // azimuth = arccos(sin(declination) - [sin(latitude) * sin(altitude)] / [cos(latitude) * cos(altitude)])
                //
                // This returns the azimuth in radians:
                double azimuthRadians = Math.Acos(
                    (Math.Sin(delta) -
                    Math.Sin(altitudeRadians) * Math.Sin(phi)) /
                    (Math.Cos(altitudeRadians) * Math.Cos(phi)));

                // Evaluate the azimuth in degrees.
                azimuth = Angle.FromRadians(azimuthRadians);

                // Fix the azimuth, if necessary.
                // Note that the result of Math.Acos(double) ranges from 0 to pi, or 0° to 180°. 
                // Azimuth runs clockwise (Eastward) from 0° to 360°.
                // This essentially mirrors the azimuth angle from quadrants I and II onto quadrants IV and III, respectively.
                if (Math.Sin(omega) > 0.0)
                {
                    if (azimuth > 0.0)
                    {
                        azimuth = new Angle(360.0 - azimuth);
                    }
                }
            }

            // Create the final horizontal coordinate using the calculated components.
            IHorizontalCoordinate coordinate = HorizontalCoordinateFactory.Create(azimuth, altitude, referencesIRM);

            return coordinate;
        }
    }
}
