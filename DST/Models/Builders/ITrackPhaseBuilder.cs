using DST.Models.DomainModels;

namespace DST.Models.Builders
{
    public interface ITrackPhaseBuilder : IBuilder
    {
        public TrackPhaseModel Current { get; set; }
    }
}
