using DST.Models.DataLayer.Query;
using Newtonsoft.Json;

namespace DST.Models.DTOs
{
    public class SearchDTO : PageSortDTO
    {
        #region Properties

        public string Type { get; set; }

        public string Catalog { get; set; }

        public string Constellation { get; set; }

        public string Season { get; set; }

        public string Local { get; set; }

        public string HasName { get; set; }

        public string Visibility { get; set; }

        public string RiseTime { get; set; }

        public string Trajectory { get; set; }

        [JsonIgnore]
        public IFilter[] Filters
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
