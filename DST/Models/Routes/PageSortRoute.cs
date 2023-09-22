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
            PageSize = int.Clamp(size, 10, 100);
        }

        public void SetPageNumber(int number)
        {
            PageNumber = number < 1 ? 1 : number;
        }

        public int GetTotalPages(int count)
            => Paging.GetTotalPages(count, PageSize);

        public string GetResultsInfo(int count)
        {
            int pages = GetTotalPages(count);

            if (pages <= 1 && count <= PageSize)
            {
                if (count == 1)
                {
                    return "1 result";
                }
                else
                {
                    return string.Format("{0} results", count);
                }
            }
            else
            {
                int fullPages = count / PageSize;
                int first = ((PageSize * PageNumber) - PageSize) + 1;
                int last = PageNumber <= fullPages ? PageSize * PageNumber : first + (count % PageSize) - 1;

                return string.Format("{0}-{1} of {2} results", first, last, count);
            }
        }

        public void SetSort(string fieldName)
            => SortField = string.IsNullOrWhiteSpace(fieldName) ? Sort.Default : fieldName;

        public void SetSortDirection(string direction)
        {
            if (direction.EqualsSeo(OrderDirection.AscendingAbbr))
            {
                SortDirection = OrderDirection.AscendingAbbr;
            }
            else if (direction.EqualsSeo(OrderDirection.DescendingAbbr))
            {
                SortDirection = OrderDirection.DescendingAbbr;
            }
            else
            {
                SortDirection = OrderDirection.Default;
            }
        }

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

        public virtual void Validate()
        {
            SetPageSize(PageSize);
            SetPageNumber(PageNumber);
            SetSort(SortField);
            SetSortDirection(SortDirection);
        }

        #endregion
    }
}