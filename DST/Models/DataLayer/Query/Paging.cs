namespace DST.Models.DataLayer.Query
{
    public static class Paging
    {
        #region Properties

        public static int MinPageNumber => 1;
        public static int MinPageSize => 10;
        public static int MaxPageSize => 100;
        public static int DefaultPageSize => MinPageSize;

        #endregion

        #region Methods

        public static int GetTotalPages(int count, int pageSize)
        {
            if (count < MinPageNumber)
            {
                count = MinPageNumber;
            }

            return pageSize > 0 ? (count + pageSize - 1) / pageSize : MinPageNumber;
        }

        public static int ClampPageNumber(int count, int pageSize, int pageNumber)
        {
            return int.Clamp(pageNumber, MinPageNumber, GetTotalPages(count, pageSize));
        }

        public static int ClampPageSize(int size)
        {
            return int.Clamp(size, MinPageSize, MaxPageSize);
        }

        #endregion
    }
}