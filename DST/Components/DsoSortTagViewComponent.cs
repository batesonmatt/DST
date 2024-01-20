using DST.Models.Builders;
using DST.Models.BusinessLogic;
using DST.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace DST.Components
{
    public class DsoSortTagViewComponent : ViewComponent
    {
        #region Fields

        private readonly IGeolocationBuilder _geoBuilder;

        #endregion

        #region Constructors

        public DsoSortTagViewComponent(IGeolocationBuilder geoBuilder)
        {
            _geoBuilder = geoBuilder;

            // Load the client geolocation, if any.
            _geoBuilder.Load();
        }

        #endregion

        #region Methods

        public IViewComponentResult Invoke(DsoModel dso, string sortField)
        {
            DsoObserverOptions options = new(dso, _geoBuilder.CurrentGeolocation);

            string sortTag = Utilities.GetSortTag(options, sortField);

            return View("~/Views/Shared/_DsoSortTagPartial.cshtml", sortTag);
        }

        #endregion
    }
}
