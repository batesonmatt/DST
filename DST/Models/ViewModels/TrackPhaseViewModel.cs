﻿using DST.Models.BusinessLogic;
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
        public IEnumerable<SelectListItem> Algorithms { get; set; }
        public IEnumerable<SelectListItem> CoordinateFormats { get; set; }
        public IEnumerable<SelectListItem> Phases { get; set; }
        public TrackPhaseModel TrackForm { get; set; }
        public PhaseResultsViewModel ResultsModel { get; set; }
        public AlertMessage Alert { get; set; }

        #endregion

        #region Methods

        public bool HasResults()
        {
            if (ResultsModel is not null && ResultsModel.Results is not null)
            {
                if (ResultsModel.Results.Any())
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsTrackingSupported()
        {
            return Phases?.Any() ?? false;
        }

        public bool HasMessage()
        {
            return Alert is not null && !string.IsNullOrWhiteSpace(Alert.Message);
        }

        #endregion
    }
}
