using DST.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DST.Models.DataLayer.Query
{
    public class QueryOptions<T>
    {
        #region Properties

        public string SortDirection { get; set; } = OrderDirection.Default;

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public bool IsSortAscending => SortDirection.EqualsSeo(OrderDirection.AscendingAbbr);

        public bool IsSortDescending => SortDirection.EqualsSeo(OrderDirection.DescendingAbbr);

        public List<string> Includes { get; set; } = null!;

        public string Include
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value) == false)
                {
                    Includes ??= new List<string>();

                    if (Includes.Contains(value) == false)
                    {
                        Includes.Add(value);
                    }
                }
            }
        }

        public string IncludeAll
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value) == false)
                {
                    Includes ??= new List<string>();
                    Includes.AddRange(value.Replace(" ", string.Empty).Split(',').Where(i => !Includes.Contains(i)));
                }
            }
        }

        public WhereClauses<T> WhereAll { get; set; } = null!;

        public Expression<Func<T, bool>> Where
        {
            set
            {
                WhereAll ??= new WhereClauses<T>();

                WhereAll.Add(value);
            }
        }

        public OrderClauses<T> OrderByAll { get; set; } = null!;

        public Expression<Func<T, object>> OrderBy
        {
            set
            {
                OrderByAll ??= new OrderClauses<T>();

                OrderByAll.Add(value);
            }
        }

        public bool HasPaging => PageNumber > 0 && PageSize > 0;

        public bool HasInclude => Includes?.Count > 0;

        public bool HasWhere => WhereAll?.Count > 0;

        public bool HasOrderBy => OrderByAll?.Count > 0;

        #endregion
    }
}