using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RewardProgramAPI.Models
{
	/// <summary>
	/// Products Class Model and Table
	/// </summary>
	public class Product
	{
		public Product()
		{
		}
		[Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
		[Required]
        public decimal Price { get; set; }
		public ICollection<ProductOrder> ProductOrders { get; set; }
	}
}

