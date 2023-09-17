using DST.Models.DataLayer.Query;
using DST.Models.Extensions;
using System.Collections.Generic;

namespace DST.Models.Routes
{
    public class PageSortRoute : IRouteDictionary<PageSortRoute>
    {
        #region Properties

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string SortField { get; set; } = Sort.Default;

        public string SortDirection { get; set; } = OrderDirection.Default;

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
            if (size is >= 0 and < int.MaxValue)
            {
                PageSize = size;
            }
        }

        public int GetTotalPages(int count)
            => PageSize > 0 ? (count + PageSize - 1) / PageSize : 0;

        public void SetSort(string fieldName)
            => SortField = string.IsNullOrWhiteSpace(fieldName) ? Sort.Default : fieldName;

        public void SetDirection(string direction)
            => SortDirection = string.IsNullOrWhiteSpace(direction) ? OrderDirection.Default : direction;

        // Sets the sorting field for a table column control and toggles the sorting direction on subsequent calls.
        public void SetTableSort(string fieldName, PageSortRoute current)
        {
            if (SortField != fieldName)
            {
                SetSort(fieldName);
            }

            if (current.SortField.EqualsSeo(fieldName) && current.SortDirection.EqualsSeo(OrderDirection.AscendingAbbr))
            {
                SortDirection = OrderDirection.DescendingAbbr;
            }
            else
            {
                // Always start in ascending order.
                SortDirection = OrderDirection.AscendingAbbr;
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