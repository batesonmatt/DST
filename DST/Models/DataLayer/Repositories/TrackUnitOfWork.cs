using DST.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DST.Models.DataLayer.Repositories
{
    /// <summary>
    /// A unit of work providing repositories for a read-only MainDbContext.
    /// </summary>
    public class TrackUnitOfWork
    {
        #region Properties

        public Repository<DsoModel> DsoItems
        {
            get
            {
                _dsoData ??= new Repository<DsoModel>(_context);

                return _dsoData;
            }
        }

        #endregion

        #region Fields

        private readonly MainDbContext _context;
        private Repository<DsoModel> _dsoData;

        #endregion

        #region Constructors

        public TrackUnitOfWork(MainDbContext context)
            => _context = context;

        #endregion

        public SeasonModel GetSeason(DsoModel dso)
        {
            if (dso is null || _context is null)
            {
                return null;
            }

            SeasonModel season;

            try
            {
                season = _context.DsoItems
                    .Where(d => d == dso)
                    .Include(i => i.Constellation)
                    .ThenInclude(i => i.Season)
                    .Select(d => d.Constellation.Season)
                    .FirstOrDefault();
            }
            catch
            {
                season = null;
            }

            return season;
        }
    }
}
