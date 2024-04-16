using DST.Models.DataLayer.Query;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DST.Models.DomainModels
{
    public class TrackPeriodModel : TrackModel
    {
        #region Properties

        public bool IsFixed { get; set; } = false;

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
