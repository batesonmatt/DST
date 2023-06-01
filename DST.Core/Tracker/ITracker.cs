using DST.Core.Coordinate;
using DST.Core.DateAndTime;

namespace DST.Core.Tracker
{
    public interface ITracker
    {
        ICoordinate Track();
        ICoordinate Track(AstronomicalDateTime dateTime);
        ICoordinate[] Track(AstronomicalDateTime[] dateTimes);
    }
}
