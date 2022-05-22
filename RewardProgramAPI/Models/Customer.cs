using System;
using System.ComponentModel.DataAnnotations;

namespace RewardProgramAPI.Models
{
	public class Customer
	{
		public Customer()
		{

           
		}
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
    }
}

