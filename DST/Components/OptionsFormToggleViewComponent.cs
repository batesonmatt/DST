using DST.Models.Extensions;
using DST.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DST.Components
{
    public class OptionsFormToggleViewComponent : ViewComponent
    {
        #region Constructors

        public OptionsFormToggleViewComponent() { }

        #endregion

        #region Methods

        public IViewComponentResult Invoke(string formId, bool enabled = true)
        {
            if (string.IsNullOrWhiteSpace(formId))
            {
                formId = string.Empty;
            }

            FormToggleViewModel viewModel = new()
            {
                FormId = formId,
                EnabledState = enabled.Disabled()
            };

            return View("~/Views/Shared/_OptionsFormTogglePartial.cshtml", viewModel);
        }

        #endregion
    }
}
