using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DST.Models.DomainModels
{
    public class DsoModel
    {
        #region Properties

        /// <summary>The unique identity of this <c>DsoModel</c> instance.</summary>
        /// <remarks>This property is a primary key for a table.</remarks>
        [Key]
        public int DsoId { get; set; }

        /// <summary>The astronomical catalog of deep-sky objects, to which this object belongs.</summary>
        /// <remarks>This property is a foreign key for a table.</remarks>
        /// <value>The name of the catalog or founder.</value>
        [Required]
        public string CatalogName { get; set; } = string.Empty;
        public CatalogModel Catalog { get; set; } = null!;

        /// <summary>The identification number of this object as it appears in its catalog.</summary>
        /// <value>A positive integer greater than <c>0</c>.</value>
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        /// <summary>The fully defined catalog identification number of this object.</summary>
        /// <value>The first letter of the catalog name, followed by the identification number.</value>
        [NotMapped]
        public string CompoundId => string.Concat(CatalogName[0], Id);

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

        /// <summary>The number of names associated to this object.</summary>
        /// <value>A positive integer greater than, or equal to, <c>0</c>.</value>
        [NotMapped]
        public int NameCount => _names?.Length ?? 0;

        /// <summary>Attempts to retrieve the first name found for this object, if any.</summary>
        /// <value>A non-empty string, or <c>null</c> if this object has no names.</value>
        [NotMapped]
        public string Name => _names?[0];

        /// <summary>Determines whether there are any names associated to this object.</summary>
        /// <value><c>True</c> if this object has any names, otherwise <c>False</c>.</value>
        [NotMapped]
        public bool HasName => NameCount > 0;

        /// <summary>Determines whether there are multiple names associated to this object.</summary>
        /// <value><c>True</c> if this object has more than <c>1</c> name, otherwise <c>False</c>.</value>
        [NotMapped]
        public bool HasMultipleNames => NameCount > 1;

        /// <summary>The formatted name of this object.</summary>
        /// <value>A concatenation of the fully defined identification number and the common name, if any.</value>
        [NotMapped]
        public string NameHeading
            => HasName ? string.Format(Resources.DisplayText.DsoNameHeadingFormat, CompoundId, Name) : CompoundId;

        /// <summary>The description for this object, if any.</summary>
        /// <value>Supplementary information regarding the specific type of deep-sky object.</value>
        /// <remarks>This value defaults to <c>null</c>.</remarks>
        [DefaultValue(null)]
        public string Description { get; set; }

        /// <summary>The type of deep-sky object.</summary>
        /// <remarks>This property is a foreign key for a table.</remarks>
        [Required]
        public string Type { get; set; } = string.Empty;
        public DsoTypeModel DsoType { get; set; } = null!;

        /// <summary>The celestial equivalent of longitude, as represented in decimal hours.</summary>
        /// <value>A floating-point value ranging from <c>0.0</c> to <c>24.0</c>.</value>
        [Range(0.0, 24.0)]
        public double RightAscension { get; set; }

        /// <summary>The celestial equivalent of latitude, as represented in decimal degrees.</summary>
        /// <value>A floating-point value ranging from <c>-90.0</c> to <c>90.0</c>.</value>
        [Range(-90.0, 90.0)]
        public double Declination { get; set; }

        /// <summary>The distance of this object, as represented in kilolight-years (x1000 light-years) from Earth.</summary>
        /// <value>A number greater than <c>0.0</c>.</value>
        public double Distance { get; set; }

        /// <summary>The apparent magnitude of this object, for which a lower magnitude resembles a brighter object.</summary>
        /// <value>A positive or negative, non-zero value if this object has an apparent magnitude. Otherwise, <c>null</c>.</value>
        /// <remarks>This value defaults to <c>null</c>.</remarks>
        [DefaultValue(null)]
        public double? Magnitude { get; set; }

        /// <summary>The celestial constellation, to which this object belongs.</summary>
        /// <remarks>This property is a foreign key for a table.</remarks>
        /// <value>The name of the constellation.</value>
        [Required]
        public string ConstellationName { get; set; } = string.Empty;
        public ConstellationModel Constellation { get; set; } = null!;

        public DsoImageModel DsoImage { get; set; } = null!;

        #endregion

        #region Fields

        private string _common;
        private string[] _names;

        #endregion

        #region Methods

        public IEnumerable<string> GetOtherNames()
        {
            if (HasMultipleNames)
            {
                foreach (string name in _names.Skip(1))
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
