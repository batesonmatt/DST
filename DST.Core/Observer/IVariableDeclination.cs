using DST.Core.Physics;

namespace DST.Core.Observer
{
    public interface IVariableDeclination
    {
        Angle GetDeclination(AstronomicalDateTime dateTime);
    }
}
