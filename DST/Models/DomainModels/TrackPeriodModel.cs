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

        [Required(ErrorMessage = "Please enter the period length.")]
        [Remote("ValidatePeriod", "Validation", AdditionalFields = "Algorithm, TimeUnit, IsFixed")]
        public int Period { get; set; }

        [Required(ErrorMessage = "Please enter the number of intervals.")]
        [Remote("ValidateInterval", "Validation", AdditionalFields = "Period")]
        public int Interval { get; set; }

        #endregion
    }
}
