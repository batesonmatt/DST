using DST.Models.DomainModels;
using System.Collections.Generic;
using DST.Models.Routes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DST.Models.ViewModels
{
    public class SearchListViewModel
    {
        #region Properties

        public SearchRoute CurrentRoute { get; set; }
        public SearchModel Search { get; set; }
        public SortFilterModel SortFilter { get; set; }
        public IEnumerable<DsoModel> DsoItems { get; set; }

        public IEnumerable<SelectListItem> SortFields { get; set; }
        public IEnumerable<SelectListItem> PageSizes { get; set; }
        public IEnumerable<SelectListItem> Catalogs { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<SelectListItem> Constellations { get; set; }
        public IEnumerable<SelectListItem> Seasons { get; set; }
        public IEnumerable<SelectListItem> Trajectories { get; set; }
        public IEnumerable<SelectListItem> Visibilities { get; set; }

        public string Results { get; set; }
        public int TotalPages { get; set; }

        #endregion
    }
}
