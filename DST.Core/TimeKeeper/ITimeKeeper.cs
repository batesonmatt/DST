using DST.Core.Physics;

namespace DST.Core.TimeKeeper
{
    public interface ITimeKeeper
    {
        Angle Calculate(AstronomicalDateTime dateTime);
    }
}
