using DST.Core.LocalHourAngle;

namespace DST.Core.LocalHourAngleDateTime
{
    public class LocalHourAngleDateTimeFactory
    {
        // Creates a new ILocalHourAngleDateTime object given the specified ILocalHourAngle argument.
        public static ILocalHourAngleDateTime Create(ILocalHourAngle localHourAngle)
        {
            _ = localHourAngle ?? throw new ArgumentNullException(nameof(localHourAngle));

            return localHourAngle switch
            {
                LocalSiderealHourAngle => new LocalSiderealHourAngleDateTime(localHourAngle),
                LocalMeanSiderealHourAngle => new LocalMeanSiderealHourAngleDateTime(localHourAngle),
                LocalStellarHourAngle => new LocalStellarHourAngleDateTime(localHourAngle),
                _ => throw new NotSupportedException($"{nameof(ILocalHourAngle)} type '{localHourAngle.GetType()}' is not supported.")
            };
        }
    }
}
