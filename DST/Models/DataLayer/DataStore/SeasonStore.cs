using DST.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DST.Models.DataLayer.DataStore
{
    internal class SeasonStore : IEntityTypeConfiguration<SeasonModel>
    {
        public void Configure(EntityTypeBuilder<SeasonModel> builder)
        {
            builder.HasData(
                new SeasonModel { Id = 1, North = "Spring", South = "Autumn", StartMonth = 3, EndMonth = 5 },
                new SeasonModel { Id = 2, North = "Summer", South = "Winter", StartMonth = 6, EndMonth = 8 },
                new SeasonModel { Id = 3, North = "Autumn", South = "Spring", StartMonth = 9, EndMonth = 11 },
                new SeasonModel { Id = 4, North = "Winter", South = "Summer", StartMonth = 12, EndMonth = 2 }

                );
        }
    }
}
