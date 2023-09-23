namespace DST.Models.DataLayer.Query
{
    public static class Paging
    {
        #region Properties

        public static int MinPageSize => 10;
        public static int MaxPageSize => 100;
        public static int DefaultPageSize => MinPageSize;

        #endregion

        #region Methods

        public static int GetTotalPages(int count, int pageSize)
        {
            return pageSize > 0 ? (count + pageSize - 1) / pageSize : 0;
        }

        public static int ClampPageNumber(int count, int pageSize, int pageNumber)
        {
            return int.Clamp(pageNumber, 0, GetTotalPages(count, pageSize));
        }

        public static int ClampPageSize(int size)
        {
            return int.Clamp(size, MinPageSize, MaxPageSize);
        }

        #endregion
    }
}