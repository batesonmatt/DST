namespace DST.Models.DataLayer.Query
{
    public static class Paging
    {
        public static int GetTotalPages(int count, int pageSize)
        {
            return pageSize > 0 ? (count + pageSize - 1) / pageSize : 0;
        }

        public static int ClampPageNumber(int count, int pageSize, int pageNumber)
        {
            return int.Clamp(pageNumber, 1, GetTotalPages(count, pageSize));
        }
    }
}
