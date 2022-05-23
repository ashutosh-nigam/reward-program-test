using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    [ApiController]
    [Route("customers")]
    public class CustomersController : ControllerBase
    {
        private readonly RewardProgramDbContext _context;
        public CustomersController(RewardProgramDbContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// Get List of All Customers with Name and Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public IEnumerable<ViewModels.Customer> GetAll()
        {
            return _context.Customers.Select(x=> new Customer()
            {
                Id=x.Id,
                Name = x.Name
            }).AsEnumerable();
        }

        /// <summary>
        /// Get Individual Customer Information including orders he did. using customer id
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ViewModels.CustomerWithRewardPoints),200)]
        [Produces("application/json")]
        public IActionResult Get(int id)
        {
            var customer = _context.Customers.Include(x=>x.Orders).FirstOrDefault(x => x.Id == id);
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

