using DST.Models.DomainModels;
using DST.Models.Extensions;
using DST.Models.Grid;

namespace DST.Models.DataLayer.Query
{
    public class SearchQueryOptions : QueryOptions<DsoModel>
    {
        #region Methods
        /* builder property values are all default */
        public void SortFilter(SearchGridBuilder builder, GeoLocationModel geoLocation)
        {
            if (builder is not null)
            {
                if (builder.IsFilterByType)
                {
                    Where = model => builder.CurrentRoute.TypeFilter.EqualsSeo(model.Type);
                }
                if (builder.IsFilterByCatalog)
                {
                    Where = model => builder.CurrentRoute.CatalogFilter.EqualsSeo(model.CatalogName);
                }
                if (builder.IsFilterByConstellation)
                {
                    Where = model => builder.CurrentRoute.ConstellationFilter.EqualsSeo(model.ConstellationName);
                }
                if (builder.IsFilterBySeason)
                {
                    /* Needs geolocation */
                }
                if (builder.IsFilterByTrajectory)
                {
                    /* Needs geolocation */
                }
                if (builder.IsFilterByLocal)
                {
                    /* Needs geolocation */
                }
                if (builder.IsFilterByHasName)
                {
                    Where = model => !string.IsNullOrWhiteSpace(model.Name);
                }
                if (builder.IsFilterByVisibility)
                {
                    /* Needs geolocation */
                }
                if (builder.IsFilterByRiseTime)
                {
                    /* Needs geolocation */
                }

                if (builder.IsSortById)
                {
                    OrderByAll = new OrderClauses<DsoModel>
                    {
                        { model => model.CatalogName },
                        { model => model.Id }
                    };
                }
                else if (builder.IsSortByName)
                {
                    OrderBy = model => model.Name;
                }
                else if (builder.IsSortByType)
                {
                    OrderByAll = new OrderClauses<DsoModel>
                    {
                        { model => model.Type },
                        { model => model.Description }
                    };
                }
                else if (builder.IsSortByConstellation)
                {
                    OrderBy = model => model.ConstellationName;
                }
                else if (builder.IsSortByDistance)
                {
                    OrderBy = model => model.Distance;
                }
                else if (builder.IsSortByBrightness)
                {
                    OrderBy = model => model.Magnitude;
                }
                else if (builder.IsSortByVisibility)
                {
                    /* Needs geolocation */
                }
                else if (builder.IsSortByRiseTime)
                {
                    /* Needs geolocation */
                }
            }
        }

        #endregion
    }
}
