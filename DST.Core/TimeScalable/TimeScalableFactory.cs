namespace DST.Core.TimeScalable
{
    public class TimeScalableFactory
    {
        // Creates a new ITimeScalable object given the specified TimeScale value.
        public static ITimeScalable Create(TimeScale timeScale)
        {
            return timeScale switch
            {
                TimeScale.MeanSolarTime => new MeanSolarTimeScalable(),
                TimeScale.SiderealTime => new SiderealTimeScalable(),
                TimeScale.StellarTime => new StellarTimeScalable(),
                _ => throw new NotSupportedException($"{nameof(TimeScale)} value '{timeScale}' is not supported.")
            };
        }
    }
}