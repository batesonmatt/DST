using DST.Core.TimeKeeper;

namespace DST.Core.LocalTimeKeeper
{
    public class LocalTimeKeeperFactory
    {
        // Creates a new ILocalTimeKeeper object given the specified ITimeKeeper argument.
        public static ILocalTimeKeeper Create(ITimeKeeper timeKeeper)
        {
            _ = timeKeeper ?? throw new ArgumentNullException(nameof(timeKeeper));

            return timeKeeper switch
            {
                SiderealTimeKeeper => new LocalSiderealTimeKeeper(timeKeeper),
                MeanSiderealTimeKeeper => new LocalMeanSiderealTimeKeeper(timeKeeper),
                StellarTimeKeeper => new LocalStellarTimeKeeper(timeKeeper),
                _ => throw new NotSupportedException($"{nameof(ITimeKeeper)} type '{timeKeeper.GetType()}' is not supported.")
            };
        }
    }
}
