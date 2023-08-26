using DST.Models.DomainModels;

namespace DST.Models.BusinessLogic
{
    public class DsoObserverOptions
    {
        public DsoModel Dso { get; } = null!;
        public GeolocationModel Geolocation { get; } = null!;

        public DsoObserverOptions(DsoModel dso)
        {
            Dso = dso;
        }

        public DsoObserverOptions(DsoModel dso, GeolocationModel geolocation)
        {
            Dso = dso;
            Geolocation = geolocation;
        }
    }
}
