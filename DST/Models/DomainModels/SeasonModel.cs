using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DST.Models.DomainModels
{
    public class SeasonModel
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        [Required]
        public string North { get; set; } = string.Empty;

        [Required]
        public string South { get; set; } = string.Empty;

        [Required]
        public int StartMonth { get; set; }

        [Required]
        public int EndMonth { get; set; }

        [NotMapped]
        public string Name => North;

        public ICollection<ConstellationModel> Children { get; set; }

        #endregion

        #region Constructors

        public SeasonModel() => Children = new HashSet<ConstellationModel>();

        #endregion

        #region Methods

        public bool ContainsDate(DateTime dateTime)
        {
            int month = dateTime.Month;

            if (EndMonth < StartMonth)
            {
                return month <= EndMonth || month >= StartMonth;
            }

            return month >= StartMonth && month <= EndMonth;
        }

        #endregion
    }
}
