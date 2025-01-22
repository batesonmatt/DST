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

        public string CoordinateFormat { get; set; } = CoordinateFormatName.Default;

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

        public string GetSortableStart()
        {
            return Start.ToString("s");
        }

        public string GetFullStart()
        {
            return Start.ToString("F");
        }

        #endregion
    }
}
