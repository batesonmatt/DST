using DST.Models.BusinessLogic;
using DST.Models.DomainModels;
using DST.Models.Routes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DST.Models.ViewModels
{
    public class TrackSummaryViewModel
    {
        #region Properties

        public DsoModel Dso { get; set; }
        public TrackSummaryRoute CurrentRoute { get; set; }
        public TrackSummaryInfo SummaryInfo { get; set; }
        public IEnumerable<SelectListItem> Algorithms { get; set; }

        #endregion
    }
}
