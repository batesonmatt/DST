using DST.Core.Physics;

namespace DST.Core.Components
{
    // Provides the means to build the components of a spherical coordinate.
    public abstract class BaseComponentsBuilder : IComponentsBuilder
    {
        // Builds a spherical coordinate's angle components given the specified angles of rotation and inclination.
        public abstract IComponents Build(Angle rotation, Angle inclination);

        protected virtual (Angle, Angle) GetFixedComponents(Angle rotation, Angle inclination)
        {
            Angle newInclination = inclination;

            // Fix the inclination if it does not lie on [-90°, 90°].
            if (Math.Abs(newInclination) > 90.0)
            {
                // Relate the inclination angle onto [0°, 90°] by using its reference angle.
                // If the inclination angle lies in Quadrants III or IV, negate its reference angle
                // so that the final inclination may lie on [-90°, 90°].
                newInclination = Math.Sin(newInclination.TotalRadians) < 0.0 ? -newInclination.Reference() : newInclination.Reference();
            }

            // If the terminal side of the inclination angle is at either of the poles (-90° or 90°), 
            // then the rotation angle should not have a quantity.
            if (Math.Abs(newInclination) == 90.0) return (Angle.Zero, newInclination);

            // If the original inclination angle's terminal side with respect to the positive x-axis lies in 
            // Quadrants II or III, rotate the rotation angle by 180°.
            // This conceptually replaces the original obtuse angle of inclination with its supplementary angle.
            if (Math.Cos(inclination.TotalRadians) < 0.0)
            {
                rotation = rotation.Flipped();
            }

            // Relate the rotation angle onto [0°, 360°), equivalent to [0, 24) hours, if necessary.
            if (rotation < 0.0 || rotation >= 360.0)
            {
                rotation = rotation.Coterminal();
            }

            return (rotation, newInclination);
        }
    }
}
