using DST.Models.BusinessLogic;
using System.Collections.Generic;

namespace DST.Models.ViewModels
{
    public class TrackResultsViewModel
    {
        #region Properties

        public string TimeKeeper { get; set; }
        public string TimeScale { get; set; }
        public string Start { get; set; }
        public IEnumerable<TrackResult> Results { get; set; }

        #endregion
    }
}
