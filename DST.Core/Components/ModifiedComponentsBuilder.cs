using DST.Core.Physics;

namespace DST.Core.Components
{
    // Provides the means to build the components of a modified spherical coordinate.
    public class ModifiedComponentsBuilder : BaseComponentsBuilder
    {
        // Builds a spherical coordinate's angle components given the specified angles of rotation and inclination.
        // The resulting coordinate will have a rotation fixed on (-180°, 180°] and an inclination fixed on [-90°, 90°].
        public override IComponents Build(Angle rotation, Angle inclination)
        {
            // Get the fixed components.
            (Angle, Angle) components = GetFixedComponents(rotation, inclination);

            // Get the fixed angular rotation.
            Angle fixedRotation = components.Item1;

            // If the angular rotation lies on (180°, 360°), relate it onto (-180°, 0°).
            if (Math.Sin(fixedRotation.TotalRadians) < 0.0)
            {
                fixedRotation = new Angle(fixedRotation - 360.0);
            }

            // Return the new IComponents object.
            return ComponentsFactory.Create(fixedRotation, components.Item2);
        }
    }
}
