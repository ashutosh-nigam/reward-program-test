using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RewardProgramAPI.Models
{
	/// <summary>
	/// Customer Details
	/// </summary>
	public class Customer
	{
		public Customer()
		{
		}
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }

		public virtual ICollection<Order> Orders { get; set; }
    }
}

