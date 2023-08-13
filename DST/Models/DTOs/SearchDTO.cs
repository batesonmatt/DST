using DST.Models.DataLayer.Query;

namespace DST.Models.DTOs
{
    public class SearchDTO : PageSortDTO
    {
        #region Properties

        public string Type { get; set; } = ListFilter.All;

        public string Catalog { get; set; } = ListFilter.All;

        public string Constellation { get; set; } = ListFilter.All;

        public string Season { get; set; } = ListFilter.All;

        public string Trajectory { get; set; } = ListFilter.All;

        public string Local { get; set; } = ToggleFilter.Off;

        public string Visible { get; set; } = ToggleFilter.Off;

        public string Rising { get; set; } = ToggleFilter.Off;

        public string HasName { get; set; } = ToggleFilter.Off;

        #endregion

        #region Methods

        public IFilter[] GetFilters()
            => new IFilter[]
            {
                new ListFilter(nameof(Type), Type),
                new ListFilter(nameof(Catalog), Catalog),
                new ListFilter(nameof(Constellation), Constellation),
                new ListFilter(nameof(Season), Season),
                new ListFilter(nameof(Trajectory), Trajectory),
                new ToggleFilter(nameof(Local), Local),
                new ToggleFilter(nameof(Visible), Visible),
                new ToggleFilter(nameof(Rising), Rising),
                new ToggleFilter(nameof(HasName), HasName)
            };

        #endregion

    }
}
