using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RewardProgramAPI.Data;
using RewardProgramAPI.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RewardProgramAPI.Controllers
{
    /// <summary>
    /// Customers Information
    /// </summary>
    public class CustomersController : Controller
    {
        RewardProgramDbContext context;
        public CustomersController(RewardProgramDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Get List of All Customers with Name and Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("customers/all")]
        public IActionResult GetAll()
        {
            return Ok(context.Customers.Select(x=> new Customer()
            {
                Id=x.Id,
                Name = x.Name
            }));
        }

        /// <summary>
        /// Get Individual Customer Information including orders he did. using customer id
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns></returns>
        [HttpGet("customers/{id}")]
        public IActionResult Get(int id)
        {
            var customer = context.Customers.Include(x=>x.Orders).FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return NotFound($"Customer with Id :{id.ToString()} Not Found.");
            }

            var customerView = new CustomerWithRewardPoints()
            {
                Id = customer.Id,
                Name = customer.Name,
                TotalPoints = customer.Orders.Sum(x => x.Points),
                OrderDetails = customer.Orders.Select(x => new OrderDetail()
                {
                    Id = x.Id,
                    Points = x.Points,
                    OrderDate = x.DateTime
                }).ToList()
            };
            return Ok(customerView);
        }
        
    }
}

