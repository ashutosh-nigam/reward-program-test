using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RewardProgramAPI.Models
{
	/// <summary>
	/// Orders Table and Model
	/// </summary>
	public class Order
	{
		public Order()
		{
		}
		[Key]
        public int Id { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required] 
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required, DefaultValue(0)]
        public int Points { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }
    }

}

