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
    public class CustomerController : Controller
    {
        RewardProgramDbContext context;
        public CustomerController(RewardProgramDbContext context)
        {
            this.context = context;
        }

        [HttpGet("customers/all")]
        public IActionResult GetAll()
        {
            return Ok(context.Customers.Select(x=> new Customer()
            {
                Id=x.Id,
                Name = x.Name
            }));
        }

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

