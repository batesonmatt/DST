using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DST.Models.DomainModels
{
    public class DsoImageModel
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [DefaultValue(null)]
        public string ImageSource { get; set; }

        [DefaultValue(null)]
        public string Author { get; set; }

        [DefaultValue(null)]
        public string License { get; set; }

        [DefaultValue(null)]
        public string LicenseSource { get; set; }

        [DefaultValue(null)]
        public string Provider { get; set; }

        [Required]
        public string DisplayPath { get; set; } = string.Empty;

        [Required]
        public string ThumbPath { get; set; } = string.Empty;

        [Required]
        public int DsoId { get; set; }
        public DsoModel Dso { get; set; } = null!;

        [NotMapped]
        public string RelativeDisplayPath => string.Concat("../", DisplayPath);

        [NotMapped]
        public string RelativeThumbPath => string.Concat("../", ThumbPath);

        #endregion
    }
}
