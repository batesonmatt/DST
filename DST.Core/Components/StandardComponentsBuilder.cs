using DST.Core.Physics;

namespace DST.Core.Components
{
    // Provides the means to build the components of a standard spherical coordinate.
    public class StandardComponentsBuilder : BaseComponentsBuilder
    {
        // Builds a spherical coordinate's angle components given the specified angles of rotation and inclination.
        // The resulting coordinate will have a rotation fixed on [0°, 360°) and an inclination fixed on [-90°, 90°].
        public override IComponents Build(Angle rotation, Angle inclination)
        {
            // Get the fixed components.
            (Angle, Angle) components = GetFixedComponents(rotation, inclination);

            // Return the components as a new IComponents object.
            return ComponentsFactory.Create(components.Item1, components.Item2);
        }
    }
}
