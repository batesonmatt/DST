using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DST.Models.DomainModels
{
    public class DsoModel
    {
        #region Properties

        /// <summary>The astronomical catalog of deep-sky objects, to which this object belongs.</summary>
        /// <remarks>This property is part of a composite primary key and foreign key for a table.</remarks>
        /// <value>The name of the catalog or founder.</value>
        [Required]
        public string CatalogName { get; set; }
        public CatalogModel Catalog { get; set; }

        /// <summary>The identification number of this object as it appears in its catalog.</summary>
        /// <remarks>This property is part of a composite primary key for a table.</remarks>
        /// <value>A positive integer greater than <c>0</c>.</value>
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [NotMapped]
        public string CompoundId => $"{CatalogName[0]}{Id}";

        /// <summary>The common name(s) of this object, if any.</summary>
        /// <value>A list of one or more names, delimited by commas.</value>
        /// <remarks>This value defaults to <c>null</c>.</remarks>
        [DefaultValue(null)]
        public string Common
        {
            get
            {
                return _common;
            }

            set
            {
                _common = value;
                _names = _common?.Split(',');
            }
        }

        [NotMapped]
        public string Name => _names?[0];

        [NotMapped]
        public bool HasMultipleNames => _names?.Length > 1;

        [DefaultValue(null)]
        public string Description { get; set; }

        /// <summary>The type of deep-sky object.</summary>
        /// <remarks>This property is a foreign key for a table.</remarks>
        [Required]
        public string Type { get; set; }
        public DsoTypeModel DsoType { get; set; }

        /// <summary>The celestial equivalent of longitude, as represented in decimal hours.</summary>
        /// <value>A floating-point value ranging from <c>0.0</c> to <c>24.0</c>.</value>
        [Range(0.0, 24.0)]
        public double RightAscension { get; set; }

        /// <summary>The celestial equivalent of latitude, as represented in decimal degrees.</summary>
        /// <value>A floating-point value ranging from <c>-90.0</c> to <c>90.0</c>.</value>
        [Range(-90.0, 90.0)]
        public double Declination { get; set; }

        /// <summary>The distance of this object, as represented in kilolight-years (x1000 light-years) from Earth.</summary>
        public double Distance { get; set; }

        /// <summary>The apparent magnitude of this object, for which a lower magnitude resembles a brighter object.</summary>
        /// <remarks>This value defaults to <c>null</c>.</remarks>
        [DefaultValue(null)]
        public double? Magnitude { get; set; }

        /// <summary>The celestial constellation, to which this object belongs.</summary>
        /// <remarks>This property is a foreign key for a table.</remarks>
        /// <value>The name of the constellation.</value>
        [Required]
        public string ConstellationName { get; set; }
        public ConstellationModel Constellation { get; set; }

        #endregion

        #region Fields

        private string _common;
        private string[] _names;

        #endregion

        #region Methods

        public IEnumerable<string> GetOtherNames()
        {
            if (_names?.Length > 1)
            {
                foreach (string name in _names)
                {
                    yield return name;
                }
            }
            else
            {
                yield return null;
            }
        }

        #endregion
    }
}
