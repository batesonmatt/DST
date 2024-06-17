using DST.Models.DomainModels;

namespace DST.Models.DataLayer.Repositories
{
    public interface ITrackUnitOfWork
    {
        Repository<DsoModel> DsoItems { get; }

        ConstellationModel GetConstellation(DsoModel dso);
        SeasonModel GetSeason(DsoModel dso);
    }
}