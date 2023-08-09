using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DST.Models.DomainModels
{
    public class CatalogModel
    {
        #region Properties

        [Key]
        public string Name { get; set; } = string.Empty;

        public ICollection<DsoModel> Children { get; set; }

        #endregion

        #region Constructors

        public CatalogModel() => Children = new HashSet<DsoModel>();

        #endregion
    }
}
