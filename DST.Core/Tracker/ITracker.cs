using DST.Core.Coordinate;
using DST.Core.DateAndTime;

namespace DST.Core.Tracker
{
    public interface ITracker
    {
        ICoordinate Track();
        ICoordinate Track(IAstronomicalDateTime dateTime);
        ICoordinate[] Track(IAstronomicalDateTime[] dateTimes);
    }
}
