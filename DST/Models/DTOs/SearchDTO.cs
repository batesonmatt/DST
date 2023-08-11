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

        public string Local { get; set; } = ToggleFilter.Off;

        public string HasName { get; set; } = ToggleFilter.Off;

        public string Visibility { get; set; } = ToggleFilter.Off;

        public string RiseTime { get; set; } = ToggleFilter.Off;

        public string Trajectory { get; set; } = ListFilter.All;

        #endregion

        #region Methods

        public IFilter[] GetFilters()
            => new IFilter[]
            {
                new ListFilter(nameof(Type), Type),
                new ListFilter(nameof(Catalog), Catalog),
                new ListFilter(nameof(Constellation), Constellation),
                new ListFilter(nameof(Season), Season),
                new ToggleFilter(nameof(Local), Local),
                new ToggleFilter(nameof(HasName), HasName),
                new ToggleFilter(nameof(Visibility), Visibility),
                new ToggleFilter(nameof(RiseTime), RiseTime),
                new ListFilter(nameof(Trajectory), Trajectory)
            };

        #endregion

    }
}
