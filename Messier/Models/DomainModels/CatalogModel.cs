using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Messier.Models.DomainModels
{
    public class CatalogModel
    {
        #region Properties

        [Key]
        public string Name { get; set; }

        public ICollection<DsoModel> Children { get; set; }

        #endregion
    }
}
