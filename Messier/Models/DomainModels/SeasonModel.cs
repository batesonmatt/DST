using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DST.Models.DomainModels
{
    public class SeasonModel
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        [Required]
        public string North { get; set; }

        [Required]
        public string South { get; set; }

        public ICollection<ConstellationModel> Children { get; set; }

        #endregion
    }
}
