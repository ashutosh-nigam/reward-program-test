using System;
using System.Collections.Generic;

namespace RewardProgramAPI.Models
{
	public class Order
	{
		public Order()
		{
		}
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public Customer Customer { get; set; }
        public int Points { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }

}

