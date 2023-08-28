namespace DST.Models.DataLayer.Query
{
    /* Consider revising */
    public static class Sort
    {
        #region Properties

        public static string Id { get; } = "Id";
        public static string Name { get; } = "Name";
        public static string Type { get; } = "Type";
        public static string Constellation { get; } = "Constellation";
        public static string Distance { get; } = "Distance";
        public static string Brightness { get; } = "Brightness";
        public static string RiseTime { get; } = "Rise Time";

        public static string Default { get; } = Id;

        public static string[] GetFields() 
            => new[]
            {
                Id,
                Name,
                Type,
                Constellation,
                Distance,
                Brightness,
                RiseTime
            };

        #endregion
    }
}
