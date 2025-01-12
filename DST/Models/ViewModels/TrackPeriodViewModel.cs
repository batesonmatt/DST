using DST.Models.BusinessLogic;
using DST.Models.DomainModels;
using DST.Models.Routes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace DST.Models.ViewModels
{
    public class TrackPeriodViewModel
    {
        #region Properties

        public DsoModel Dso { get; set; }
        public TrackPeriodRoute CurrentRoute { get; set; }
        public IEnumerable<SelectListItem> Algorithms { get; set; }
        public IEnumerable<SelectListItem> TimeUnits { get; set; }
        public TrackPeriodModel TrackForm { get; set; }
        public TrackResults Results { get; set; }
        public AlertMessage Alert { get; set; }

        #endregion

        #region Methods

        public bool HasResults()
        {
            return Results?.Any() ?? false;
        }

        public bool HasMessage()
        {
            return Alert is not null && !string.IsNullOrWhiteSpace(Alert.Message);
        }

        #endregion
    }
}
