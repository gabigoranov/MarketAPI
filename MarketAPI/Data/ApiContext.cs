using MarketAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Reflection.Emit;

namespace MarketAPI.Data
{
    public class ApiContext: DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) :base(options) 
        {
            //Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferType> OfferTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>(entity =>
            {
                entity.HasOne(u => u.Buyer)
                    .WithMany(u => u.BoughtOrders)
                    .HasForeignKey(u => u.BuyerId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(n => n.Seller)
                    .WithMany(u => u.SoldOrders)
                    .HasForeignKey(n => n.SellerId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(n => n.Offer)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(n => n.OfferId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<Offer>().HasOne(x => x.Owner).WithMany(x => x.Offers).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.Entity<Offer>().HasOne(x => x.OfferType);
            builder.Entity<Offer>().Navigation(x => x.Owner).AutoInclude(true);
            
            builder.Entity<Offer>().Navigation(x => x.Orders).AutoInclude(true);
            
            builder.Entity<User>().HasMany(x => x.Offers).WithOne(x => x.Owner).OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<User>().Navigation(x => x.Offers).AutoInclude(true);
            
            builder.Entity<User>().Property(typeof(double), "Rating");

            

            base.OnModelCreating(builder);
        }
    }

}
