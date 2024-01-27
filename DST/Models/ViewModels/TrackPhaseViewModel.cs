﻿using DST.Core.Observer;
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
        public ILocalObserver ClientObserver { get; set; }
        public TrackPhaseRoute CurrentRoute { get; set; }
        public TrackPhaseModel PhaseModel { get; set; }
        public IEnumerable<TrackPhaseItem> Phases { get; set; }

        #endregion
    }
}
