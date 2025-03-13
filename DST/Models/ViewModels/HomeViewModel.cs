using DST.Models.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace DST.Models.ViewModels
{
    public class HomeViewModel
    {
        #region "Properties"

        public IEnumerable<DsoModel> DsoItems { get; set; }
        public int DsoCount => DsoItems?.Count() ?? 0;

        #endregion
    }
}
