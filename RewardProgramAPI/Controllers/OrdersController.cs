using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RewardProgramAPI.Data;
using RewardProgramAPI.Models;
using RewardProgramAPI.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RewardProgramAPI.Controllers
{
    [ApiController]
  
    public class OrdersController : ControllerBase
    {
        private RewardProgramDbContext context;

        public OrdersController(RewardProgramDbContext context)
        {
            this.context = context;
        }
        
        /// <summary>
        /// Get List of Orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("orders/all")]
        public IEnumerable<ViewModels.Order> GetAll()
        {
            return context.Orders.Include(x => x.ProductOrders).ThenInclude(x=>x.Product).Select(x=> new ViewModels.Order()
            {
                Id= x.Id,
                Date = x.DateTime,
                Customer = new ViewModels.Customer()
                {
                    Id= x.Customer.Id,
                    Name=x.Customer.Name
                },
                Points = x.Points,
                NoOfProducts = x.ProductOrders.Count(),
                TotalAmount = x.ProductOrders.Sum(y=> (int)(y.Quantity* y.Product.Price))
            }).ToList();
        }
        
        /// <summary>
        /// Get Single Order Info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("orders/{id}")]
        public IActionResult Get(int id)
        {
            var order= context.Orders.Include(x => x.ProductOrders).ThenInclude(x=>x.Product).FirstOrDefault(x => x.Id == id);
            ViewModels.Order orderView = null;
            if (order != null)
                orderView = new ViewModels.Order()
                {
                    Id = order.Id,
                    Date = order.DateTime,
                    Points = order.Points,
                    NoOfProducts = order.ProductOrders.Count(),
                    TotalAmount = order.ProductOrders.Sum(y=> (int)(y.Quantity * y.Product.Price)),
                    Products = order.ProductOrders.Select(y => new ViewModels.Product()
                    {
                        Id = y.Product.Id,
                        Name = y.Product.Name,
                        Quantity = y.Quantity,
                        Price = y.Product.Price
                    }).ToList()
                };
            return order != null ? Ok(orderView) : NotFound($"Order with Id: {id.ToString()} not found.");
        }
        
        
        
    }
}

