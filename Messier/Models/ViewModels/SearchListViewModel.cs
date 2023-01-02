using Messier.Models.DomainModels;
using Messier.Models.Grid;
using Messier.Models.DataLayer.Query;
using System.Collections.Generic;

namespace Messier.Models.ViewModels
{
    public class SearchListViewModel
    {
        #region Properties

        public GeoLocationModel GeoLocation { get; set; }
        public IEnumerable<DsoModel> DsoItems { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }

        // Filter data
        public IEnumerable<DsoTypeModel> Types { get; set; }
        public IEnumerable<CatalogModel> Catalogs { get; set; }
        public IEnumerable<ConstellationModel> Constellations { get; set; }
        public IEnumerable<SeasonModel> Seasons { get; set; }

        public Dictionary<string, string> Trajectories
            => new Dictionary<string, string>
            {
                { nameof(Trajectory.RiseSet), Trajectory.RiseSet },
                { nameof(Trajectory.Circumpolar), Trajectory.Circumpolar },
                { nameof(Trajectory.NeverRise), Trajectory.NeverRise }
            };

        #endregion
    }
}
