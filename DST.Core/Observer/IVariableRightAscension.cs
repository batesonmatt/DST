using DST.Core.DateAndTime;
using DST.Core.Physics;

namespace DST.Core.Observer
{
    public interface IVariableRightAscension
    {
        Angle GetRightAscension(IAstronomicalDateTime dateTime);
    }
}
