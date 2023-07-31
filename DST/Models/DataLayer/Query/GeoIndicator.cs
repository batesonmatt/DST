using System;

namespace DST.Models.DataLayer.Query
{
    [Obsolete("Not adding geolocation information to routing service at this time.")]
    public static class GeoIndicator
    {
        #region Properties

        public static string North { get; } = "N";
        public static string South { get; } = "S";
        public static string East { get; } = "E";
        public static string West { get; } = "W";

        public static string DefaultLatitude { get; } = "N0";
        public static string DefaultLongitude { get; } = "E0";

        #endregion
    }
}
