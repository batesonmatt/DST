using DST.Models.BusinessLogic;
using DST.Models.DomainModels;
using DST.Models.Routes;
using System.Collections.Generic;
using System.Linq;

namespace DST.Models.ViewModels
{
    public class TrackPeriodViewModel
    {
        #region Properties

        public DsoModel Dso { get; set; }
        public TrackPeriodRoute CurrentRoute { get; set; }
        public IEnumerable<TrackAlgorithmItem> Algorithms { get; set; }
        public IEnumerable<TimeUnitItem> TimeUnits { get; set; }
        public TrackPeriodModel TrackForm { get; set; }
        public TrackResults Results { get; set; }
        public string WarningMessage { get; set; }

        #endregion

        #region Methods

        public bool HasResults()
        {
            return Results?.Any() ?? false;
        }

        public bool HasWarning()
        {
            return !string.IsNullOrWhiteSpace(WarningMessage);
        }

        #endregion
    }
}
