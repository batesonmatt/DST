namespace DST.Core.Physics
{
    public readonly partial struct AstronomicalDateTime
    {
        // Provides values for how an instance of DateTimeKind.Unspecified should be treated.
        public enum UnspecifiedKind
        {
            IsLocal,
            IsUtc,
        }
    }
}