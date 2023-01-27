using DST.Core.Physics;

namespace DST.Core.Components
{
    // Represents the components of a spherical coordinate.
    public class SphericalComponents : IComponents
    {
        protected readonly Angle _rotation;
        protected readonly Angle _inclination;

        // Gets the rotation component for this SphericalComponents instance.
        public Angle Rotation => _rotation;

        // Gets the inclination component for this SphericalComponents instance.
        public Angle Inclination => _inclination;

        // Creates a new SphericalComponents instance with the specified rotation and inclination Angle values.
        public SphericalComponents(Angle rotation, Angle inclination)
        {
            _rotation = rotation;
            _inclination = inclination;
        }
    }
}
