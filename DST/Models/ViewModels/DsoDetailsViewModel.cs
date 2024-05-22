using DST.Models.BusinessLogic;
using DST.Models.DomainModels;

namespace DST.Models.ViewModels
{
    public class DsoDetailsViewModel
    {
        #region Properties

        public DsoModel Dso { get; set; }
        public DsoDetailsInfo DetailsInfo { get; set; }

        #endregion
    }
}
