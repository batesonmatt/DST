namespace DST.Models.DataLayer.Query
{
    /* Revise this concept, now that the DST.Core library has been added. */
    public static class Trajectory
    {
        #region Properties

        public static string Circumpolar { get; } = "Circumpolar";
        public static string RiseSet { get; } = "Rise and set";
        public static string NeverRise { get; } = "Never rise";

        #endregion
    }
}
