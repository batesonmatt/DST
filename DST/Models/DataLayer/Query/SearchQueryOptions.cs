using DST.Models.DomainModels;
using DST.Models.Builders.Routing;
using DST.Models.Builders;
using DST.Models.BusinessLogic;

namespace DST.Models.DataLayer.Query
{
    public class SearchQueryOptions : QueryOptions<DsoModel>
    {
        #region Methods

        public void SortFilter(SearchRouteBuilder builder, GeolocationBuilder geoBuilder)
        {
            if (builder is null)
            {
                return;
            }

            // Filtering

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
                if (geoBuilder is not null)
                {
                    Include = "Constellation.Season";

                    Where = geoBuilder.CurrentGeolocation.Latitude switch
                    {
                        // For positive latitudes, select all constellations for the specified season in the northern date range.
                        > 0.0 => model => builder.CurrentRoute.SeasonFilter.EqualsSeo(model.Constellation.Season.North),

                        // For negative latitudes, select all constellations for the specified season in the southern date range.
                        < 0.0 => model => builder.CurrentRoute.SeasonFilter.EqualsSeo(model.Constellation.Season.South),

                        // For equatorial latitudes, select all constellations for the specified season in
                        // both the northern and southern date ranges.
                        _ => model => builder.CurrentRoute.SeasonFilter.EqualsSeo(model.Constellation.Season.North) ||
                                      builder.CurrentRoute.SeasonFilter.EqualsSeo(model.Constellation.Season.South),
                    };
                }
            }
            if (builder.IsFilterByTrajectory)
            {
                if (geoBuilder is not null)
                {
                    Where = model => builder.CurrentRoute.TrajectoryFilter.EqualsSeo(
                        Utilities.GetTrajectoryName(model, geoBuilder.CurrentGeolocation));
                }
            }
            if (builder.IsFilterByLocal)
            {
                /* Needs geolocation */
            }
            if (builder.IsFilterByHasName)
            {
                Where = model => model.HasName;
            }
            if (builder.IsFilterByVisibility)
            {
                /* Needs geolocation */
            }
            if (builder.IsFilterByRiseTime)
            {
                /* Needs geolocation */
            }

            // Sorting

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
                OrderByAll = new OrderClauses<DsoModel>
                {
                    { model => model.HasName == false },
                    { model => model.Name },
                    { model => model.CatalogName },
                    { model => model.Id }
                };
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
                // The brightness of a deep sky object is inversely proportional to its magnitude.
                // DsoModel.Magnitude allows null, for which the object is said to have no brightness and
                // should be sorted as having the highest magnitude (darkest).
                OrderBy = model => -(model.Magnitude ?? double.PositiveInfinity);
            }
            else if (builder.IsSortByVisibility)
            {
                /* Needs geolocation */
                /* This will be based on a lot of factors. */
            }
            else if (builder.IsSortByRiseTime)
            {
                /* Needs geolocation */
            }
        }

        #endregion
    }
}
