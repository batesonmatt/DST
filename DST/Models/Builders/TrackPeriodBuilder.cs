using DST.Models.DomainModels;
using DST.Models.Extensions;
using Microsoft.AspNetCore.Http;

namespace DST.Models.Builders
{
    public class TrackPeriodBuilder : ITrackPeriodBuilder
    {
        #region Properties

        public TrackPeriodModel Current { get; set; }

        #endregion

        #region Fields

        private const string _periodKey = "periodKey";
        private readonly ISession _session;

        #endregion

        #region Constructors

        public TrackPeriodBuilder(IHttpContextAccessor context)
        {
            _session = context.HttpContext!.Session;
        }

        #endregion

        #region Methods

        public void Load()
        {
            // Try to load from session state.
            // Create new object if no value is stored.
            Current = _session.GetObject<TrackPeriodModel>(_periodKey) ?? new TrackPeriodModel();

            // Save the current object to session state.
            Save();
        }

        public void Save()
        {
            switch (Current)
            {
                // If set to null, remove the object from session state.
                case null:
                    _session.Remove(_periodKey);
                    break;

                // Save to session state.
                default:
                    _session.SetObject(_periodKey, Current);
                    break;
            }
        }

        #endregion
    }
}
