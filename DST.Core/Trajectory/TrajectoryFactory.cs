using DST.Core.Observer;

namespace DST.Core.Trajectory
{
    public class TrajectoryFactory
    {
        // Creates a new ITrajectory object given the specified IObserver object and DiurnalArc value.
        public static ITrajectory Create(IObserver observer, DiurnalArc diurnalArc)
        {
            _ = observer ?? throw new ArgumentNullException(nameof(observer));

            return observer switch
            {
                ILocalObserver localObserver => Create(localObserver, diurnalArc),
                _ => throw new NotSupportedException($"{nameof(IObserver)} type '{observer.GetType()}' is not supported.")
            };
        }

        // Creates a new ITrajectory object given the specified ILocalObserver object and DiurnalArc value.
        private static ITrajectory Create(ILocalObserver localObserver, DiurnalArc diurnalArc)
        {
            _ = localObserver ?? throw new ArgumentNullException(nameof(localObserver));

            return diurnalArc switch
            {
                DiurnalArc.NeverRise => new NeverRiseTrajectory(localObserver),
                DiurnalArc.RiseSet => new RiseSetTrajectory(localObserver),
                DiurnalArc.Circumpolar => new CircumpolarTrajectory(localObserver),
                DiurnalArc.CircumpolarOffset => new CircumpolarOffsetTrajectory(localObserver),
                _ => throw new NotSupportedException($"{nameof(DiurnalArc)} value '{diurnalArc}' is not supported.")
            };
        }
    }
}
