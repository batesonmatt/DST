using DST.Models.BusinessLogic;
using DST.Models.DomainModels;
using DST.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DST.Components
{
    [Obsolete]
    public class DsoDetailsViewComponent : ViewComponent
    {
        #region Constructors

        public DsoDetailsViewComponent() { }

        #endregion

        #region Methods

        public IViewComponentResult Invoke(DsoModel dso)
        {
            DsoDetailsViewModel viewModel = new()
            {
                Dso = dso,
                DetailsInfo = Utilities.GetDetailsInfo(dso)
            };

            return View("~/Views/Shared/_DsoDetailsPartial.cshtml", viewModel);
        }

        #endregion
    }
}
