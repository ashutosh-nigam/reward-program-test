using System;
using Microsoft.EntityFrameworkCore;
using RewardProgramAPI.Models;

namespace RewardProgramAPI.Data
{
	public class RewardProgramDbContext : DbContext
	{
		public RewardProgramDbContext(DbContextOptions<RewardProgramDbContext> dbContextOptions):base(dbContextOptions) 
		{
		}

		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductOrder> ProductOrders { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductOrder>().HasKey(x => new {x.ProductId, x.OrderId});
		}
	}
}

