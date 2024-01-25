namespace DST.Models.DataLayer.Query
{
    public static class PhaseName
    {
        #region Properties

        public static string Rise => Resources.DisplayText.AltitudeRise;
        public static string Apex => Resources.DisplayText.AltitudeApex;
        public static string Set => Resources.DisplayText.AltitudeSet;

        public static string Default => Rise;

        #endregion
    }
}
