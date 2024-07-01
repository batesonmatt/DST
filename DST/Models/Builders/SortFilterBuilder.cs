using DST.Models.DomainModels;
using DST.Models.Extensions;
using Microsoft.AspNetCore.Http;

namespace DST.Models.Builders
{
    public class SortFilterBuilder : ISortFilterBuilder
    {
        #region Properties

        public SortFilterModel CurrentSortFilter { get; set; }

        #endregion

        #region Fields

        private const string _sortFilterKey = "sortFilterKey";
        private readonly ISession _session;

        #endregion

        #region Constructors

        public SortFilterBuilder(IHttpContextAccessor context)
        {
            _session = context.HttpContext!.Session;
        }

        #endregion

        #region Methods

        public void Load()
        {
            // Try to load from session state.
            // Create new object if no value is stored.
            CurrentSortFilter = _session.GetObject<SortFilterModel>(_sortFilterKey) ?? new SortFilterModel();

            // Save the current object to session state.
            Save();
        }

        public void Save()
        {
            switch (CurrentSortFilter)
            {
                // If set to null, remove the object from session state.
                case null:
                    _session.Remove(_sortFilterKey);
                    break;

                // Save to session state.
                default:
                    _session.SetObject(_sortFilterKey, CurrentSortFilter);
                    break;
            }
        }

        #endregion
    }
}
