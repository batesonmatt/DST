using Messier.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messier.Models.DataLayer.DataStore
{
    internal class DsoTypeStore : IEntityTypeConfiguration<DsoTypeModel>
    {
        public void Configure(EntityTypeBuilder<DsoTypeModel> builder)
        {
            builder.HasData(
                new DsoTypeModel { Type = "Asterism" },
                new DsoTypeModel { Type = "Double star" },
                new DsoTypeModel { Type = "Galaxy" },
                new DsoTypeModel { Type = "Nebula" },
                new DsoTypeModel { Type = "Star cloud" },
                new DsoTypeModel { Type = "Star cluster" }

                );
        }
    }
}
