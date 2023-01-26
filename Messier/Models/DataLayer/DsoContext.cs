﻿using DST.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DST.Models.DataLayer
{
    public class DsoContext : DbContext
    {
        #region Properties

        public DbSet<DsoModel> DsoItems { get; set; }
        public DbSet<ConstellationModel> Constellations { get; set; }
        public DbSet<SeasonModel> Seasons { get; set; }
        public DbSet<DsoTypeModel> DsoTypes { get; set; }
        public DbSet<CatalogModel> Catalogs { get; set; }

        #endregion

        #region Constructors

        public DsoContext(DbContextOptions<DsoContext> options)
            : base(options)
        { }

        #endregion

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // DsoModel: Set composite primary key.
            modelBuilder.Entity<DsoModel>()
                .HasKey(dso => new { dso.CatalogName, dso.Id });

            // DsoModel: Set Catalog foreign key and remove cascading delete with Catalog.
            modelBuilder.Entity<DsoModel>()
                .HasOne(dso => dso.Catalog)
                .WithMany(cat => cat.Children)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(dso => dso.CatalogName);

            // DsoModel: Set Constellation foreign key and remove cascading delete with Constellation.
            modelBuilder.Entity<DsoModel>()
                .HasOne(dso => dso.Constellation)
                .WithMany(con => con.Children)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(dso => dso.ConstellationName);

            // DsoModel: Set DsoType foreign key and remove cascading delete with DsoType.
            modelBuilder.Entity<DsoModel>()
                .HasOne(dso => dso.DsoType)
                .WithMany(type => type.Children)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(dso => dso.Type);

            // ConstellationModel: Set Season foreign key and remove cascading delete with Season.
            modelBuilder.Entity<ConstellationModel>()
                .HasOne(con => con.Season)
                .WithMany(sea => sea.Children)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(con => con.SeasonId);

            // DsoModel: Update property access mode(s).
            modelBuilder.Entity<DsoModel>()
                .Property(dso => dso.Common)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            // Seed initial data
            modelBuilder.ApplyConfiguration(new DataStore.CatalogStore());
            modelBuilder.ApplyConfiguration(new DataStore.DsoTypeStore());
            modelBuilder.ApplyConfiguration(new DataStore.SeasonStore());
            modelBuilder.ApplyConfiguration(new DataStore.ConstellationStore());
            modelBuilder.ApplyConfiguration(new DataStore.DsoStore());
        }

        #endregion
    }
}
