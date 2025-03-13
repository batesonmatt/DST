using DST.Models.DomainModels;
using System.Collections.Generic;

namespace DST.Models.DataLayer.Repositories
{
    public interface IHomeUnitOfWork
    {
        Repository<DsoModel> DsoItems { get; }

        IEnumerable<DsoModel> GetRandomDsoItems(int count);
    }
}
