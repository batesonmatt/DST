using DST.Models.DomainModels;
using DST.Models.DataLayer.Query;
using System.Collections.Generic;
using DST.Models.Builders.Routing;

namespace DST.Models.ViewModels
{
    public class SearchListViewModel
    {
        #region Properties

        // Geolocation data
        public GeolocationModel Geolocation { get; set; }
        public IEnumerable<TimeZoneItem> TimeZoneItems { get; set; }

        public IEnumerable<DsoModel> DsoItems { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }

        // Filter data
        public IEnumerable<DsoTypeModel> Types { get; set; }
        public IEnumerable<CatalogModel> Catalogs { get; set; }
        public IEnumerable<ConstellationModel> Constellations { get; set; }
        public IEnumerable<SeasonModel> Seasons { get; set; }

        /* Eventually remove the initializer list */
        /* Use nameof(SearchDTO.Trajectory) in the view like the other filter lists. */
        public IEnumerable<string> Trajectories { get; set; }
            = new List<string>()
        {
            Core.Resources.DisplayText.TrajectoryCircumpolar,
            Core.Resources.DisplayText.TrajectoryNeverRise,
            Core.Resources.DisplayText.TrajectoryRiseAndSet
        };

        #endregion
    }
}
