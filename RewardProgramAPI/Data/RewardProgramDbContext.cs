using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RewardProgramAPI.Models;

namespace RewardProgramAPI.Data
{
    /// <summary>
    /// Reward Program Databases Context using EF Core
    /// </summary>
    public class RewardProgramDbContext : DbContext
    {
        public RewardProgramDbContext(DbContextOptions<RewardProgramDbContext> dbContextOptions) : base(
            dbContextOptions)
        {
            // Ensure Database exists
            this.Database.EnsureCreated();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Many to Many Relation columns
            modelBuilder.Entity<ProductOrder>().HasKey(x => new {x.ProductId, x.OrderId});

            // Seeding some Initial Data
            modelBuilder.Entity<Customer>().HasData(
                new Customer() {Id = 1, Name = "Ashutosh Nigam"},
                new Customer() {Id = 2, Name = "John"});
            modelBuilder.Entity<Product>().HasData(
                new Product() {Id = 1, Name = "Toothpaste", Price = 40},
                new Product() {Id = 2, Name = "Soap", Price = 60},
                new Product() {Id = 3, Name = "Bag", Price = 350},
                new Product() {Id = 4, Name = "Water Bottle", Price = 20},
                new Product() {Id = 5, Name = "Rice Bag", Price = 400});
            modelBuilder.Entity<Order>().HasData(
                new Order() {Id = 1, DateTime = new DateTime(2021, 01, 01), CustomerId = 1, Points = 2230},
                new Order() {Id = 2, DateTime = new DateTime(2021, 02, 01), CustomerId = 2, Points = 490},
                new Order() {Id = 3, DateTime = new DateTime(2021, 01, 01), CustomerId = 1, Points = 650},
                new Order() {Id = 4, DateTime = new DateTime(2021, 01, 01), CustomerId = 1, Points = 90});
            modelBuilder.Entity<ProductOrder>().HasData(
                new ProductOrder() {OrderId = 1, ProductId = 1, Quantity = 1},
                new ProductOrder() {OrderId = 1, ProductId = 3, Quantity = 1},
                new ProductOrder() {OrderId = 1, ProductId = 5, Quantity = 2},
                new ProductOrder() {OrderId = 2, ProductId = 4, Quantity = 10},
                new ProductOrder() {OrderId = 2, ProductId = 2, Quantity = 2},
                new ProductOrder() {OrderId = 3, ProductId = 5, Quantity = 1},
                new ProductOrder() {OrderId = 4, ProductId = 2, Quantity = 2});

        }
    }
}