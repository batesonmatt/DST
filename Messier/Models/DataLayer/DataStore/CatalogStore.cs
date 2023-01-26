using DST.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DST.Models.DataLayer.DataStore
{
    internal class CatalogStore : IEntityTypeConfiguration<CatalogModel>
    {
        public void Configure(EntityTypeBuilder<CatalogModel> builder)
        {
            builder.HasData(
                new CatalogModel { Name = "Messier" },
                new CatalogModel { Name = "Caldwell" }

                );
        }
    }
}
