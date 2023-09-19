using DST.Models.Extensions;
using DST.Models.Routes;
using Microsoft.AspNetCore.Http;

namespace DST.Models.Builders
{
    public class SearchBuilder : ISearchBuilder
    {
        #region Properties

        public SearchRoute Route { get; set; }

        #endregion

        #region Fields

        private const string _routeKey = "searchrouteKey";
        private readonly ISession _session;

        #endregion

        #region Constructors

        public SearchBuilder(IHttpContextAccessor context)
        {
            _session = context.HttpContext!.Session;
        }

        #endregion

        #region Methods

        public void Load()
        {
            // Try to load from session state.
            // Create new SearchRoute object if no value is stored.
            Route =
                _session.GetObject<SearchRoute>(_routeKey)
                ?? new SearchRoute();

            // Save the current route to session state.
            Save();
        }

        public void Save()
        {
            switch (Route)
            {
                // If set to null, remove the route from session state.
                case null:
                    _session.Remove(_routeKey);
                    break;

                // Save to session state.
                default:
                    _session.SetObject(_routeKey, Route);
                    break;
            }
        }

        #endregion
    }
}
