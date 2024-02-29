using DST.Core.Vector;
using DST.Models.BusinessLogic;
using DST.Models.DomainModels;
using DST.Models.Routes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DST.Models.ViewModels
{
    public class TrackPhaseViewModel
    {
        #region Properties

        public DsoModel Dso { get; set; }
        public TrackPhaseRoute CurrentRoute { get; set; }
        public IEnumerable<TrackAlgorithmItem> Algorithms { get; set; }
        public List<SelectListItem> Phases { get; set; }
        public TrackPhaseModel TrackForm { get; set; }
        public IEnumerable<IVector> Results { get; set; }

        #endregion
    }
}
