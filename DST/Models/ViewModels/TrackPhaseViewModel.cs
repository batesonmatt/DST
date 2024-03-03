using DST.Core.Vector;
using DST.Models.BusinessLogic;
using DST.Models.DomainModels;
using DST.Models.Routes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace DST.Models.ViewModels
{
    public class TrackPhaseViewModel
    {
        #region Properties

        public DsoModel Dso { get; set; }
        public TrackPhaseRoute CurrentRoute { get; set; }
        public IEnumerable<TrackAlgorithmItem> Algorithms { get; set; }
        public IEnumerable<SelectListItem> Phases { get; set; }
        public TrackPhaseModel TrackForm { get; set; }
        public IEnumerable<IVector> Results { get; set; }
        public string WarningMessage { get; set; }

        #endregion

        #region Methods

        public bool HasResults()
        {
            return Results?.Any() ?? false;
        }

        public bool IsTrackingSupported()
        {
            return Phases?.Any() ?? false;
        }

        public bool HasWarning()
        {
            return !string.IsNullOrWhiteSpace(WarningMessage);
        }

        #endregion
    }
}
