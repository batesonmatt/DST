using DST.Models.DomainModels;
using DST.Models.Grid;

namespace DST.Models.DataLayer.Query
{
    public class SearchQueryOptions : QueryOptions<DsoModel>
    {
        #region Methods
        /* builder property values are all default */
        public void SortFilter(SearchGridBuilder builder, GeolocationModel geolocation)
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
                /* Check Where = model => model.Constellation.* ... */
                /* Use builder.CurrentRoute.SeasonFilter.Value, or .Id */

                if (geolocation is not null)
                {
                    
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
            }
            else if (builder.IsSortByRiseTime)
            {
                /* Needs geolocation */
            }
        }

        #endregion
    }
}
