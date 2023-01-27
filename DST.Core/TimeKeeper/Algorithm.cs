namespace DST.Core.TimeKeeper
{
    // Provides values for various timekeeping algorithms.
    public enum Algorithm
    {
        GMST, // Greenwich Mean Sidereal Time
        GAST, // Greenwich Apparent Sidereal Time
        ERA, // Earth Rotation Angle
        Default = GMST
    }
}