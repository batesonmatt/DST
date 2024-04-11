using DST.Models.DataLayer.Query;
using System;
using System.ComponentModel.DataAnnotations;

namespace DST.Models.DomainModels
{
    public class TrackPhaseModel : TrackModel
    {
        #region Properties

        public string Phase { get; set; } = PhaseName.Default;

        [Required(ErrorMessage = "Please enter the number of cycles.")]
        [Range(-100, 100, ErrorMessage = "The number of cycles must be between -100 and 100.")]
        public int Cycles { get; set; } = 0;

        #endregion
    }
}
