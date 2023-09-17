namespace DST.Models.DataLayer.Query
{
    public static class OrderDirection
    {
        #region Properties

        public static string Ascending => Resources.DisplayText.SortAscending;
        public static string Descending => Resources.DisplayText.SortDescending;
        public static string AscendingAbbr => Resources.DisplayText.SortAsc;
        public static string DescendingAbbr => Resources.DisplayText.SortDesc;

        public static string Default => AscendingAbbr;

        #endregion
    }
}
