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

        public DbSet<OfferType> OfferTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OfferType>().HasData(
                new OfferType(){
                    Id = 1,
                    Name = "Apple",
                },
                new OfferType()
                {
                    Id = 2,
                    Name = "Pear",
                },
                new OfferType()
                {
                    Id = 3,
                    Name = "Pumpkin",
                },
                new OfferType()
                {
                    Id = 4,
                    Name = "Tomato",
                },
                new OfferType()
                {
                    Id = 5,
                    Name = "Egg",
                },
                new OfferType()
                {
                    Id = 6,
                    Name = "Milk",
                },
                new OfferType()
                {
                    Id = 7,
                    Name = "Honey",
                });

            builder.Entity<Offer>().HasOne(x => x.Owner).WithMany(x => x.Offers).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.Entity<Offer>().Navigation(x => x.Owner).AutoInclude(true);
            builder.Entity<User>().HasMany(x => x.Offers).WithOne(x => x.Owner).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<User>().Navigation(x => x.Offers).AutoInclude(true);
            base.OnModelCreating(builder);
        }
    }

}
