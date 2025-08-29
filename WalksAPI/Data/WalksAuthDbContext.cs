using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Walks.API.Data
{
    public class WalksAuthDbContext:IdentityDbContext
    {
        public WalksAuthDbContext(DbContextOptions<WalksAuthDbContext> dbContextOptions):base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "73ddfcb1-e8b8-43be-854a-150a5a37d176";
            var writerRoleId = "b2c834af-0615-4489-a6ac-4c217013eb74";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id=writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

        }

    }
}
