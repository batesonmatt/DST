﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DST.Models.DomainModels
{
    public class ConstellationModel
    {
        #region Properties

        [Key]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int SeasonId { get; set; }
        public SeasonModel Season { get; set; } = null!;

        [Range(0, 90)]
        public int NorthernLatitude { get; set; }

        [Range(0, 90)]
        public int SouthernLatitude { get; set; }

        public ICollection<DsoModel> Children { get; set; }

        #endregion

        #region Constructors

        public ConstellationModel() => Children = new HashSet<DsoModel>();

        #endregion
    }
}
