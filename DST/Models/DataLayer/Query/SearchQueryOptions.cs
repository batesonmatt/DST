using DST.Models.DomainModels;
using DST.Models.Builders.Routing;
using DST.Models.Builders;
using DST.Models.BusinessLogic;

namespace DST.Models.DataLayer.Query
{
    public class SearchQueryOptions : QueryOptions<DsoModel>
    {
        #region Methods

        public void SortFilter(SearchRouteBuilder builder, GeolocationModel geolocation)
        {
            if (builder is null)
            {
                return;
            }

            // Filtering

            if (builder.IsFilterByType)
            {
                Where = dso => builder.CurrentRoute.TypeFilter.EqualsSeo(dso.Type);
            }
            if (builder.IsFilterByCatalog)
            {
                Where = dso => builder.CurrentRoute.CatalogFilter.EqualsSeo(dso.CatalogName);
            }
            if (builder.IsFilterByConstellation)
            {
                Where = dso => builder.CurrentRoute.ConstellationFilter.EqualsSeo(dso.ConstellationName);
            }
            if (builder.IsFilterBySeason && geolocation is not null)
            {
                Include = "Constellation.Season";

                // Get all objects visible during the specified season, regardless of the client's geolocation.
                // The latitudinal checks here only determine the date ranges for the client's hemisphere.
                Where = geolocation.Latitude switch
                {
                    // For positive latitudes, select all constellations for the specified season in the northern date range.
                    > 0.0 => dso => builder.CurrentRoute.SeasonFilter.EqualsSeo(dso.Constellation.Season.North),

                    // For negative latitudes, select all constellations for the specified season in the southern date range.
                    < 0.0 => dso => builder.CurrentRoute.SeasonFilter.EqualsSeo(dso.Constellation.Season.South),

                    // Although all objects rise and set from the equator, they only do so seasonally.
                    // For equatorial latitudes, select all constellations for the specified season in
                    // both the northern and southern date ranges.
                    _ => dso => builder.CurrentRoute.SeasonFilter.EqualsSeo(dso.Constellation.Season.North) ||
                                  builder.CurrentRoute.SeasonFilter.EqualsSeo(dso.Constellation.Season.South)
                };
            }
            if (builder.IsFilterByTrajectory && geolocation is not null)
            {
                Where = dso => builder.CurrentRoute.TrajectoryFilter.EqualsSeo(
                    Utilities.GetPrimaryTrajectoryName(dso, geolocation));
            }
            if (builder.IsFilterByLocal && geolocation is not null)
            {
                Include = "Constellation.Season";

                Where = dso => Utilities.IsLocal(dso, geolocation);
            }
            if (builder.IsFilterByVisible && geolocation is not null)
            {
                Include = "Constellation.Season";

                Where = dso => Utilities.IsVisible(dso, geolocation);
            }
            if (builder.IsFilterByRising && geolocation is not null)
            {
                Include = "Constellation.Season";

                Where = dso => Utilities.IsRising(dso, geolocation);
            }
            if (builder.IsFilterByHasName)
            {
                Where = dso => dso.HasName;
            }

            // Sorting

            if (builder.IsSortById)
            {
                OrderByAll = new OrderClauses<DsoModel>
                {
                    { dso => dso.CatalogName },
                    { dso => dso.Id }
                };
            }
            else if (builder.IsSortByName)
            {
                OrderByAll = new OrderClauses<DsoModel>
                {
                    { dso => dso.HasName == false },
                    { dso => dso.Name },
                    { dso => dso.CatalogName },
                    { dso => dso.Id }
                };
            }
            else if (builder.IsSortByType)
            {
                OrderByAll = new OrderClauses<DsoModel>
                {
                    { dso => dso.Type },
                    { dso => dso.Description }
                };
            }
            else if (builder.IsSortByConstellation)
            {
                OrderBy = dso => dso.ConstellationName;
            }
            else if (builder.IsSortByDistance)
            {
                OrderBy = dso => dso.Distance;
            }
            else if (builder.IsSortByBrightness)
            {
                //OrderBy = dso => -(dso.Magnitude ?? double.PositiveInfinity);

                OrderByAll = new OrderClauses<DsoModel>
                {
                    { dso => -dso.Magnitude },
                    { dso => dso.Magnitude == null }
                };
            }
            else if (builder.IsSortByRiseTime && geolocation is not null)
            {
                Include = "Constellation.Season";

                OrderBy = dso => Utilities.GetRiseTime(dso, geolocation);
            }
        }

        #endregion
    }
}
