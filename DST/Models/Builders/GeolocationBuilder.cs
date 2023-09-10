using DST.Models.DomainModels;
using DST.Models.Extensions;
using Microsoft.AspNetCore.Http;

namespace DST.Models.Builders
{
    public class GeolocationBuilder : IGeolocationBuilder
    {
        #region Properties

        public GeolocationModel CurrentGeolocation { get; set; } = null!;

        #endregion

        #region Fields

        private const string _geolocationKey = "geolocationKey";
        private readonly ISession _session;
        private readonly IRequestCookieCollection _requestCookies;
        private readonly IResponseCookies _responseCookies;

        #endregion

        #region Constructors

        public GeolocationBuilder(IHttpContextAccessor context)
        {
            _session = context.HttpContext!.Session;
            _requestCookies = context.HttpContext!.Request.Cookies;
            _responseCookies = context.HttpContext!.Response.Cookies;
        }

        #endregion

        #region Methods

        public void Load()
        {
            // Try to load from session state, or persistent cookie, if possible.
            // Use the default geolocation if no value is stored.
            CurrentGeolocation =
                _session.GetObject<GeolocationModel>(_geolocationKey)
                ?? _requestCookies.GetObject<GeolocationModel>(_geolocationKey)
                ?? GeolocationModel.Default;

            // Save the current geolocation to session state and create a persistent cookie.
            Save();
        }

        public void Save()
        {
            switch (CurrentGeolocation)
            {
                // If set to null, remove the geolocation from session state and delete the persistent cookie.
                case null:
                    _session.Remove(_geolocationKey);
                    _responseCookies.Delete(_geolocationKey);
                    break;

                // Save to session state and create a persistent cookie.
                default:
                    _session.SetObject(_geolocationKey, CurrentGeolocation);
                    _responseCookies.SetObject(_geolocationKey, CurrentGeolocation);
                    break;
            }
        }

        #endregion
    }
}
