using DST.Core.Observer;
using DST.Core.Vector;
using DST.Models.BusinessLogic;
using DST.Models.DomainModels;
using DST.Models.Routes;
using System.Collections.Generic;

namespace DST.Models.ViewModels
{
    public class TrackPhaseViewModel
    {
        #region Properties

        public DsoModel Dso { get; set; }
        public TrackPhaseRoute CurrentRoute { get; set; }
        public IEnumerable<TrackAlgorithmItem> Algorithms { get; set; }
        public IEnumerable<TrackPhaseItem> Phases { get; set; }
        public TrackPhaseModel PhaseModel { get; set; }
        public IEnumerable<IVector> Results { get; set; }

        #endregion
    }
}
