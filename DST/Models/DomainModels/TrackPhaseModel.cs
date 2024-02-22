using DST.Models.DataLayer.Query;
using DST.Models.Validation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace DST.Models.DomainModels
{
    public class TrackPhaseModel
    {
        #region Properties

        public string Algorithm { get; set; } = AlgorithmName.Default;

        public string Phase { get; set; } = PhaseName.Default;

        [Required(ErrorMessage = "Please enter the start date.")]
        [Remote("ValidateStartDate", "Validation")]
        [EpochDateRange]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "Please enter the number of cycles.")]
        [Range(-100, 100, ErrorMessage = "The number of cycles must be between -100 and 100.")]
        public int Cycles { get; set; } = 0;

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
