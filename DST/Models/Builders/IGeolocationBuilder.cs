using DST.Models.DomainModels;

namespace DST.Models.Builders
{
    public interface IGeolocationBuilder
    {
        GeolocationModel CurrentGeolocation { get; set; }

        void Load();
        void Save();
    }
}