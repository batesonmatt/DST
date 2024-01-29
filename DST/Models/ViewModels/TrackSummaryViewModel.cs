using DST.Models.BusinessLogic;
using DST.Models.DomainModels;
using DST.Models.Routes;
using System.Collections.Generic;

namespace DST.Models.ViewModels
{
    public class TrackSummaryViewModel
    {
        #region Properties

        public DsoModel Dso { get; set; }
        public TrackSummaryRoute CurrentRoute { get; set; }
        public TrackSummaryInfo DisplayInfo { get; set; }
        public IEnumerable<TrackAlgorithmItem> Algorithms { get; set; }

        #endregion
    }
}
