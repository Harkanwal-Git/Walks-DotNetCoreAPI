using Microsoft.EntityFrameworkCore;
using Walks.API.Models.Domain;



namespace Walks.API.Data
{
    public class WalksDBContext : DbContext
    {
        public WalksDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Region>().HasData(
                new Region {Id=Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name="Mississauga" ,Code="MIS"},
                new Region { Id=Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name="Toronto",Code="TOR",RegionImageUrl= "https://upload.wikimedia.org/wikipedia/commons/a/a1/Hillside_Gardens_%2824677009128%29.jpg" } 
                );
        }

    }
}
