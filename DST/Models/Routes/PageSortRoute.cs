using DST.Models.DataLayer.Query;
using DST.Models.Extensions;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DST.Models.Routes
{
    public class PageSortRoute : IRouteDictionary<PageSortRoute>
    {
        #region Properties

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortField { get; set; } = Sort.Default;
        public string SortDirection { get; set; } = OrderDirection.Default;

        [JsonIgnore] public bool IsSortAscending => SortDirection.EqualsSeo(OrderDirection.AscendingAbbr);
        [JsonIgnore] public bool IsSortDescending => SortDirection.EqualsSeo(OrderDirection.DescendingAbbr);

        #endregion

        #region Constructors

        public PageSortRoute() { }

        public PageSortRoute(PageSortRoute values)
        {
            PageNumber = values.PageNumber;
            PageSize = values.PageSize;
            SortField = values.SortField;
            SortDirection = values.SortDirection;
        }

        #endregion

        #region Methods

        public void SetPageSize(int size)
        {
            if (size is >= 0 and <= 100)
            {
                PageSize = size;
            }
        }

        public int GetTotalPages(int count)
            => PageSize > 0 ? (count + PageSize - 1) / PageSize : 0;

        public void SetSort(string fieldName)
            => SortField = string.IsNullOrWhiteSpace(fieldName) ? Sort.Default : fieldName;

        public void SortAscending()
            => SortDirection = OrderDirection.AscendingAbbr;

        public void SortDescending()
            => SortDirection = OrderDirection.DescendingAbbr;

        // Sets the sorting field for a table column control and toggles the sorting direction on subsequent calls.
        public void SetTableSort(string fieldName, PageSortRoute current)
        {
            if (SortField != fieldName)
            {
                SetSort(fieldName);
            }

            if (current.SortField.EqualsSeo(fieldName) && current.IsSortAscending)
            {
                SortDescending();
            }
            else
            {
                // Always start in ascending order.
                SortAscending();
            }
        }

        public PageSortRoute Clone()
        {
            return (PageSortRoute)MemberwiseClone();
        }

        public virtual IDictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>
            {
                { nameof(PageNumber), PageNumber.ToString().ToKebabCase() },
                { nameof(PageSize), PageSize.ToString().ToKebabCase() },
                { nameof(SortField), SortField.ToKebabCase() },
                { nameof(SortDirection), SortDirection.ToKebabCase() },
            };
        }

        #endregion
    }
}