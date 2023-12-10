using MarketAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Data
{
    public class ApiContext: DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) :base(options) 
        {
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Offer> Offers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Offer>().HasOne(x => x.Owner).WithMany(x => x.Offers).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.Entity<Offer>().Navigation(x => x.Owner).AutoInclude(true);
            builder.Entity<User>().HasMany(x => x.Offers).WithOne(x => x.Owner).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<User>().Navigation(x => x.Offers).AutoInclude(true);
            base.OnModelCreating(builder);
        }
    }

}
