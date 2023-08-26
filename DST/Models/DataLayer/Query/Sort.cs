using System.Collections.Generic;

namespace DST.Models.DataLayer.Query
{
    /* Consider revising */
    public static class Sort
    {
        #region Properties

        public static string Id { get; } = "id";
        public static string Name { get; } = "name";
        public static string Type { get; } = "type";
        public static string Constellation { get; } = "constellation";
        public static string Distance { get; } = "distance";
        public static string Brightness { get; } = "brightness";
        public static string RiseTime { get; } = "rise-time";

        public static string Default { get; } = Id;

        public static Dictionary<string, string> GetFields()
            => new()
            {
                { "Id", Id },
                { "Name", Name },
                { "Type", Type },
                { "Constellation", Constellation },
                { "Distance", Distance },
                { "Brightness", Brightness },
                { "Rise Time", RiseTime }
            };

        #endregion
    }
}
