using DST.Core.Observer;

namespace DST.Core.Tracker
{
    public class TrackerFactory
    {
        // Returns a new ITracker object given a specified IObserver argument.
        public static ITracker Create(IObserver observer)
        {
            _ = observer ?? throw new ArgumentNullException(nameof(observer));

            return observer switch
            {
                ILocalObserver localObserver => new LocalTracker(localObserver),
                _ => throw new NotSupportedException($"{nameof(IObserver)} type '{observer.GetType()}' is not supported.")
            };
        }
    }
}
