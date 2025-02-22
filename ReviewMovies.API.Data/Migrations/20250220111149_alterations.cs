using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReviewMovie.API.Migrations
{
    /// <inheritdoc />
    public partial class alterations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, "A story of hope and friendship.", "Drama", 1994, "The Shawshank Redemption" },
                    { 2, "An iconic mafia story.", "Drama", 1972, "The Godfather" },
                    { 3, "Batman faces the Joker.", "Drama", 2008, "The Dark Knight" },
                    { 4, "Interwoven crime stories.", "Drama", 1994, "Pulp Fiction" },
                    { 5, "Life seen through Forrest's eyes.", "Drama", 1994, "Forrest Gump" },
                    { 6, "A journey into dreams.", "Drama", 2010, "Inception" },
                    { 7, "A psychological thriller.", "Drama", 1999, "Fight Club" },
                    { 8, "Reality is not what it seems.", "Drama", 1999, "The Matrix" },
                    { 9, "A journey beyond the stars.", "Drama", 2014, "Interstellar" },
                    { 10, "The final battle for Middle-earth.", "Drama", 2003, "The Lord of the Rings: The Return of the King" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "MovieId", "Rating", "ReviewerName" },
                values: new object[,]
                {
                    { 1, "An unforgettable masterpiece.", 1, 7, "Alice" },
                    { 2, "A deeply moving story of hope.", 1, 9, "Bob" },
                    { 3, "The greatest crime film ever made.", 2, 5, "Charlie" },
                    { 4, "A timeless classic with incredible performances.", 2, 6, "David" },
                    { 5, "Heath Ledger's Joker is legendary.", 3, 10, "Eve" },
                    { 6, "A dark and thrilling superhero film.", 3, 4, "Frank" },
                    { 7, "Quentin Tarantino at his finest.", 4, 8, "Grace" },
                    { 8, "Brilliant dialogues and unforgettable characters.", 4, 5, "Hank" },
                    { 9, "A heartwarming tale of an extraordinary life.", 5, 10, "Ivy" },
                    { 10, "Tom Hanks delivers a phenomenal performance.", 5, 6, "Jack" },
                    { 11, "A mind-bending sci-fi thriller.", 6, 9, "Karen" },
                    { 12, "Nolan's masterpiece with stunning visuals.", 6, 7, "Leo" },
                    { 13, "A gripping and thought-provoking film.", 7, 10, "Mia" },
                    { 14, "A mind-blowing twist and brilliant acting.", 7, 9, "Nate" },
                    { 15, "A sci-fi revolution in filmmaking.", 8, 5, "Olivia" },
                    { 16, "An iconic action-packed adventure.", 8, 9, "Paul" },
                    { 17, "A visually stunning and emotional journey.", 9, 9, "Quinn" },
                    { 18, "One of the best space movies ever made.", 9, 9, "Rachel" },
                    { 19, "An epic conclusion to an incredible trilogy.", 10, 6, "Steve" },
                    { 20, "Visually breathtaking and emotionally powerful.", 10, 9, "Tina" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieId",
                table: "Reviews",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
