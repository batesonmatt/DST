using DST.Models.DomainModels;
using DST.Models.BusinessLogic;
using DST.Models.Extensions;
using DST.Models.Routes;

namespace DST.Models.DataLayer.Query
{
    public class SearchQueryOptions : QueryOptions<DsoModel>
    {
        #region Methods

        public void SortFilter(SearchRoute values, GeolocationModel geolocation)
        {
            if (values is null)
            {
                return;
            }

            // Filtering

            if (values.IsFilterByType)
            {
                Where = dso => values.Type.EqualsSeo(dso.Type);
            }
            if (values.IsFilterByCatalog)
            {
                Where = dso => values.Catalog.EqualsSeo(dso.CatalogName);
            }
            if (values.IsFilterByConstellation)
            {
                Where = dso => values.Constellation.EqualsSeo(dso.ConstellationName);
            }
            if (values.IsFilterBySeason && geolocation is not null)
            {
                Include = "Constellation.Season";

                // Get all objects visible during the specified season, regardless of the client's geolocation.
                // The latitudinal checks here only determine the date ranges for the client's hemisphere.
                Where = geolocation.Latitude switch
                {
                    // For positive latitudes, select all constellations for the specified season in the northern date range.
                    > 0.0 => dso => values.Season.EqualsSeo(dso.Constellation.Season.North),

                    // For negative latitudes, select all constellations for the specified season in the southern date range.
                    < 0.0 => dso => values.Season.EqualsSeo(dso.Constellation.Season.South),

                    // Although all objects rise and set from the equator, they only do so seasonally.
                    // For equatorial latitudes, select all constellations for the specified season in
                    // both the northern and southern date ranges.
                    _ => dso => values.Season.EqualsSeo(dso.Constellation.Season.North) ||
                                  values.Season.EqualsSeo(dso.Constellation.Season.South)
                };
            }
            if (values.IsFilterByTrajectory && geolocation is not null)
            {
                Where = dso => values.Trajectory.EqualsSeo(
                    Utilities.GetPrimaryTrajectoryName(dso, geolocation));
            }
            if (values.IsFilterByLocal && geolocation is not null)
            {
                Include = "Constellation.Season";

                Where = dso => Utilities.IsLocal(dso, geolocation);
            }
            if (values.IsFilterByVisible && geolocation is not null)
            {
                Include = "Constellation.Season";

                Where = dso => Utilities.IsVisible(dso, geolocation);
            }
            if (values.IsFilterByRising && geolocation is not null)
            {
                Include = "Constellation.Season";

                Where = dso => Utilities.IsRising(dso, geolocation);
            }
            if (values.IsFilterByHasName)
            {
                Where = dso => dso.HasName;
            }

            // Sorting

            if (values.IsSortById)
            {
                OrderByAll = new OrderClauses<DsoModel>
                {
                    { dso => dso.CatalogName },
                    { dso => dso.Id }
                };
            }
            else if (values.IsSortByName)
            {
                OrderByAll = new OrderClauses<DsoModel>
                {
                    { dso => dso.HasName == false },
                    { dso => dso.Name },
                    { dso => dso.CatalogName },
                    { dso => dso.Id }
                };
            }
            else if (values.IsSortByType)
            {
                OrderByAll = new OrderClauses<DsoModel>
                {
                    { dso => dso.Type },
                    { dso => dso.Description }
                };
            }
            else if (values.IsSortByConstellation)
            {
                OrderBy = dso => dso.ConstellationName;
            }
            else if (values.IsSortByDistance)
            {
                OrderBy = dso => dso.Distance;
            }
            else if (values.IsSortByBrightness)
            {
                //OrderBy = dso => -(dso.Magnitude ?? double.PositiveInfinity);

                OrderByAll = new OrderClauses<DsoModel>
                {
                    { dso => -dso.Magnitude },
                    { dso => dso.Magnitude == null }
                };
            }
            else if (values.IsSortByRiseTime && geolocation is not null)
            {
                Include = "Constellation.Season";

                OrderBy = dso => Utilities.GetRiseTime(dso, geolocation);
            }
        }

        #endregion
    }
}
