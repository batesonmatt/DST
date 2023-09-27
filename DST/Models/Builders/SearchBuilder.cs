using DST.Models.DomainModels;
using DST.Models.Extensions;
using Microsoft.AspNetCore.Http;

namespace DST.Models.Builders
{
    public class SearchBuilder : ISearchBuilder
    {
        #region Properties

        public SearchModel CurrentSearch { get; set; } = null!;

        #endregion

        #region Fields

        private const string _searchKey = "searchKey";
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
            // Create new object if no value is stored.
            CurrentSearch =
                _session.GetObject<SearchModel>(_searchKey)
                ?? new SearchModel();

            // Save the current object to session state.
            Save();
        }

        public void Save()
        {
            switch (CurrentSearch)
            {
                // If set to null, remove the object from session state.
                case null:
                    _session.Remove(_searchKey);
                    break;

                // Save to session state.
                default:
                    _session.SetObject(_searchKey, CurrentSearch);
                    break;
            }
        }

        #endregion
    }
}
