using DST.Models.DataLayer.Query;
using DST.Models.DTOs;
using DST.Models.Extensions;
using Microsoft.AspNetCore.Http;

namespace DST.Models.Grid
{
    public class GridBuilder
    {
        #region Properties

        public RouteDictionary CurrentRoute => _routes;

        #endregion

        #region Fields

        protected const string _routeKey = "currentroute";

        protected RouteDictionary _routes;

        protected readonly ISession _session;

        #endregion

        #region Constructors

        public GridBuilder(ISession session)
        {
            _session = session;

            _routes = _session.GetObject<RouteDictionary>(_routeKey) ?? new RouteDictionary();
        }

        public GridBuilder(ISession session, GridDTO values)
        {
            _session = session;

            // Clear previous route segments.
            _routes = new RouteDictionary
            {
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                SortField = values.SortField ?? Sort.Default,
                SortDirection = values.SortDirection
            };

            SaveRouteSegments();
        }

        #endregion

        #region Methods

        public void SaveRouteSegments() => _session.SetObject<RouteDictionary>(_routeKey, _routes);

        public int GetTotalPages(int count)
        {
            int size = _routes.PageSize;

            return size > 0 ? (count + size - 1) / size : 0;
        }

        #endregion
    }
}
