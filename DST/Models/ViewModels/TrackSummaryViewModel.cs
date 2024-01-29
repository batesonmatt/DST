using DST.Models.DomainModels;
using DST.Models.Routes;

namespace DST.Models.ViewModels
{
    public class TrackSummaryViewModel
    {
        #region Properties

        public DsoModel Dso { get; set; }
        public TrackSummaryRoute CurrentRoute { get; set; }
        public TrackSummaryInfo DisplayInfo { get; set; }

        #endregion
    }
}
