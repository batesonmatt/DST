using DST.Models.DataLayer.Query;
using DST.Models.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DST.Models.DomainModels
{
    public class SortFilterModel
    {
        #region Properties

        public string SortField { get; set; } = Sort.Default;
        public string SortDirection { get; set; } = OrderDirection.Default;
        public int PageSize { get; set; } = Paging.DefaultPageSize;

        public string Catalog { get; set; } = Filter.All;
        public string Type { get; set; } = Filter.All;
        public string Constellation { get; set; } = Filter.All;
        public string Season { get; set; } = Filter.All;
        public string Trajectory { get; set; } = Filter.All;
        public string Visibility { get; set; } = Filter.Any;
        
        public bool IsFilterByHasName { get; set; } = false;

        [BindNever]
        public bool IsSortAscending => SortDirection.EqualsSeo(OrderDirection.Ascending);

        [BindNever]
        public bool IsSortDescending => SortDirection.EqualsSeo(OrderDirection.Descending);

        #endregion
    }
}
