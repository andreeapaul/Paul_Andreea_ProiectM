using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Paul_Andreea_Proiect.Models;

namespace Paul_Andreea_Proiect.Data
{
    public class AutoTraderContext : DbContext
    {
        public AutoTraderContext(DbContextOptions<AutoTraderContext> options) :
       base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SoldCar> SoldCars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Car>().ToTable("Car");
            modelBuilder.Entity<Seller>().ToTable("Seller");
            modelBuilder.Entity<SoldCar>().ToTable("SoldCar");
            modelBuilder.Entity<SoldCar>() .HasKey(c => new { c.CarID, c.SellerID });
           
        }
    }
}
