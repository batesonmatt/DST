using DST.Models.DomainModels;
using DST.Models.Extensions;
using Microsoft.AspNetCore.Http;

namespace DST.Models.Builders
{
    public class GeolocationBuilder
    {
        #region Properties

        public GeolocationModel CurrentGeolocation => _currentGeolocation;

        #endregion

        #region Fields

        private const string _sessionKey = "currentgeosession";
        private GeolocationModel _currentGeolocation;
        private readonly ISession _session;

        #endregion

        #region Constructors

        public GeolocationBuilder(ISession session)
        {
            _session = session;
            _currentGeolocation = _session.GetObject<GeolocationModel>(_sessionKey) ?? GeolocationModel.Default;
        }

        public GeolocationBuilder(ISession session, GeolocationModel geolocation)
        {
            _session = session;
            _currentGeolocation = geolocation ?? GeolocationModel.Default;
            Save();
        }

        #endregion

        #region Methods

        public static void SaveGeolocation(ISession session, GeolocationModel geolocation)
        {
            _ = new GeolocationBuilder(session, geolocation);
        }

        public void SaveGeolocation(GeolocationModel geolocation)
        {
            _currentGeolocation = geolocation ?? GeolocationModel.Default;
            Save();
        }

        private void Save()
        {
            _session.SetObject(_sessionKey, _currentGeolocation);
        }

        #endregion
    }
}
