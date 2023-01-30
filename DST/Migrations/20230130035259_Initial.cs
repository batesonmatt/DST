using Microsoft.EntityFrameworkCore.Migrations;

namespace DST.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "DsoTypes",
                columns: table => new
                {
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsoTypes", x => x.Type);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    North = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    South = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Constellations",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    NorthernLatitude = table.Column<int>(type: "int", nullable: false),
                    SouthernLatitude = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constellations", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Constellations_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DsoItems",
                columns: table => new
                {
                    CatalogName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Common = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RightAscension = table.Column<double>(type: "float", nullable: false),
                    Declination = table.Column<double>(type: "float", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    Magnitude = table.Column<double>(type: "float", nullable: true),
                    ConstellationName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsoItems", x => new { x.CatalogName, x.Id });
                    table.ForeignKey(
                        name: "FK_DsoItems_Catalogs_CatalogName",
                        column: x => x.CatalogName,
                        principalTable: "Catalogs",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DsoItems_Constellations_ConstellationName",
                        column: x => x.ConstellationName,
                        principalTable: "Constellations",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DsoItems_DsoTypes_Type",
                        column: x => x.Type,
                        principalTable: "DsoTypes",
                        principalColumn: "Type",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Catalogs",
                column: "Name",
                values: new object[]
                {
                    "Messier",
                    "Caldwell"
                });

            migrationBuilder.InsertData(
                table: "DsoTypes",
                column: "Type",
                values: new object[]
                {
                    "Asterism",
                    "Double star",
                    "Galaxy",
                    "Nebula",
                    "Star cloud",
                    "Star cluster"
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "Id", "North", "South" },
                values: new object[,]
                {
                    { 1, "Spring", "Autumn" },
                    { 2, "Summer", "Winter" },
                    { 3, "Autumn", "Spring" },
                    { 4, "Winter", "Summer" }
                });

            migrationBuilder.InsertData(
                table: "Constellations",
                columns: new[] { "Name", "NorthernLatitude", "SeasonId", "SouthernLatitude" },
                values: new object[,]
                {
                    { "Antlia", 45, 1, 90 },
                    { "Triangulum", 90, 3, 60 },
                    { "Sculptor", 50, 3, 90 },
                    { "Piscis Austrinus", 55, 3, 90 },
                    { "Pisces", 90, 3, 65 },
                    { "Phoenix", 32, 3, 90 },
                    { "Perseus", 90, 3, 35 },
                    { "Pegasus", 90, 3, 60 },
                    { "Octans", 0, 3, 90 },
                    { "Tucana", 25, 3, 90 },
                    { "Lacerta", 90, 3, 40 },
                    { "Cetus", 70, 3, 90 },
                    { "Cepheus", 90, 3, 10 },
                    { "Cassiopeia", 90, 3, 20 },
                    { "Aries", 90, 3, 60 },
                    { "Aquarius", 65, 3, 90 },
                    { "Andromeda", 90, 3, 40 },
                    { "Vulpecula", 90, 2, 55 },
                    { "Triangulum Australe", 25, 2, 90 },
                    { "Grus", 34, 3, 90 },
                    { "Telescopium", 40, 2, 90 },
                    { "Auriga", 90, 4, 40 },
                    { "Camelopardalis", 90, 4, 10 },
                    { "Taurus", 90, 4, 65 },
                    { "Reticulum", 23, 4, 90 },
                    { "Puppis", 40, 4, 90 },
                    { "Pictor", 26, 4, 90 },
                    { "Orion", 85, 4, 75 },
                    { "Monoceros", 75, 4, 90 },
                    { "Mensa", 4, 4, 90 },
                    { "Lepus", 63, 4, 90 },
                    { "Caelum", 40, 4, 90 },
                    { "Hydrus", 8, 4, 90 },
                    { "Gemini", 90, 4, 60 },
                    { "Fornax", 50, 4, 90 },
                    { "Eridanus", 32, 4, 90 },
                    { "Dorado", 20, 4, 90 },
                    { "Columba", 45, 4, 90 },
                    { "Carina", 20, 4, 90 },
                    { "Canis Minor", 90, 4, 75 },
                    { "Canis Major", 60, 4, 90 },
                    { "Horologium", 30, 4, 90 }
                });

            migrationBuilder.InsertData(
                table: "Constellations",
                columns: new[] { "Name", "NorthernLatitude", "SeasonId", "SouthernLatitude" },
                values: new object[,]
                {
                    { "Serpens", 80, 2, 80 },
                    { "Scutum", 80, 2, 90 },
                    { "Scorpius", 40, 2, 90 },
                    { "Ursa Major", 90, 1, 30 },
                    { "Sextans", 80, 1, 90 },
                    { "Pyxis", 50, 1, 90 },
                    { "Musca", 10, 1, 90 },
                    { "Lynx", 90, 1, 55 },
                    { "Lupus", 35, 1, 90 },
                    { "Leo Minor", 90, 1, 45 },
                    { "Leo", 90, 1, 65 },
                    { "Ursa Minor", 90, 1, 10 },
                    { "Hydra", 54, 1, 83 },
                    { "Crater", 65, 1, 90 },
                    { "Corvus", 60, 1, 90 },
                    { "Coma Berenices", 90, 1, 70 },
                    { "Chamaeleon", 0, 1, 90 },
                    { "Centaurus", 25, 1, 90 },
                    { "Canes Venatici", 90, 1, 40 },
                    { "Cancer", 90, 1, 60 },
                    { "Boötes", 90, 1, 50 },
                    { "Crux", 20, 1, 90 },
                    { "Virgo", 80, 1, 80 },
                    { "Apus", 5, 2, 90 },
                    { "Aquila", 90, 2, 75 },
                    { "Sagittarius", 55, 2, 90 },
                    { "Sagitta", 90, 2, 70 },
                    { "Pavo", 30, 2, 90 },
                    { "Ophiuchus", 80, 2, 80 },
                    { "Norma", 30, 2, 90 },
                    { "Microscopium", 45, 2, 90 },
                    { "Lyra", 90, 2, 40 },
                    { "Libra", 65, 2, 90 },
                    { "Indus", 15, 2, 90 },
                    { "Hercules", 90, 2, 50 },
                    { "Equuleus", 90, 2, 80 },
                    { "Draco", 90, 2, 15 },
                    { "Delphinus", 90, 2, 70 },
                    { "Cygnus", 90, 2, 40 },
                    { "Corona Borealis", 90, 2, 50 },
                    { "Corona Australis", 40, 2, 90 },
                    { "Circinus", 30, 2, 90 }
                });

            migrationBuilder.InsertData(
                table: "Constellations",
                columns: new[] { "Name", "NorthernLatitude", "SeasonId", "SouthernLatitude" },
                values: new object[,]
                {
                    { "Capricornus", 60, 2, 90 },
                    { "Ara", 25, 2, 90 },
                    { "Vela", 30, 4, 90 },
                    { "Volans", 15, 4, 90 }
                });

            migrationBuilder.InsertData(
                table: "DsoItems",
                columns: new[] { "CatalogName", "Id", "Common", "ConstellationName", "Declination", "Description", "Distance", "Magnitude", "RightAscension", "Type" },
                values: new object[,]
                {
                    { "Caldwell", 45, null, "Boötes", 8.8852778000000008, "Spiral galaxy", 57700.0, 10.199999999999999, 13.6255556, "Galaxy" },
                    { "Caldwell", 23, "Silver Sliver Galaxy,Outer Limits Galaxy", "Andromeda", 42.349166699999998, "Spiral galaxy", 31000.0, 10.0, 2.3759443999999998, "Galaxy" },
                    { "Caldwell", 28, null, "Andromeda", 37.865833299999998, "Open cluster", 1.3, 5.7000000000000002, 1.9652778, "Star cluster" },
                    { "Messier", 2, null, "Aquarius", -0.82325000000000004, "Globular cluster", 33.0, 6.2999999999999998, 21.557505599999999, "Star cluster" },
                    { "Messier", 72, null, "Aquarius", -12.5373056, "Globular cluster", 54.57, 9.4000000000000004, 20.8910278, "Star cluster" },
                    { "Messier", 73, null, "Aquarius", -12.6333333, "Group of 4 stars", 2.5, 9.0, 20.981666700000002, "Asterism" },
                    { "Caldwell", 55, "Saturn Nebula", "Aquarius", -11.363402799999999, "Planetary nebula", 1.3999999999999999, 8.0, 21.0696881, "Nebula" },
                    { "Caldwell", 63, "Helix Nebula", "Aquarius", -20.837111100000001, "Planetary nebula", 0.65000000000000002, 7.2999999999999998, 22.4940417, "Nebula" },
                    { "Messier", 52, null, "Cassiopeia", 61.5833333, "Open cluster", 5.0, 5.0, 23.4033333, "Star cluster" },
                    { "Messier", 103, null, "Cassiopeia", 60.700000000000003, "Open cluster", 10.0, 7.4000000000000004, 1.5533333, "Star cluster" },
                    { "Caldwell", 8, null, "Cassiopeia", 63.302, "Open cluster", 3.7000000000000002, 9.5, 1.492, "Star cluster" },
                    { "Caldwell", 10, null, "Cassiopeia", 61.218333299999998, "Open cluster", 6.8499999999999996, 7.0999999999999996, 1.7711110999999999, "Star cluster" },
                    { "Caldwell", 11, "Bubble Nebula", "Cassiopeia", 61.201666699999997, "H II region nebula", 7.0999999999999996, 10.0, 23.34675, "Nebula" },
                    { "Caldwell", 13, "Owl Cluster,E.T. Cluster,Dragonfly Cluster,Kachina Doll Cluster", "Cassiopeia", 58.290833300000003, "Open cluster", 7.9219999999999997, 6.4000000000000004, 1.3257222, "Star cluster" },
                    { "Caldwell", 17, null, "Cassiopeia", 48.508888900000002, "Dwarf spheroidal galaxy", 2580.0, 9.5, 0.55336110000000005, "Galaxy" },
                    { "Caldwell", 18, null, "Cassiopeia", 48.337377799999999, "Dwarf spheroidal galaxy", 2050.0, 9.1999999999999993, 0.64943609999999996, "Galaxy" },
                    { "Caldwell", 1, null, "Cepheus", 85.254999999999995, "Open cluster", 5.4000000000000004, 8.0999999999999996, 0.8072222, "Star cluster" },
                    { "Caldwell", 2, "Bow-Tie Nebula", "Cepheus", 72.521968099999995, "Planetary nebula", 3.5, 12.300000000000001, 0.21694859999999999, "Nebula" },
                    { "Caldwell", 4, "Iris Nebula", "Cepheus", 68.163333300000005, "Reflection nebula with open cluster", 1.3999999999999999, 7.0, 21.0266667, "Nebula" },
                    { "Caldwell", 9, "Cave Nebula", "Cepheus", 62.475944400000003, "H II region nebula", 2.7999999999999998, 7.7000000000000002, 22.954761099999999, "Nebula" },
                    { "Caldwell", 12, "Fireworks Galaxy", "Cepheus", 60.153888899999998, "Spiral galaxy", 18000.0, 9.6999999999999993, 20.581194400000001, "Galaxy" },
                    { "Messier", 77, "Cetus A", "Cetus", -0.013333299999999999, "Spiral galaxy", 47000.0, 9.5999999999999996, 2.7113056000000002, "Galaxy" },
                    { "Caldwell", 51, null, "Cetus", 2.1177777999999998, "Irregular galaxy", 2380.0, 9.8000000000000007, 1.0799444, "Galaxy" },
                    { "Caldwell", 56, "Skull Nebula", "Cetus", -11.8719278, "Planetary nebula", 1.6000000000000001, 8.0, 0.78426059999999997, "Nebula" },
                    { "Caldwell", 22, "Blue Snowball Nebula", "Andromeda", 42.534999999999997, "Planetary nebula", 2.5, 8.3000000000000007, 23.431666700000001, "Nebula" },
                    { "Messier", 110, "Satellite of Andromeda Galaxy", "Andromeda", 41.685277800000001, "Dwarf elliptical galaxy", 2690.0, 9.0, 0.6728056, "Galaxy" },
                    { "Messier", 32, "Satellite of Andromeda Galaxy", "Andromeda", 40.865277800000001, "Dwarf elliptical galaxy", 2490.0, 8.0999999999999996, 0.71161110000000005, "Galaxy" },
                    { "Messier", 31, "Andromeda Galaxy", "Andromeda", 41.2691667, "Spiral galaxy", 2540.0, 3.3999999999999999, 0.71230559999999998, "Galaxy" },
                    { "Messier", 23, null, "Sagittarius", -19.016666699999998, "Open cluster", 2.1499999999999999, 6.9000000000000004, 17.946666700000002, "Star cluster" },
                    { "Messier", 24, "Small Sagittarius Star Cloud", "Sagittarius", -18.550000000000001, "Milky Way star cloud", 10.0, 2.5, 18.283333299999999, "Star cloud" },
                    { "Messier", 25, null, "Sagittarius", -19.25, "Open cluster", 2.0, 4.5999999999999996, 18.5266667, "Star cluster" },
                    { "Messier", 28, null, "Sagittarius", -24.8698333, "Globular cluster", 17.899999999999999, 7.7000000000000002, 18.409136100000001, "Star cluster" },
                    { "Messier", 54, null, "Sagittarius", -30.479861100000001, "Globular cluster", 87.400000000000006, 8.4000000000000004, 18.917591699999999, "Star cluster" },
                    { "Messier", 55, null, "Sagittarius", -30.964749999999999, "Globular cluster", 17.600000000000001, 7.4000000000000004, 19.6665861, "Star cluster" },
                    { "Messier", 69, null, "Sagittarius", -32.348083299999999, "Globular cluster", 29.699999999999999, 8.3000000000000007, 18.5230833, "Star cluster" },
                    { "Messier", 70, null, "Sagittarius", -32.2921111, "Globular cluster", 29.399999999999999, 9.0999999999999996, 18.7202111, "Star cluster" },
                    { "Messier", 75, null, "Sagittarius", -21.921166700000001, "Globular cluster", 67.5, 9.1999999999999993, 20.101319400000001, "Star cluster" },
                    { "Caldwell", 57, "Barnard's Galaxy", "Sagittarius", -14.789166700000001, "Barred irregular galaxy", 2300.0, 8.8000000000000007, 19.749055599999998, "Galaxy" },
                    { "Messier", 4, null, "Scorpius", -26.525749999999999, "Globular cluster", 7.2000000000000002, 5.9000000000000004, 16.3931167, "Star cluster" },
                    { "Caldwell", 62, null, "Cetus", -20.760277800000001, "Spiral galaxy", 11100.0, 9.1999999999999993, 0.78569440000000002, "Galaxy" },
                    { "Messier", 6, "Butterfly Cluster", "Scorpius", -32.216666699999998, "Open cluster", 1.6000000000000001, 4.2000000000000002, 17.6683333, "Star cluster" },
                    { "Messier", 80, null, "Scorpius", -22.976083299999999, "Globular cluster", 32.600000000000001, 7.9000000000000004, 16.2840028, "Star cluster" }
                });

            migrationBuilder.InsertData(
                table: "DsoItems",
                columns: new[] { "CatalogName", "Id", "Common", "ConstellationName", "Declination", "Description", "Distance", "Magnitude", "RightAscension", "Type" },
                values: new object[,]
                {
                    { "Caldwell", 69, "Butterfly Nebula,Bug Nebula", "Scorpius", -37.104427800000003, "Planetary nebula", 3.7000000000000002, 9.5, 17.2289475, "Nebula" },
                    { "Caldwell", 75, null, "Scorpius", -40.6402778, "Open cluster", 18.600000000000001, 5.7999999999999998, 16.421666699999999, "Star cluster" },
                    { "Caldwell", 76, null, "Scorpius", -41.826666699999997, "Open cluster", 5.5999999999999996, 2.6000000000000001, 16.902363900000001, "Star cluster" },
                    { "Messier", 11, "Wild Duck Cluster", "Scutum", -6.2666667, "Open cluster", 6.2000000000000002, 6.2999999999999998, 18.851666699999999, "Star cluster" },
                    { "Messier", 26, null, "Scutum", -9.4000000000000004, "Open cluster", 5.0, 8.0, 18.753333300000001, "Star cluster" },
                    { "Messier", 5, null, "Serpens", 2.0810278000000002, "Globular cluster", 24.5, 6.7000000000000002, 15.3092278, "Star cluster" },
                    { "Messier", 16, "Eagle Nebula", "Serpens", -13.816666700000001, "H II region nebula with cluster", 7.0, 6.0, 18.3133333, "Nebula" },
                    { "Caldwell", 95, null, "Triangulum Australe", -60.433333300000001, "Open cluster", 2.7000000000000002, 5.0999999999999996, 16.055, "Star cluster" },
                    { "Messier", 27, "Dumbbell Nebula", "Vulpecula", 22.721136099999999, "Planetary nebula", 1.3340000000000001, 7.5, 19.993427799999999, "Nebula" },
                    { "Caldwell", 37, null, "Vulpecula", 26.483333300000002, "Open cluster", 1.95, 6.9000000000000004, 20.199999999999999, "Star cluster" },
                    { "Messier", 7, "Ptolemy Cluster", "Scorpius", -34.792777800000003, "Open cluster", 0.97999999999999998, 3.2999999999999998, 17.8975556, "Star cluster" },
                    { "Messier", 22, "Sagittarius Cluster", "Sagittarius", -23.90475, "Globular cluster", 10.6, 5.0999999999999996, 18.606649999999998, "Star cluster" },
                    { "Caldwell", 16, null, "Lacerta", 49.897500000000001, "Open cluster", 2.7999999999999998, 6.4000000000000004, 22.2523889, "Star cluster" },
                    { "Caldwell", 30, null, "Pegasus", 34.415555599999998, "Spiral galaxy", 45000.0, 9.5, 22.617805600000001, "Galaxy" },
                    { "Caldwell", 73, null, "Columba", -40.046555599999998, "Globular cluster", 39.399999999999999, 7.2999999999999998, 5.2352110999999999, "Star cluster" },
                    { "Caldwell", 103, "Center of the Tarantula Nebula", "Dorado", -69.100833300000005, "H II region nebula", 170.0, 4.0, 5.6451389000000001, "Nebula" },
                    { "Caldwell", 67, null, "Fornax", -30.274999999999999, "Barred spiral galaxy", 45000.0, 9.1999999999999993, 2.7719444000000002, "Galaxy" },
                    { "Messier", 35, null, "Gemini", 24.350000000000001, "Open cluster", 2.7999999999999998, 5.2999999999999998, 6.1516666999999998, "Star cluster" },
                    { "Caldwell", 39, "Eskimo Nebula,Clown Face Nebula,Lion Nebula", "Gemini", 20.9118022, "Planetary nebula", 5.4800000000000004, 9.1999999999999993, 7.4863241, "Nebula" },
                    { "Caldwell", 87, null, "Horologium", -55.216222199999997, "Globular cluster", 53.5, 8.4000000000000004, 3.2045028000000002, "Star cluster" },
                    { "Messier", 79, null, "Lepus", -24.524249999999999, "Globular cluster", 41.0, 8.5999999999999996, 5.4029417000000004, "Star cluster" },
                    { "Messier", 50, null, "Monoceros", -8.3333332999999996, "Open cluster", 3.2000000000000002, 5.9000000000000004, 7.0533333000000002, "Star cluster" },
                    { "Caldwell", 46, "Hubble's Variable Nebula", "Monoceros", 8.75, "Reflection nebula", 2.5, 9.5, 6.6527778, "Nebula" },
                    { "Caldwell", 49, "Rosette Nebula", "Monoceros", 4.9983332999999996, "H II region nebula with open cluster", 5.0300000000000002, 9.0, 6.5625, "Nebula" },
                    { "Caldwell", 50, "Satellite Cluster", "Monoceros", 4.9333333000000001, "Open cluster", 5.0499999999999998, 4.7999999999999998, 6.5316666999999997, "Star cluster" },
                    { "Caldwell", 54, null, "Monoceros", -10.77, "Open cluster", 12.699999999999999, 7.5999999999999996, 8.0002777999999992, "Star cluster" },
                    { "Messier", 42, "Orion Nebula", "Orion", -5.3911110999999998, "H II region nebula", 1.3440000000000001, 4.0, 5.5881388999999997, "Nebula" },
                    { "Messier", 43, "De Mairan's Nebula", "Orion", -5.2666667, "H II region nebula", 1.6000000000000001, 9.0, 5.5933333000000003, "Nebula" },
                    { "Messier", 78, null, "Orion", 0.013888899999999999, "Reflection nebula", 1.6000000000000001, 8.3000000000000007, 5.7796389000000001, "Nebula" },
                    { "Messier", 46, null, "Puppis", -14.816666700000001, "Open cluster", 5.4000000000000004, 6.0999999999999996, 7.6966666999999998, "Star cluster" },
                    { "Messier", 47, null, "Puppis", -14.5, "Open cluster", 1.6000000000000001, 4.2000000000000002, 7.6100000000000003, "Star cluster" },
                    { "Messier", 93, null, "Puppis", -23.8666667, "Open cluster", 3.6000000000000001, 6.0, 7.7433332999999998, "Star cluster" },
                    { "Caldwell", 71, null, "Puppis", -38.533333300000002, "Open cluster", 3.7000000000000002, 5.7999999999999998, 7.8693888999999997, "Star cluster" },
                    { "Messier", 1, "Crab Nebula", "Taurus", 22.014500000000002, "Supernova remnant", 6.5, 8.4000000000000004, 5.5755388999999997, "Nebula" },
                    { "Messier", 45, "Pleiades,The Seven Sisters", "Taurus", 24.1166667, "Open cluster", 0.42499999999999999, 1.6000000000000001, 3.79, "Star cluster" },
                    { "Caldwell", 41, "Hyades", "Taurus", 15.761111100000001, "Open cluster", 0.151, 0.5, 4.4713889, "Star cluster" },
                    { "Caldwell", 74, "Eight Burst Nebula,Southern Ring Nebula", "Vela", -40.436405600000001, "Planetary nebula", 2.0, 9.5999999999999996, 10.117156700000001, "Nebula" },
                    { "Caldwell", 102, "Southern Pleiades,Theta Carinae Cluster", "Carina", -64.3941667, "Open cluster", 0.54600000000000004, 1.8999999999999999, 10.7159722, "Star cluster" },
                    { "Caldwell", 96, "Southern Beehive,Sprinter", "Carina", -60.866666700000003, "Open cluster", 1.3, 3.7999999999999998, 7.9722222, "Star cluster" },
                    { "Caldwell", 92, "Carina Nebula,Grand Nebula", "Carina", -59.867777799999999, "Nebula collection", 7.5, 4.7999999999999998, 10.7523611, "Nebula" },
                    { "Caldwell", 91, "Wishing Well Cluster,Pincushion Cluster,Football Cluster,Black Arrow Cluster", "Carina", -58.729999999999997, "Open cluster", 1.3200000000000001, 3.0, 11.092499999999999, "Star cluster" },
                    { "Caldwell", 43, null, "Pegasus", 16.145555600000002, "Spiral galaxy", 40000.0, 10.5, 0.054138899999999997, "Galaxy" }
                });

            migrationBuilder.InsertData(
                table: "DsoItems",
                columns: new[] { "CatalogName", "Id", "Common", "ConstellationName", "Declination", "Description", "Distance", "Magnitude", "RightAscension", "Type" },
                values: new object[,]
                {
                    { "Caldwell", 44, null, "Pegasus", 12.322777800000001, "Barred spiral galaxy", 106000.0, 10.800000000000001, 23.082388900000002, "Galaxy" },
                    { "Messier", 34, null, "Perseus", 42.766666700000002, "Open cluster", 1.5, 5.5, 2.7016667000000001, "Star cluster" },
                    { "Messier", 76, "Little Dumbbell Nebula,Barbell Nebula,Cork Nebula", "Perseus", 51.575277800000002, "Planetary nebula", 2.5, 10.1, 1.7066667, "Nebula" },
                    { "Caldwell", 14, "Double Cluster (West)", "Perseus", 57.149999999999999, "Open cluster", 7.5999999999999996, 5.2999999999999998, 2.3183332999999999, "Star cluster" },
                    { "Caldwell", 24, "Perseus A", "Perseus", 41.511666699999999, "Supergiant elliptical galaxy", 230000.0, 11.9, 3.3300277999999999, "Galaxy" },
                    { "Caldwell", 110, "Double Cluster (East)", "Perseus", 57.133333299999997, "Open cluster", 7.5999999999999996, 6.0999999999999996, 2.3666667000000001, "Star cluster" },
                    { "Messier", 74, "Phantom Galaxy", "Pisces", 15.7836111, "Spiral galaxy", 30000.0, 10.0, 1.6116111, "Galaxy" },
                    { "Caldwell", 65, "Sculptor Galaxy,Silver Coin Galaxy", "Sculptor", -25.288333300000001, "Spiral galaxy", 10200.0, 7.5999999999999996, 0.79249999999999998, "Galaxy" },
                    { "Caldwell", 70, null, "Sculptor", -37.684444399999997, "Spiral galaxy", 6070.0, 8.0999999999999996, 0.91486109999999998, "Galaxy" },
                    { "Caldwell", 72, null, "Sculptor", -39.196666700000002, "Barred spiral galaxy", 6500.0, 7.7999999999999998, 0.2482222, "Galaxy" },
                    { "Messier", 15, null, "Pegasus", 12.167, "Globular cluster", 33.0, 6.2000000000000002, 21.4995361, "Star cluster" },
                    { "Messier", 33, "Triangulum Galaxy,Pinwheel Galaxy", "Triangulum", 30.660194400000002, "Spiral galaxy", 2725.0, 5.7000000000000002, 1.5638943999999999, "Galaxy" },
                    { "Caldwell", 106, "47 Tucanae", "Tucana", -72.081277799999995, "Globular cluster", 16.699999999999999, 4.0999999999999996, 0.40157500000000002, "Star cluster" },
                    { "Messier", 36, null, "Auriga", 34.1344444, "Open cluster", 4.0999999999999996, 6.2999999999999998, 5.6033333000000001, "Star cluster" },
                    { "Messier", 37, null, "Auriga", 32.550555600000003, "Open cluster", 4.5110000000000001, 6.2000000000000002, 5.8716666999999996, "Star cluster" },
                    { "Messier", 38, "Starfish Cluster", "Auriga", 35.854999999999997, "Open cluster", 4.2000000000000002, 7.4000000000000004, 5.4783333000000001, "Star cluster" },
                    { "Caldwell", 31, "Flaming Star Nebula", "Auriga", 34.463611100000001, "Emission and reflection nebula", 1.6000000000000001, 6.0, 5.2680556000000003, "Nebula" },
                    { "Caldwell", 5, "Hidden Galaxy", "Camelopardalis", 68.096111100000002, "Spiral galaxy", 10700.0, 8.8000000000000007, 3.7801388999999999, "Galaxy" },
                    { "Caldwell", 7, null, "Camelopardalis", 65.602500000000006, "Spiral galaxy", 9650.0, 8.9000000000000004, 7.6142778, "Galaxy" },
                    { "Messier", 41, null, "Canis Major", -20.766666699999998, "Open cluster", 2.2999999999999998, 4.5, 6.7666667, "Star cluster" },
                    { "Caldwell", 58, "Caroline's Cluster", "Canis Major", -15.6333333, "Open cluster", 3.7000000000000002, 7.2000000000000002, 7.2949999999999999, "Star cluster" },
                    { "Caldwell", 64, "Tau Canis Majoris Cluster", "Canis Major", -24.983333300000002, "Open cluster", 4.9649999999999999, 3.8999999999999999, 7.3099999999999996, "Star cluster" },
                    { "Caldwell", 90, null, "Carina", -58.311282400000003, "Planetary nebula", 6.2000000000000002, 9.6999999999999993, 9.3570509000000008, "Nebula" },
                    { "Caldwell", 104, null, "Tucana", -70.848777799999993, "Globular cluster", 27.699999999999999, 6.4000000000000004, 1.0539611, "Star cluster" },
                    { "Messier", 21, null, "Sagittarius", -22.5, "Open cluster", 4.25, 6.5, 18.076666700000001, "Star cluster" },
                    { "Messier", 20, "Trifid Nebula", "Sagittarius", -23.030000000000001, "H II region nebula with cluster", 5.2000000000000002, 6.2999999999999998, 18.0397222, "Nebula" },
                    { "Messier", 18, null, "Sagittarius", -17.1333333, "Open cluster", 4.9000000000000004, 7.5, 18.3316667, "Star cluster" },
                    { "Caldwell", 36, null, "Coma Berenices", 27.959722200000002, "Spiral galaxy", 30000.0, 9.9000000000000004, 12.599361099999999, "Galaxy" },
                    { "Caldwell", 38, "Needle Galaxy", "Coma Berenices", 25.9877778, "Spiral galaxy", 42000.0, 9.5999999999999996, 12.6057778, "Galaxy" },
                    { "Caldwell", 60, "Antennae Galaxies (North)", "Corvus", -18.864999999999998, "Interacting galaxy", 45000.0, 10.699999999999999, 12.0313333, "Galaxy" },
                    { "Caldwell", 61, "Antennae Galaxies (South)", "Corvus", -18.885000000000002, "Interacting galaxy", 45000.0, 11.0, 12.031611099999999, "Galaxy" },
                    { "Caldwell", 94, "Jewel Box", "Crux", -60.349444400000003, "Open cluster", 6.4400000000000004, 4.2000000000000002, 12.894813900000001, "Star cluster" },
                    { "Caldwell", 98, null, "Crux", -62.994999999999997, "Open cluster", 4.0999999999999996, 6.9000000000000004, 12.705, "Star cluster" },
                    { "Caldwell", 99, "Coalsack Nebula", "Crux", -62.424444399999999, "Dark nebula", 0.60999999999999999, null, 12.8719444, "Nebula" },
                    { "Messier", 48, null, "Hydra", -5.75, "Open cluster", 1.5, 5.5, 8.2283332999999992, "Star cluster" },
                    { "Messier", 68, null, "Hydra", -26.744055599999999, "Globular cluster", 33.600000000000001, 9.6999999999999993, 12.6577722, "Star cluster" },
                    { "Messier", 83, "Southern Pinwheel Galaxy", "Hydra", -29.865833299999998, "Barred spiral galaxy", 14700.0, 7.5, 13.616916700000001, "Galaxy" },
                    { "Caldwell", 59, "Ghost of Jupiter", "Hydra", -18.6423889, "Planetary nebula", 1.3999999999999999, 8.1999999999999993, 10.4128056, "Nebula" },
                    { "Caldwell", 66, null, "Hydra", -26.538333300000001, "Globular cluster", 113.0, 10.199999999999999, 14.6601389, "Star cluster" },
                    { "Messier", 65, "Leo Triplet", "Leo", 13.0922222, "Barred spiral galaxy", 41500.0, 10.300000000000001, 11.3155278, "Galaxy" },
                    { "Messier", 66, "Leo Triplet", "Leo", 12.9916667, "Barred spiral galaxy", 36000.0, 8.9000000000000004, 11.3375, "Galaxy" },
                    { "Messier", 95, null, "Leo", 11.703888900000001, "Barred spiral galaxy", 32600.0, 11.4, 10.7326944, "Galaxy" }
                });

            migrationBuilder.InsertData(
                table: "DsoItems",
                columns: new[] { "CatalogName", "Id", "Common", "ConstellationName", "Declination", "Description", "Distance", "Magnitude", "RightAscension", "Type" },
                values: new object[,]
                {
                    { "Messier", 96, null, "Leo", 11.82, "Spiral galaxy", 31000.0, 10.1, 10.779361099999999, "Galaxy" },
                    { "Messier", 105, null, "Leo", 12.5816667, "Elliptical galaxy", 32000.0, 10.199999999999999, 10.7971111, "Galaxy" },
                    { "Caldwell", 40, null, "Leo", 18.356791699999999, "Spiral galaxy", 74000.0, 10.800000000000001, 11.3343872, "Galaxy" },
                    { "Caldwell", 25, null, "Lynx", 38.881916699999998, "Globular cluster", 275.0, 10.4, 7.6356972000000001, "Star cluster" },
                    { "Caldwell", 105, null, "Musca", -70.874611099999996, "Globular cluster", 21.5, 7.4000000000000004, 12.99305, "Star cluster" },
                    { "Caldwell", 108, null, "Musca", -72.659083300000006, "Globular cluster", 18.899999999999999, 9.8499999999999996, 12.429286100000001, "Star cluster" },
                    { "Caldwell", 53, "Spindle Galaxy", "Sextans", -7.7186111000000004, "Lenticular galaxy", 31600.0, 9.9000000000000004, 10.087222199999999, "Galaxy" },
                    { "Messier", 40, "Winnecke-4", "Ursa Major", 58.083055600000002, "Optical double star", 0.51000000000000001, 9.6999999999999993, 12.370138900000001, "Double star" },
                    { "Caldwell", 35, "Coma B", "Coma Berenices", 27.976944400000001, "Supergiant elliptical galaxy", 300000.0, 11.4, 13.00225, "Galaxy" },
                    { "Messier", 100, null, "Coma Berenices", 15.8225, "Spiral galaxy", 55000.0, 10.1, 12.3819167, "Galaxy" },
                    { "Messier", 99, null, "Coma Berenices", 14.416388899999999, "Spiral galaxy", 50200.0, 10.4, 12.3137778, "Galaxy" },
                    { "Messier", 98, null, "Coma Berenices", 14.9004694, "Spiral galaxy", 44400.0, 11.0, 12.2300811, "Galaxy" },
                    { "Messier", 44, "Beehive Cluster,Praesepe", "Cancer", 19.983333300000002, "Open cluster", 0.57699999999999996, 3.7000000000000002, 8.6733332999999995, "Star cluster" },
                    { "Messier", 67, null, "Cancer", 11.816666700000001, "Open cluster", 2.77, 6.0999999999999996, 8.8550000000000004, "Star cluster" },
                    { "Caldwell", 48, null, "Cancer", 7.0379250000000004, "Spiral galaxy", 67000.0, 10.699999999999999, 9.1722532999999995, "Galaxy" },
                    { "Messier", 3, null, "Canes Venatici", 28.377277800000002, "Globular cluster", 33.899999999999999, 6.2000000000000002, 13.703227800000001, "Star cluster" },
                    { "Messier", 51, "Whirlpool Galaxy", "Canes Venatici", 47.1952778, "Spiral galaxy", 23000.0, 8.4000000000000004, 13.4979722, "Galaxy" },
                    { "Messier", 63, "Sunflower Galaxy", "Canes Venatici", 42.029166699999998, "Spiral galaxy", 37000.0, 9.3000000000000007, 13.2636944, "Galaxy" },
                    { "Messier", 94, "Cat's Eye Galaxy,Croc's Eye Galaxy", "Canes Venatici", 41.120555600000003, "Spiral galaxy", 16000.0, 9.0, 12.848083300000001, "Galaxy" },
                    { "Messier", 106, null, "Canes Venatici", 47.303888899999997, "Spiral galaxy", 23700.0, 9.0999999999999996, 12.315972199999999, "Galaxy" },
                    { "Caldwell", 21, null, "Canes Venatici", 44.0944444, "Irregular galaxy", 12000.0, 9.5999999999999996, 12.469972200000001, "Galaxy" },
                    { "Caldwell", 26, null, "Canes Venatici", 37.807222199999998, "Spiral galaxy", 13000.0, 10.4, 12.291555600000001, "Galaxy" },
                    { "Caldwell", 29, null, "Canes Venatici", 37.059166699999999, "Spiral galaxy", 75000.0, 9.8000000000000007, 13.1822778, "Galaxy" },
                    { "Messier", 81, "Bode's Galaxy", "Ursa Major", 69.065277800000004, "Spiral galaxy", 11800.0, 6.9000000000000004, 9.9258889000000003, "Galaxy" },
                    { "Caldwell", 32, "Whale Galaxy", "Canes Venatici", 32.541388900000001, "Barred spiral galaxy", 25000.0, 9.3000000000000007, 12.7022222, "Galaxy" },
                    { "Caldwell", 80, "Omega Centauri", "Centaurus", -47.476861100000001, "Globular cluster", 17.300000000000001, 3.7000000000000002, 13.4460806, "Star cluster" },
                    { "Caldwell", 83, null, "Centaurus", -49.468333299999998, "Barred spiral galaxy", 11700.0, 9.3000000000000007, 13.0909722, "Galaxy" },
                    { "Caldwell", 84, null, "Centaurus", -51.373472200000002, "Globular cluster", 35.899999999999999, 7.5999999999999996, 13.774050000000001, "Star cluster" },
                    { "Caldwell", 97, "Pearl Cluster", "Centaurus", -61.615277800000001, "Open cluster", 5.5, 5.2999999999999998, 11.6036944, "Star cluster" },
                    { "Caldwell", 100, "Lambda Centauri Nebula,Running Chicken Nebula", "Centaurus", -63.372777800000001, "Emission nebula with open cluster", 6.0, 2.8999999999999999, 11.638999999999999, "Nebula" },
                    { "Caldwell", 109, null, "Chamaeleon", -80.858536099999995, "Planetary nebula", 5.5, 11.6, 10.1558083, "Nebula" },
                    { "Messier", 53, null, "Coma Berenices", 18.1681667, "Globular cluster", 58.0, 8.3000000000000007, 13.2153472, "Star cluster" },
                    { "Messier", 64, "Black Eye Galaxy", "Coma Berenices", 21.6827778, "Spiral galaxy", 24000.0, 9.4000000000000004, 12.945472199999999, "Galaxy" },
                    { "Messier", 85, null, "Coma Berenices", 18.191111100000001, "Lenticular galaxy", 60000.0, 10.0, 12.423333299999999, "Galaxy" },
                    { "Messier", 88, null, "Coma Berenices", 14.4205556, "Spiral galaxy", 47500.0, 10.4, 12.533111099999999, "Galaxy" },
                    { "Messier", 91, null, "Coma Berenices", 14.496388899999999, "Barred spiral galaxy", 63000.0, 11.0, 12.5906667, "Galaxy" },
                    { "Caldwell", 77, "Centaurus A", "Centaurus", -43.0191667, "Elliptical or lenticular galaxy", 11000.0, 6.7000000000000002, 13.424333300000001, "Galaxy" },
                    { "Messier", 82, "Cigar Galaxy", "Ursa Major", 69.6797222, "Starburst galaxy", 11500.0, 8.4000000000000004, 9.9311667000000003, "Galaxy" },
                    { "Messier", 97, "Owl Nebula", "Ursa Major", 55.019027800000003, "Planetary nebula", 2.0299999999999998, 9.9000000000000004, 11.2465928, "Nebula" },
                    { "Messier", 101, "Pinwheel Galaxy", "Ursa Major", 54.349166699999998, "Spiral galaxy", 20750.0, 7.9000000000000004, 14.0535, "Galaxy" },
                    { "Caldwell", 33, "East Veil Nebula", "Cygnus", 31.696666700000002, "Supernova remnant", 2.3999999999999999, 7.0, 20.938888899999998, "Nebula" },
                    { "Caldwell", 34, "West Veil Nebula", "Cygnus", 30.7083333, "Supernova remnant", 2.3999999999999999, 7.0, 20.7605556, "Nebula" }
                });

            migrationBuilder.InsertData(
                table: "DsoItems",
                columns: new[] { "CatalogName", "Id", "Common", "ConstellationName", "Declination", "Description", "Distance", "Magnitude", "RightAscension", "Type" },
                values: new object[,]
                {
                    { "Caldwell", 42, null, "Delphinus", 16.187333299999999, "Globular cluster", 135.0, 10.6, 21.024833300000001, "Star cluster" },
                    { "Caldwell", 47, null, "Delphinus", 7.4041389000000004, "Globular cluster", 52.0, 8.8000000000000007, 20.569861100000001, "Star cluster" },
                    { "Messier", 102, "Spindle Galaxy", "Draco", 55.763333299999999, "Lenticular galaxy", 50000.0, 10.699999999999999, 15.1081944, "Galaxy" },
                    { "Caldwell", 3, null, "Draco", 69.462500000000006, "Barred spiral galaxy", 11700.0, 9.6999999999999993, 12.2783611, "Galaxy" },
                    { "Caldwell", 6, "Cat's Eye Nebula", "Draco", 66.633200000000002, "Planetary nebula", 3.2999999999999998, 8.5, 17.9759508, "Nebula" },
                    { "Messier", 13, "Great Globular Cluster", "Hercules", 36.459861099999998, "Globular cluster", 22.199999999999999, 5.7999999999999998, 16.694788899999999, "Star cluster" },
                    { "Messier", 92, null, "Hercules", 43.1359444, "Globular cluster", 26.699999999999999, 6.2999999999999998, 17.2853861, "Star cluster" },
                    { "Messier", 56, null, "Lyra", 30.183472200000001, "Globular cluster", 32.899999999999999, 8.3000000000000007, 19.2765472, "Star cluster" },
                    { "Messier", 57, "Ring Nebula", "Lyra", 33.029175000000002, "Planetary nebula", 2.7000000000000002, 8.8000000000000007, 18.8930775, "Nebula" },
                    { "Caldwell", 27, "Crescent Nebula", "Cygnus", 38.354999999999997, "Emission nebula", 4.7000000000000002, 8.1999999999999993, 20.201944399999999, "Nebula" },
                    { "Caldwell", 89, "S Normae Cluster", "Norma", -57.933333300000001, "Open cluster", 3.5, 5.4000000000000004, 16.3133333, "Star cluster" },
                    { "Messier", 10, null, "Ophiuchus", -4.0994638999999999, "Globular cluster", 14.300000000000001, 6.4000000000000004, 16.9524778, "Star cluster" },
                    { "Messier", 12, null, "Ophiuchus", -1.9485277999999999, "Globular cluster", 15.699999999999999, 7.7000000000000002, 16.7872722, "Star cluster" },
                    { "Messier", 14, null, "Ophiuchus", -3.2459167, "Globular cluster", 30.300000000000001, 8.3000000000000007, 17.626708300000001, "Star cluster" },
                    { "Messier", 19, null, "Ophiuchus", -26.267944400000001, "Globular cluster", 28.699999999999999, 7.5, 17.043802800000002, "Star cluster" },
                    { "Messier", 62, null, "Ophiuchus", -30.112361100000001, "Globular cluster", 22.199999999999999, 7.4000000000000004, 17.020166700000001, "Star cluster" },
                    { "Messier", 107, null, "Ophiuchus", -13.053777800000001, "Globular cluster", 20.899999999999999, 8.9000000000000004, 16.542183300000001, "Star cluster" },
                    { "Caldwell", 93, "Great Peacock Globular", "Pavo", -59.981861100000003, "Globular cluster", 13.0, 5.4000000000000004, 19.181049999999999, "Star cluster" },
                    { "Caldwell", 101, null, "Pavo", -63.857500000000002, "Spiral galaxy", 31000.0, 8.9000000000000004, 19.162805599999999, "Galaxy" },
                    { "Messier", 71, null, "Sagitta", 18.779194400000002, "Globular cluster", 13.0, 6.0999999999999996, 19.896247200000001, "Star cluster" },
                    { "Messier", 8, "Lagoon Nebula", "Sagittarius", -24.386666699999999, "Emission nebula with cluster", 4.0999999999999996, 6.0, 18.060277800000001, "Nebula" },
                    { "Messier", 17, "Omega Nebula,Swan Nebula,Horseshoe Nebula,Lobster Nebula", "Sagittarius", -16.176666699999998, "H II region nebula with cluster", 5.5, 6.0, 18.340555599999998, "Nebula" },
                    { "Messier", 9, null, "Ophiuchus", -18.516249999999999, "Globular cluster", 25.800000000000001, 8.4000000000000004, 17.3199389, "Star cluster" },
                    { "Caldwell", 79, null, "Vela", -46.411222199999997, "Globular cluster", 16.300000000000001, 6.7999999999999998, 10.2935444, "Star cluster" },
                    { "Caldwell", 20, "North America Nebula", "Cygnus", 44.516666700000002, "Emission nebula", 1.8, 4.0, 20.988333300000001, "Nebula" },
                    { "Caldwell", 15, "Blinking Planetary", "Cygnus", 50.525083299999999, "Planetary nebula", 2.2000000000000002, 8.8000000000000007, 19.746722200000001, "Nebula" },
                    { "Messier", 108, null, "Ursa Major", 55.674166700000001, "Barred spiral galaxy", 46000.0, 10.699999999999999, 11.191944400000001, "Galaxy" },
                    { "Messier", 109, null, "Ursa Major", 53.374444400000002, "Barred spiral galaxy", 83500.0, 10.6, 11.960000000000001, "Galaxy" },
                    { "Messier", 49, null, "Virgo", 8.0005556000000002, "Elliptical galaxy", 55900.0, 9.4000000000000004, 12.496305599999999, "Galaxy" },
                    { "Messier", 58, null, "Virgo", 11.818055599999999, "Barred spiral galaxy", 63000.0, 10.5, 12.62875, "Galaxy" },
                    { "Messier", 59, null, "Virgo", 11.646944400000001, "Elliptical galaxy", 60000.0, 10.6, 12.7006389, "Galaxy" },
                    { "Messier", 60, null, "Virgo", 11.5525, "Elliptical galaxy", 55000.0, 9.8000000000000007, 12.7276667, "Galaxy" },
                    { "Messier", 61, null, "Virgo", 4.4736111000000003, "Spiral galaxy", 52500.0, 10.199999999999999, 12.36525, "Galaxy" },
                    { "Messier", 84, null, "Virgo", 12.886944400000001, "Lenticular galaxy", 60000.0, 10.1, 12.4176944, "Galaxy" },
                    { "Messier", 86, null, "Virgo", 12.9461111, "Lenticular galaxy", 52000.0, 9.8000000000000007, 12.436583300000001, "Galaxy" },
                    { "Messier", 87, "Virgo A", "Virgo", 12.3911233, "Elliptical galaxy", 53500.0, 9.5999999999999996, 12.5137287, "Galaxy" },
                    { "Messier", 89, null, "Virgo", 12.5563889, "Elliptical galaxy", 50000.0, 10.699999999999999, 12.5943889, "Galaxy" },
                    { "Caldwell", 19, "Cocoon Nebula", "Cygnus", 47.268333300000002, "Emission and reflection nebula with open cluster", 2.5, 7.2000000000000002, 21.892222199999999, "Nebula" },
                    { "Messier", 90, null, "Virgo", 13.162777800000001, "Spiral galaxy", 58700.0, 10.300000000000001, 12.6138333, "Galaxy" },
                    { "Caldwell", 52, null, "Virgo", -5.8008332999999999, "Elliptical galaxy", 44000.0, 10.9, 12.809972200000001, "Galaxy" },
                    { "Caldwell", 107, null, "Apus", -72.202194399999996, "Globular cluster", 49.899999999999999, 10.6, 16.430033300000002, "Star cluster" },
                    { "Caldwell", 81, null, "Ara", -48.422166699999998, "Globular cluster", 18.600000000000001, 7.7999999999999998, 17.4247528, "Star cluster" }
                });

            migrationBuilder.InsertData(
                table: "DsoItems",
                columns: new[] { "CatalogName", "Id", "Common", "ConstellationName", "Declination", "Description", "Distance", "Magnitude", "RightAscension", "Type" },
                values: new object[,]
                {
                    { "Caldwell", 82, null, "Ara", -48.763333299999999, "Open cluster with nebula", 3.7650000000000001, 5.2000000000000002, 16.688888899999998, "Star cluster" },
                    { "Caldwell", 86, null, "Ara", -53.674333300000001, "Globular cluster", 7.7999999999999998, 5.7000000000000002, 17.678358299999999, "Star cluster" },
                    { "Messier", 30, null, "Capricornus", -23.1798611, "Globular cluster", 29.399999999999999, 7.7000000000000002, 21.672811100000001, "Star cluster" },
                    { "Caldwell", 88, null, "Circinus", -55.625, "Open cluster", 3.887, 7.9000000000000004, 15.0957778, "Star cluster" },
                    { "Caldwell", 68, null, "Corona Australis", -36.953333299999997, "Emission and reflection nebula", 0.42399999999999999, 9.5, 19.031694399999999, "Nebula" },
                    { "Caldwell", 78, null, "Corona Australis", -43.714888899999998, "Globular cluster", 22.800000000000001, 6.2999999999999998, 18.133988899999999, "Star cluster" },
                    { "Messier", 29, "Cooling Tower", "Cygnus", 38.523333299999997, "Open cluster", 7.2000000000000002, 7.0999999999999996, 20.398888899999999, "Star cluster" },
                    { "Messier", 39, null, "Cygnus", 48.433333300000001, "Open cluster", 0.82440000000000002, 5.5, 21.5283333, "Star cluster" },
                    { "Messier", 104, "Sombrero Galaxy", "Virgo", -11.623055600000001, "Spiral galaxy", 29800.0, 9.0, 12.666499999999999, "Galaxy" },
                    { "Caldwell", 85, "Omicron Velorum Cluster", "Vela", -52.9166667, "Open cluster", 0.57399999999999995, 2.5, 8.6716666999999994, "Star cluster" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Constellations_SeasonId",
                table: "Constellations",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_DsoItems_ConstellationName",
                table: "DsoItems",
                column: "ConstellationName");

            migrationBuilder.CreateIndex(
                name: "IX_DsoItems_Type",
                table: "DsoItems",
                column: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DsoItems");

            migrationBuilder.DropTable(
                name: "Catalogs");

            migrationBuilder.DropTable(
                name: "Constellations");

            migrationBuilder.DropTable(
                name: "DsoTypes");

            migrationBuilder.DropTable(
                name: "Seasons");
        }
    }
}
