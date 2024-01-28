using DST.Core.Observer;
using DST.Models.DomainModels;
using DST.Models.Routes;
using System.Collections.Generic;

namespace DST.Models.ViewModels
{
    public class TrackSummaryViewModel
    {
        #region Properties

        public DsoModel Dso { get; set; }
        public ILocalObserver ClientObserver { get; set; }
        public TrackSummaryRoute CurrentRoute { get; set; }
        public IDictionary<string, string> DisplayInfo { get; set; }

        #endregion
    }
}
