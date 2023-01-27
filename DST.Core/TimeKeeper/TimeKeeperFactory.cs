namespace DST.Core.TimeKeeper
{
    public class TimeKeeperFactory
    {
        // Returns a new ITimeKeeper object given a specified Algorithm value.
        public static ITimeKeeper Create(Algorithm algorithm)
        {
            return algorithm switch
            {
                Algorithm.GMST => new MeanSiderealTimeKeeper(),
                Algorithm.GAST => new SiderealTimeKeeper(),
                Algorithm.ERA => new StellarTimeKeeper(),
                _ => throw new NotSupportedException($"{nameof(Algorithm)} value '{algorithm}' is not supported.")
            };
        }
    }
}
