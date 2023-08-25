using DST.Models.DTOs;
using DST.Models.Extensions;
using Microsoft.AspNetCore.Http;

namespace DST.Models.Builders.Routing
{
    public class RouteBuilder
    {
        #region Properties

        public RouteDictionary CurrentRoute => _routes;

        #endregion

        #region Fields

        protected const string _routeKey = "currentroute";

        protected RouteDictionary _routes;

        protected ISession _session;

        #endregion

        #region Constructors

        public RouteBuilder(ISession session)
        {
            _session = session;

            _routes = _session.GetObject<RouteDictionary>(_routeKey) ?? new RouteDictionary();
        }

        public RouteBuilder(ISession session, PageSortDTO values)
        {
            _session = session;

            // Clear previous route segments.
            _routes = new RouteDictionary
            {
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                SortField = values.SortField,
                SortDirection = values.SortDirection
            };

            SaveRouteSegments();
        }

        #endregion

        #region Methods

        public void SaveRouteSegments() => _session.SetObject(_routeKey, _routes);

        public int GetTotalPages(int count)
        {
            int size = _routes.PageSize;

            return size > 0 ? (count + size - 1) / size : 0;
        }

        #endregion
    }
}
