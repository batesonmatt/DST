using DST.Models.BusinessLogic;
using DST.Models.DomainModels;
using System;

namespace DST.Models.ViewModels
{
    [Obsolete]
    public class DsoDetailsViewModel
    {
        #region Properties

        public DsoModel Dso { get; set; }
        public DsoDetailsInfo DetailsInfo { get; set; }

        #endregion
    }
}
