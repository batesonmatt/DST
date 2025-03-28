﻿using DST.Models.DataLayer.Query;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DST.Models.DataLayer.Repositories
{
    public interface IRepository<T> 
        where T : class
    {
        #region Properties

        DbSet<T> Items { get; }

        #endregion

        #region Methods

        IEnumerable<T> List(QueryOptions<T> options);

        T Get(params object[] keyValues);

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        void Save();

        #endregion
    }
}
