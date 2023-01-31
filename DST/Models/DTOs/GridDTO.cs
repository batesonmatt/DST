using DST.Models.DataLayer.Query;

namespace DST.Models.DTOs
{
    /* Consider renaming since the page using this DTO does not use a grid. */
    public class GridDTO
    {
        #region Properties

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string SortField { get; set; }

        public string SortDirection { get; set; } = OrderDirection.Default;

        #endregion
    }
}
