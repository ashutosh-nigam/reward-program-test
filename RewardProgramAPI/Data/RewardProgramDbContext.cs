using System;
using Microsoft.EntityFrameworkCore;
using RewardProgramAPI.Models;

namespace RewardProgramAPI.Data
{
	/// <summary>
	/// Reward Program Databases Context using EF Core
	/// </summary>
	public class RewardProgramDbContext : DbContext
	{
		public RewardProgramDbContext(DbContextOptions<RewardProgramDbContext> dbContextOptions):base(dbContextOptions) 
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
			
			base.OnModelCreating(modelBuilder);
		}
	}
}

