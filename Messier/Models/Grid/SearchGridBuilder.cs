using System;
using Messier.Models.DataLayer.Query;
using Messier.Models.DTOs;
using Messier.Models.Extensions;
using Microsoft.AspNetCore.Http;

namespace Messier.Models.Grid
{
    public class SearchGridBuilder : GridBuilder
    {
        #region Properties

        public bool IsFilterByType => !_routes.TypeFilter.IsDefault();
        public bool IsFilterByCatalog => !_routes.CatalogFilter.IsDefault();
        public bool IsFilterByConstellation => !_routes.ConstellationFilter.IsDefault();
        public bool IsFilterBySeason => !_routes.SeasonFilter.IsDefault();
        public bool IsFilterByLocal => !_routes.LocalFilter.IsDefault();
        public bool IsFilterByHasName => !_routes.HasNameFilter.IsDefault();
        public bool IsFilterByVisibility => !_routes.VisibilityFilter.IsDefault();
        public bool IsFilterByRiseTime => !_routes.RiseTimeFilter.IsDefault();
        public bool IsFilterByTrajectory => !_routes.TrajectoryFilter.IsDefault();

        public bool IsSortById => _routes.SortField.EqualsIgnoreCase(Sort.Id);
        public bool IsSortByName => _routes.SortField.EqualsIgnoreCase(Sort.Name);
        public bool IsSortByType => _routes.SortField.EqualsIgnoreCase(Sort.Type);
        public bool IsSortByConstellation => _routes.SortField.EqualsIgnoreCase(Sort.Constellation);
        public bool IsSortByDistance => _routes.SortField.EqualsIgnoreCase(Sort.Distance);
        public bool IsSortByBrightness => _routes.SortField.EqualsIgnoreCase(Sort.Brightness);
        public bool IsSortByVisibility => _routes.SortField.EqualsIgnoreCase(Sort.Visibility);
        public bool IsSortByRiseTime => _routes.SortField.EqualsIgnoreCase(Sort.RiseTime);

        #endregion

        #region Constructors

        public SearchGridBuilder(ISession session)
            : base(session)
        { }

        public SearchGridBuilder(ISession session, SearchGridDTO values)
            : base(session, values)
        {
            LoadFilterSegments(values.Filters);
        }

        #endregion

        #region Methods

        public void LoadFilterSegments(IFilter[] filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            _routes.SetFilters(filters);
        }

        public void ClearFilterSegments() => _routes.ClearFilters();

        #endregion
    }
}
