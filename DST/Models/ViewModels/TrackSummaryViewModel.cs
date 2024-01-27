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
        
        public IDictionary<string, string> TargetInfo { get; set; }
        public IDictionary<string, string> ObserverInfo { get; set; }
        public IDictionary<string, string> TrackerInfo { get; set; }

        #endregion
    }
}
