using DST.Core.Observer;
using DST.Core.Physics;

namespace DST.Core.LocalTimeKeeper
{
    public interface ILocalTimeKeeper
    {
        Angle Calculate(ILocalObserver localObserver, AstronomicalDateTime dateTime);
    }
}
