using DST.Models.DomainModels;

namespace DST.Models.Builders
{
    public interface IGeolocationBuilder : IBuilder
    {
        GeolocationModel CurrentGeolocation { get; set; }
    }
}