using DST.Models.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DST.Models.DomainModels
{
    public class TrackPeriodModel : TrackModel
    {
        #region Properties

        public bool IsFixed { get; set; } = false;

        // If set to true, tracking will use Aggregated interval calculation.
        // Each date is determined by calculating the number of days since the start date 
        // in Mean Solar Time for the selected time unit, which is then represented in 
        // whole days of the time scale for the selected timekeeping algorithm.
        //
        // If set to false, tracking will use Successive interval calculation.
        // Each date is determined by calculating the number of days since the previous date 
        // in Mean Solar time for the selected time unit, which is then represented in 
        // whole days of the time scale for the selected timekeeping algorithm.
        // This more accurately depicts the length of each interval in the underlying time scale,
        // but might be less intuitive for some people.
        public bool IsAggregated { get; set; } = true;

        public string TimeUnit { get; set; } = TimeUnitName.Default;

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resources.DisplayText), ErrorMessageResourceName = "TrackValidationPeriod")]
        [Remote("ValidatePeriod", "Validation", AdditionalFields = "Algorithm, TimeUnit, IsFixed")]
        public int Period { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resources.DisplayText), ErrorMessageResourceName = "TrackValidationInterval")]
        [Remote("ValidateInterval", "Validation", AdditionalFields = "Period")]
        public int Interval { get; set; }

        #endregion
    }
}
