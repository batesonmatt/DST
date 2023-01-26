namespace DST.Models.DataLayer.Query
{
    public static class Sort
    {
        #region Properties

        public static string Id { get; } = "id";
        public static string Name { get; } = "name";
        public static string Type { get; } = "type";
        public static string Constellation { get; } = "constellation";
        public static string Distance { get; } = "distance";
        public static string Brightness { get; } = "brightness";
        public static string Visibility { get; } = "visibility";
        public static string RiseTime { get; } = "rise-time";

        public static string Default { get; } = Id;

        #endregion
    }
}
