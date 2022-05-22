using System;
using System.Collections.Generic;

namespace RewardProgramAPI.Models
{
	public class Product
	{
		public Product()
		{
		}
        public int Id { get; set; }
        public string Name { get; set; }
		public virtual ICollection<ProductOrder> ProductOrders { get; set; }
	}
}

