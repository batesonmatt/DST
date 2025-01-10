using DST.Models.Extensions;
using DST.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DST.Components
{
    public class GeolocationFormToggleViewComponent : ViewComponent
    {
        #region Constructors

        public GeolocationFormToggleViewComponent() { }

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

            return View("~/Views/Shared/_GeolocationFormTogglePartial.cshtml", viewModel);
        }

        #endregion
    }
}
