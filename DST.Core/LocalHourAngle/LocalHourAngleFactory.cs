using DST.Core.LocalTimeKeeper;

namespace DST.Core.LocalHourAngle
{
    public class LocalHourAngleFactory
    {
        // Creates a new ILocalHourAngle object given the specified ILocalTimeKeeper argument.
        public static ILocalHourAngle Create(ILocalTimeKeeper localTimeKeeper)
        {
            _ = localTimeKeeper ?? throw new ArgumentNullException(nameof(localTimeKeeper));

            return localTimeKeeper switch
            {
                LocalSiderealTimeKeeper => new LocalSiderealHourAngle(localTimeKeeper),
                LocalMeanSiderealTimeKeeper => new LocalMeanSiderealHourAngle(localTimeKeeper),
                LocalStellarTimeKeeper => new LocalStellarHourAngle(localTimeKeeper),
                _ => throw new NotSupportedException($"{nameof(ILocalTimeKeeper)} type '{localTimeKeeper.GetType()}' is not supported.")
            };
        }
    }
}
