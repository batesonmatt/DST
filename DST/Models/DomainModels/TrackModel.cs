using DST.Models.BusinessLogic;
using DST.Models.Validation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace DST.Models.DomainModels
{
    public class TrackModel
    {
        #region Properties

        public string Algorithm { get; set; } = AlgorithmName.Default;

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resources.DisplayText), ErrorMessageResourceName = "TrackValidationStartDate")]
        [Remote("ValidateStartDate", "Validation")]
        [EpochDateRange]
        public DateTime Start { get; set; }

        public bool IsTrackOnce { get; set; } = false;

        public bool IsReady { get; set; } = false;

        #endregion

        #region Methods

        public long GetTicks()
        {
            return Start.Ticks;
        }

        public string GetFormattedStart()
        {
            return Start.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        #endregion
    }
}
