using DST.Models.DomainModels;

namespace DST.Models.Builders
{
    public interface ITrackPeriodBuilder : IBuilder
    {
        TrackPeriodModel Current { get; set; }
    }
}
