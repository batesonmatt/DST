using DST.Core.Observer;
using DST.Models.BusinessLogic;
using DST.Models.DomainModels;
using DST.Models.Routes;
using System.Collections.Generic;

namespace DST.Models.ViewModels
{
    /* Use ClientObserver to display Geolocation and RA and DEC so that we can display in DMS format, etc. */
    /* 
     * Display:
     * ideal viewing season (including start and end months)
     * Geolocation, timezone, current datetime, algorithm type, current timekeeper, current local timekeeper, LHA,
     * Trajectory name, local?, visible?, current rise/apex/set datetime and position,
     * current altaz track position
     */
    /* Use Resources for formatting values */
    public class TrackViewModel
    {
        #region Properties

        public GeolocationModel Geolocation { get; set; }
        public IEnumerable<TimeZoneItem> TimeZoneItems { get; set; }

        public DsoModel Dso { get; set; }
        public IObserver ClientObserver { get; set; }

        public TrackSummaryRoute CurrentRoute { get; set; }

        #endregion
    }
}
