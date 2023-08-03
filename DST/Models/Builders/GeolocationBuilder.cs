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
        private readonly GeolocationModel _currentGeolocation;
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
        }

        #endregion

        #region Methods

        public void SaveGeolocation()
        {
            _session.SetObject(_sessionKey, _currentGeolocation);
        }

        #endregion
    }
}
