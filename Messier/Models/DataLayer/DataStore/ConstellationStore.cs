﻿using Messier.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messier.Models.DataLayer.DataStore
{
    internal class ConstellationStore : IEntityTypeConfiguration<ConstellationModel>
    {
        public void Configure(EntityTypeBuilder<ConstellationModel> builder)
        {
            builder.HasData(
                new ConstellationModel { Name = "Andromeda", SeasonId = 3, NorthernLatitude = 90, SouthernLatitude = 40 },
                new ConstellationModel { Name = "Antlia", SeasonId = 1, NorthernLatitude = 45, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Apus", SeasonId = 2, NorthernLatitude = 5, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Aquarius", SeasonId = 3, NorthernLatitude = 65, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Aquila", SeasonId = 2, NorthernLatitude = 90, SouthernLatitude = 75 },
                new ConstellationModel { Name = "Ara", SeasonId = 2, NorthernLatitude = 25, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Aries", SeasonId = 3, NorthernLatitude = 90, SouthernLatitude = 60 },
                new ConstellationModel { Name = "Auriga", SeasonId = 4, NorthernLatitude = 90, SouthernLatitude = 40 },
                new ConstellationModel { Name = "Boötes", SeasonId = 1, NorthernLatitude = 90, SouthernLatitude = 50 },
                new ConstellationModel { Name = "Caelum", SeasonId = 4, NorthernLatitude = 40, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Camelopardalis", SeasonId = 4, NorthernLatitude = 90, SouthernLatitude = 10 },
                new ConstellationModel { Name = "Cancer", SeasonId = 1, NorthernLatitude = 90, SouthernLatitude = 60 },
                new ConstellationModel { Name = "Canes Venatici", SeasonId = 1, NorthernLatitude = 90, SouthernLatitude = 40 },
                new ConstellationModel { Name = "Canis Major", SeasonId = 4, NorthernLatitude = 60, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Canis Minor", SeasonId = 4, NorthernLatitude = 90, SouthernLatitude = 75 },
                new ConstellationModel { Name = "Capricornus", SeasonId = 2, NorthernLatitude = 60, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Carina", SeasonId = 4, NorthernLatitude = 20, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Cassiopeia", SeasonId = 3, NorthernLatitude = 90, SouthernLatitude = 20 },
                new ConstellationModel { Name = "Centaurus", SeasonId = 1, NorthernLatitude = 25, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Cepheus", SeasonId = 3, NorthernLatitude = 90, SouthernLatitude = 10 },
                new ConstellationModel { Name = "Cetus", SeasonId = 3, NorthernLatitude = 70, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Chamaeleon", SeasonId = 1, NorthernLatitude = 0, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Circinus", SeasonId = 2, NorthernLatitude = 30, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Columba", SeasonId = 4, NorthernLatitude = 45, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Coma Berenices", SeasonId = 1, NorthernLatitude = 90, SouthernLatitude = 70 },
                new ConstellationModel { Name = "Corona Australis", SeasonId = 2, NorthernLatitude = 40, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Corona Borealis", SeasonId = 2, NorthernLatitude = 90, SouthernLatitude = 50 },
                new ConstellationModel { Name = "Corvus", SeasonId = 1, NorthernLatitude = 60, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Crater", SeasonId = 1, NorthernLatitude = 65, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Crux", SeasonId = 1, NorthernLatitude = 20, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Cygnus", SeasonId = 2, NorthernLatitude = 90, SouthernLatitude = 40 },
                new ConstellationModel { Name = "Delphinus", SeasonId = 2, NorthernLatitude = 90, SouthernLatitude = 70 },
                new ConstellationModel { Name = "Dorado", SeasonId = 4, NorthernLatitude = 20, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Draco", SeasonId = 2, NorthernLatitude = 90, SouthernLatitude = 15 },
                new ConstellationModel { Name = "Equuleus", SeasonId = 2, NorthernLatitude = 90, SouthernLatitude = 80 },
                new ConstellationModel { Name = "Eridanus", SeasonId = 4, NorthernLatitude = 32, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Fornax", SeasonId = 4, NorthernLatitude = 50, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Gemini", SeasonId = 4, NorthernLatitude = 90, SouthernLatitude = 60 },
                new ConstellationModel { Name = "Grus", SeasonId = 3, NorthernLatitude = 34, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Hercules", SeasonId = 2, NorthernLatitude = 90, SouthernLatitude = 50 },
                new ConstellationModel { Name = "Horologium", SeasonId = 4, NorthernLatitude = 30, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Hydra", SeasonId = 1, NorthernLatitude = 54, SouthernLatitude = 83 },
                new ConstellationModel { Name = "Hydrus", SeasonId = 4, NorthernLatitude = 8, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Indus", SeasonId = 2, NorthernLatitude = 15, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Lacerta", SeasonId = 3, NorthernLatitude = 90, SouthernLatitude = 40 },
                new ConstellationModel { Name = "Leo", SeasonId = 1, NorthernLatitude = 90, SouthernLatitude = 65 },
                new ConstellationModel { Name = "Leo Minor", SeasonId = 1, NorthernLatitude = 90, SouthernLatitude = 45 },
                new ConstellationModel { Name = "Lepus", SeasonId = 4, NorthernLatitude = 63, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Libra", SeasonId = 2, NorthernLatitude = 65, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Lupus", SeasonId = 1, NorthernLatitude = 35, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Lynx", SeasonId = 1, NorthernLatitude = 90, SouthernLatitude = 55 },
                new ConstellationModel { Name = "Lyra", SeasonId = 2, NorthernLatitude = 90, SouthernLatitude = 40 },
                new ConstellationModel { Name = "Mensa", SeasonId = 4, NorthernLatitude = 4, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Microscopium", SeasonId = 2, NorthernLatitude = 45, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Monoceros", SeasonId = 4, NorthernLatitude = 75, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Musca", SeasonId = 1, NorthernLatitude = 10, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Norma", SeasonId = 2, NorthernLatitude = 30, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Octans", SeasonId = 3, NorthernLatitude = 0, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Ophiuchus", SeasonId = 2, NorthernLatitude = 80, SouthernLatitude = 80 },
                new ConstellationModel { Name = "Orion", SeasonId = 4, NorthernLatitude = 85, SouthernLatitude = 75 },
                new ConstellationModel { Name = "Pavo", SeasonId = 2, NorthernLatitude = 30, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Pegasus", SeasonId = 3, NorthernLatitude = 90, SouthernLatitude = 60 },
                new ConstellationModel { Name = "Perseus", SeasonId = 3, NorthernLatitude = 90, SouthernLatitude = 35 },
                new ConstellationModel { Name = "Phoenix", SeasonId = 3, NorthernLatitude = 32, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Pictor", SeasonId = 4, NorthernLatitude = 26, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Pisces", SeasonId = 3, NorthernLatitude = 90, SouthernLatitude = 65 },
                new ConstellationModel { Name = "Piscis Austrinus", SeasonId = 3, NorthernLatitude = 55, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Puppis", SeasonId = 4, NorthernLatitude = 40, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Pyxis", SeasonId = 1, NorthernLatitude = 50, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Reticulum", SeasonId = 4, NorthernLatitude = 23, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Sagitta", SeasonId = 2, NorthernLatitude = 90, SouthernLatitude = 70 },
                new ConstellationModel { Name = "Sagittarius", SeasonId = 2, NorthernLatitude = 55, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Scorpius", SeasonId = 2, NorthernLatitude = 40, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Sculptor", SeasonId = 3, NorthernLatitude = 50, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Scutum", SeasonId = 2, NorthernLatitude = 80, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Serpens", SeasonId = 2, NorthernLatitude = 80, SouthernLatitude = 80 },
                new ConstellationModel { Name = "Sextans", SeasonId = 1, NorthernLatitude = 80, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Taurus", SeasonId = 4, NorthernLatitude = 90, SouthernLatitude = 65 },
                new ConstellationModel { Name = "Telescopium", SeasonId = 2, NorthernLatitude = 40, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Triangulum", SeasonId = 3, NorthernLatitude = 90, SouthernLatitude = 60 },
                new ConstellationModel { Name = "Triangulum Australe", SeasonId = 2, NorthernLatitude = 25, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Tucana", SeasonId = 3, NorthernLatitude = 25, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Ursa Major", SeasonId = 1, NorthernLatitude = 90, SouthernLatitude = 30 },
                new ConstellationModel { Name = "Ursa Minor", SeasonId = 1, NorthernLatitude = 90, SouthernLatitude = 10 },
                new ConstellationModel { Name = "Vela", SeasonId = 4, NorthernLatitude = 30, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Virgo", SeasonId = 1, NorthernLatitude = 80, SouthernLatitude = 80 },
                new ConstellationModel { Name = "Volans", SeasonId = 4, NorthernLatitude = 15, SouthernLatitude = 90 },
                new ConstellationModel { Name = "Vulpecula", SeasonId = 2, NorthernLatitude = 90, SouthernLatitude = 55 }

                );
        }
    }
}
