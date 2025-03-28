﻿using DST.Core.Physics;

namespace DST.Core.Components
{
    public class ComponentsFactory
    {
        // Returns a new IComponents object with default Angle values.
        public static IComponents Create()
        {
            return new SphericalComponents(Angle.Zero, Angle.Zero);
        }

        // Returns a new IComponents object given the specified angular rotation and inclination Angle values.
        public static IComponents Create(Angle rotation, Angle inclination)
        {
            return new SphericalComponents(rotation, inclination);
        }
    }
}
