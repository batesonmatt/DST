using DST.Models.DataLayer.Query;
using System;
using System.ComponentModel.DataAnnotations;

namespace DST.Models.DomainModels
{
    public class TrackPhaseModel : TrackModel
    {
        #region Properties

        public string Phase { get; set; } = PhaseName.Default;

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resources.DisplayText), ErrorMessageResourceName = "TrackValidationCycles")]
        [Range(-100, 100, ErrorMessageResourceType = typeof(Resources.DisplayText), ErrorMessageResourceName = "TrackValidationCyclesRange")]
        public int Cycles { get; set; } = 0;

        #endregion
    }
}
