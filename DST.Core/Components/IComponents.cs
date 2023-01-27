using DST.Core.Physics;

namespace DST.Core.Components
{
    public interface IComponents
    {
        Angle Rotation { get; }
        Angle Inclination { get; }
    }
}
