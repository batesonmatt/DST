using DST.Models.DomainModels;
using DST.Models.Builders.Routing;
using DST.Models.Builders;

namespace DST.Models.DataLayer.Query
{
    public class SearchQueryOptions : QueryOptions<DsoModel>
    {
        #region Methods
        /* builder property values are all default */
        public void SortFilter(SearchRouteBuilder builder, GeolocationBuilder geoBuilder)
        {
            if (builder is null)
            {
                return;
            }

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
                /* Check latitude to determine north/south hemisphere */
                /* Don't check if user latitude is within range of the Constellation NorthernLatitude/SouthernLatitude.
                 *      This will be part of the visibility filter (not visibility sort).
                 */
                /* Check Where = model => model.Constellation.* ... */
                /* Use builder.CurrentRoute.SeasonFilter.Value, or .Id */

                /* Testing */
                if (geoBuilder is not null)
                {
                    switch (geoBuilder.CurrentGeolocation.Latitude)
                    {
                        case > 0.0:
                            /* Look at Season Id for North value */
                            /* Get the Season Id for when North = SeasonFilter */
                            /* Get the Constellation for when SeasonId = Id */
                            Where = model => builder.CurrentRoute.SeasonFilter.EqualsSeo(model.Constellation.Season.North);
                            break;
                        case < 0.0:
                            /* Look at Season Id for South value */
                            /* Get the Season Id for when South = SeasonFilter */
                            /* Get the Constellation for when SeasonId = Id */
                            Where = model => builder.CurrentRoute.SeasonFilter.EqualsSeo(model.Constellation.Season.South);
                            break;
                        default:
                            /* Look at Season Id for both North and South values */
                            /* Get the Season Id for when North = SeasonFilter */
                            /* Get the Season Id for when South = SeasonFilter */
                            /* Get the Constellation for when SeasonId = the North Id, or the South Id */
                            Where = model => builder.CurrentRoute.SeasonFilter.EqualsSeo(model.Constellation.Season.North) ||
                                             builder.CurrentRoute.SeasonFilter.EqualsSeo(model.Constellation.Season.South);
                            break;
                    }
                }
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
