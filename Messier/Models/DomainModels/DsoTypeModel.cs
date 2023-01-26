using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DST.Models.DomainModels
{
    public class DsoTypeModel
    {
        #region Properties

        /// <summary>The type of deep-sky object.</summary>
        /// <remarks>This property is a primary key for a table.</remarks>
        [Key]
        public string Type { get; set; }

        public ICollection<DsoModel> Children { get; set; }

        #endregion
    }
}
