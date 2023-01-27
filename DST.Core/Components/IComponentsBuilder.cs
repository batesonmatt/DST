using DST.Core.Physics;

namespace DST.Core.Components
{
    public interface IComponentsBuilder
    {
        IComponents Build(Angle rotation, Angle inclination);
    }
}
