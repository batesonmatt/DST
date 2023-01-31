using System;
using System.Linq.Expressions;

namespace DST.Models.DataLayer.Query
{
    public class QueryOptions<T>
    {
        #region Properties

        public string SortDirection { get; set; } = OrderDirection.Default;

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public OrderClauses<T> OrderByAll { get; set; }

        public Expression<Func<T, object>> OrderBy
        {
            set
            {
                OrderByAll ??= new OrderClauses<T>();

                OrderByAll.Add(value);
            }
        }

        public WhereClauses<T> WhereAll { get; set; }

        public Expression<Func<T, bool>> Where
        {
            set
            {
                WhereAll ??= new WhereClauses<T>();

                WhereAll.Add(value);
            }
        }

        public bool HasWhere => WhereAll is not null;

        public bool HasOrderBy => OrderByAll is not null;

        public bool HasPaging => PageNumber > 0 && PageSize > 0;

        public string Include
        {
            set => _includes = value?.Replace(" ", string.Empty).Split(',');
        }

        #endregion

        #region Fields

        private string[] _includes;

        #endregion

        #region Methods

        public string[] GetIncludes()
        {
            return _includes ?? Array.Empty<string>();
        }

        #endregion
    }
}