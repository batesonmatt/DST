using DST.Models.DataLayer.Query;
using DST.Models.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DST.Models.DataLayer.Repositories
{
    public class Repository<T> : IRepository<T> 
        where T : class
    {
        #region Properties

        public int Count => _count ?? _dbset.Count();

        #endregion

        #region Fields
        
        protected MainDbContext _context;

        private readonly DbSet<T> _dbset;

        private int? _count;

        #endregion

        #region Constructors

        public Repository(MainDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        #endregion

        #region Methods

        private IQueryable<T> BuildQuery(QueryOptions<T> options)
        {
            IQueryable<T> query = _dbset;

            // Include entity navigation properties to allow eager loading related entities.
            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            
            // Call ToList() to allow querying the T instance methods and non-mapped properties.
            query = query.ToList().AsQueryable();

            if (options.HasWhere)
            {
                foreach (var clause in options.WhereAll)
                {
                    query = query.Where(clause);
                }

                // Calculate the new count after filtering.
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

        public virtual T Get(QueryOptions<T> options) => BuildQuery(options).FirstOrDefault();

        #endregion

        #region IRepository<T> Members

        public virtual IEnumerable<T> List(QueryOptions<T> options) => BuildQuery(options).ToList();

        public virtual T Get(params object[] keyValues) => _dbset.Find(keyValues);

        public virtual void Insert(T entity) => _dbset.Add(entity);

        public virtual void Update(T entity) => _dbset.Update(entity);

        public virtual void Delete(T entity) => _dbset.Remove(entity);

        public virtual void Save() => _context.SaveChanges();

        #endregion
    }
}
