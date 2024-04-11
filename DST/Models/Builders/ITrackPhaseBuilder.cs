using DST.Models.DomainModels;

namespace DST.Models.Builders
{
    public interface ITrackPhaseBuilder : IBuilder
    {
        TrackPhaseModel Current { get; set; }
    }
}
