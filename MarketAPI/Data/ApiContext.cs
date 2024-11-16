﻿using MarketAPI.Data.Models;
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
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Purchase>().HasMany(x => x.Orders);
            builder.Entity<Purchase>().HasOne(x => x.Buyer).WithMany(x => x.BoughtPurchases).HasForeignKey(x => x.BuyerId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Offer>().HasMany(x => x.Reviews).WithOne(x => x.Offer).HasForeignKey(x => x.OfferId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Offer>().HasOne(x => x.Stock).WithMany(x => x.Offers).HasForeignKey(x => x.StockId).IsRequired().OnDelete(DeleteBehavior.Restrict);

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
            builder.Entity<Offer>().Navigation(x => x.Owner);
            builder.Entity<Offer>().Navigation(x => x.Stock).AutoInclude(true);
            builder.Entity<Stock>().Navigation(x => x.OfferType).AutoInclude(true);
            builder.Entity<Offer>().Navigation(x => x.Reviews).AutoInclude(true);


            
            builder.Entity<Seller>().HasMany(x => x.Offers).WithOne(x => x.Owner).OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Seller>().Navigation(x => x.Offers).AutoInclude(true);
            builder.Entity<Seller>().Navigation(x => x.SoldOrders).AutoInclude(true); //
            
            builder.Entity<User>().Property(typeof(double), "Rating");
            builder.Entity<User>()
                .HasDiscriminator<bool>(x => x.isSeller)
                .HasValue<User>(false)
                .HasValue<Seller>(true);

            

            base.OnModelCreating(builder);
        }
    }

}
