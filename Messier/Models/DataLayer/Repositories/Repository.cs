using Messier.Models.DataLayer.Query;
using Messier.Models.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Messier.Models.DataLayer.Repositories
{
    public class Repository<T> : IRepository<T> 
        where T : class
    {
        #region Properties

        public int Count => _count ?? _dbset.Count();

        // Access the underlying model instances associated with this repository.
        // Allows querying their methods and non-mapped properties.
        public List<T> Table => _dbset?.ToList();

        #endregion

        #region Fields
        
        protected DsoContext _context;

        private readonly DbSet<T> _dbset;

        private int? _count;

        #endregion

        #region Constructors

        public Repository(DsoContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        #endregion

        #region Methods

        private IQueryable<T> BuildQuery(QueryOptions<T> options)
        {
            IQueryable<T> query = Table.AsQueryable();

            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }

            if (options.HasWhere)
            {
                foreach (var clause in options.WhereAll)
                {
                    query = query.Where(clause);
                }

                _count = query.Count();
            }

            if (options.HasOrderBy)
            {
                foreach (var clause in options.OrderByAll)
                {
                    if (options.SortDirection == OrderDirection.Ascending)
                    {
                        query = query.AppendOrderBy(clause);
                    }
                    else
                    {
                        query = query.AppendOrderByDescending(clause);
                    }
                }
            }

            if (options.HasPaging)
            {
                query = query.PageBy(options.PageNumber, options.PageSize);
            }

            return query;
        }

        public virtual T Get(QueryOptions<T> options)
        {
            IQueryable<T> query = BuildQuery(options);

            return query.FirstOrDefault();
        }

        #endregion

        #region IRepository<T> Members

        public virtual IEnumerable<T> List(QueryOptions<T> options)
        {
            IQueryable<T> query = BuildQuery(options);

            return query.ToList();
        }

        public virtual T Get(params object[] keyValues) => _dbset.Find(keyValues);

        public virtual void Insert(T entity) => _dbset.Add(entity);

        public virtual void Update(T entity) => _dbset.Update(entity);

        public virtual void Delete(T entity) => _dbset.Remove(entity);

        public virtual void Save() => _context.SaveChanges();

        #endregion
    }
}
