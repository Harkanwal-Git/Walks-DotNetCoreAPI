using Microsoft.EntityFrameworkCore;
using Walks.API.Models.Domain;



namespace Walks.API.Data
{
    public class WalksDBContext : DbContext
    {
        public WalksDBContext(DbContextOptions<WalksDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Seed data for regions
            modelBuilder.Entity<Region>().HasData(
                new Region {Id=Guid.Parse("45cb6417-c2b3-4937-9916-11341a49a08a"),
                    Name="Mississauga" ,Code="MIS"},
                new Region { Id=Guid.Parse("02a69f01-994f-4ac6-b293-bb14ff574f81"),
                    Name="Toronto",Code="TOR",RegionImageUrl= "https://upload.wikimedia.org/wikipedia/commons/a/a1/Hillside_Gardens_%2824677009128%29.jpg" } 
                );

            var difficulties = new List<Difficulty>() {   new Difficulty
                {
                    Id = Guid.Parse("05b87b94-b740-431c-9f80-1ebd82ada49e"),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("95ef9256-0d28-4bac-ad4d-b2c15267cd5d"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("832e0765-0f4a-43a0-b098-7349f0ba2c01"),
                    Name = "Hard"
                }};
            //Seed data for difficulties
            modelBuilder.Entity<Difficulty>().HasData(difficulties);
        }

    }
}
