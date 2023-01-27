namespace DST.Core.Physics
{
    public readonly partial struct Angle
    {
        // Provides miscellaneous values for formatting this Angle as a string.
        public enum FormatModifierType
        {
            None,
            Signed,
            Unsigned,
            Latitude,
            Longitude,
        }
    }
}