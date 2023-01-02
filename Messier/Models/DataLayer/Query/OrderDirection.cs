namespace Messier.Models.DataLayer.Query
{
    public static class OrderDirection
    {
        #region Properties

        public static string Ascending { get; } = "asc";
        public static string Descending { get; } = "desc";

        public static string Default { get; } = Ascending;

        #endregion
    }
}
