using DST.Models.DomainModels;
using System.Collections.Generic;
using DST.Models.Routes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DST.Models.ViewModels
{
    public class SearchListViewModel
    {
        #region Properties

        public SearchModel Search { get; set; }

        public SearchRoute CurrentRoute { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<SelectListItem> PageSizes { get; set; }
        public string Results { get; set; }

        public IEnumerable<DsoModel> DsoItems { get; set; }
        public IEnumerable<DsoTypeModel> Types { get; set; }
        public IEnumerable<CatalogModel> Catalogs { get; set; }
        public IEnumerable<ConstellationModel> Constellations { get; set; }
        public IEnumerable<SeasonModel> Seasons { get; set; }
        public IEnumerable<string> Trajectories { get; set; }

        #endregion
    }
}
