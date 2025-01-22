namespace DST.Models.ViewModels
{
    public class PeriodResultsViewModel : TrackResultsViewModel
    {
        #region Properties

        public string TimeUnit { get; set; }
        public string Period { get; set; }
        public string Interval { get; set; }
        public string Fixed { get; set; }
        public string Aggregated { get; set; }

        #endregion
    }
}
