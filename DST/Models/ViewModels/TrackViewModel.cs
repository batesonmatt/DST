using DST.Core.Observer;
using DST.Models.DomainModels;
using DST.Models.Routes;

namespace DST.Models.ViewModels
{
    /* Display:
     * 
     * Compound Id
     * names
     * 
     *      Target:
     * RA, DEC
     * catalog
     * type
     * description
     * constellation
     * distance
     * magnitude
     * ideal viewing season (including start and end months)
     * 
     *      Observer:
     * Geolocation
     * timezone
     * current local datetime
     * current UTC datetime
     * 
     *      Current Tracking info:
     * Trajectory name
     * current timekeeper
     * current local timekeeper
     * LHA
     * local?
     * visible?
     * current rise/apex/set datetimes and positions
     * current altaz track position
     */
    /* Use Resources for formatting values */
    public class TrackViewModel
    {
        #region Properties

        public DsoModel Dso { get; set; }
        public ILocalObserver ClientObserver { get; set; }
        public TrackSummaryRoute CurrentRoute { get; set; }

        #endregion
    }
}
