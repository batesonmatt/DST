using System;
using DST.Models.DataLayer.Query;
using DST.Models.DTOs;
using DST.Models.Extensions;
using Microsoft.AspNetCore.Http;

namespace DST.Models.Builders.Routing
{
    public class SearchRouteBuilder : RouteBuilder
    {
        #region Properties

        public bool IsFilterByType => !_routes.TypeFilter.IsDefault();
        public bool IsFilterByCatalog => !_routes.CatalogFilter.IsDefault();
        public bool IsFilterByConstellation => !_routes.ConstellationFilter.IsDefault();
        public bool IsFilterBySeason => !_routes.SeasonFilter.IsDefault();
        public bool IsFilterByTrajectory => !_routes.TrajectoryFilter.IsDefault();
        public bool IsFilterByLocal => !_routes.LocalFilter.IsDefault();
        public bool IsFilterByVisible => !_routes.VisibleFilter.IsDefault();
        public bool IsFilterByRising => !_routes.RisingFilter.IsDefault();
        public bool IsFilterByHasName => !_routes.HasNameFilter.IsDefault();

        public bool IsSortById => _routes.SortField.EqualsIgnoreCase(Sort.Id);
        public bool IsSortByName => _routes.SortField.EqualsIgnoreCase(Sort.Name);
        public bool IsSortByType => _routes.SortField.EqualsIgnoreCase(Sort.Type);
        public bool IsSortByConstellation => _routes.SortField.EqualsIgnoreCase(Sort.Constellation);
        public bool IsSortByDistance => _routes.SortField.EqualsIgnoreCase(Sort.Distance);
        public bool IsSortByBrightness => _routes.SortField.EqualsIgnoreCase(Sort.Brightness);
        public bool IsSortByRiseTime => _routes.SortField.EqualsIgnoreCase(Sort.RiseTime);

        #endregion

        #region Constructors

        public SearchRouteBuilder(ISession session)
            : base(session)
        { }

        public SearchRouteBuilder(ISession session, SearchDTO values)
            : base(session, values)
        {
            LoadFilterSegments(values.GetFilters());
        }

        #endregion

        #region Methods

        public void LoadFilterSegments(IFilter[] filters)
        {
            _ = filters ?? throw new ArgumentNullException(nameof(filters));

            _routes.SetFilters(filters);
        }

        public void ClearFilterSegments() => _routes.ClearFilters();

        #endregion
    }
}
