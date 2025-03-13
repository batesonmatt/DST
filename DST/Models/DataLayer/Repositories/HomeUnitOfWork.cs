using DST.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DST.Models.DataLayer.Repositories
{
    /// <summary>
    /// A unit of work providing repositories for a read-only MainDbContext.
    /// </summary>
    public class HomeUnitOfWork : IHomeUnitOfWork
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

        #region "Constructors"

        public HomeUnitOfWork(MainDbContext context)
            => _context = context;

        #endregion

        #region "Methods"

        public IEnumerable<DsoModel> GetRandomDsoItems(int count)
        {
            IEnumerable<DsoModel> items;

            try
            {
                items = _context.DsoItems
                    .Include(dso => dso.DsoImage)
                    .OrderBy(dso => Guid.NewGuid())
                    .Take(count);
            }
            catch
            {
                items = null;
            }

            return items;
        }

        #endregion
    }
}
