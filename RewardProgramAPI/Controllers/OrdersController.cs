using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RewardProgramAPI.Data;
using RewardProgramAPI.Models;
using RewardProgramAPI.ViewModels;
using Order = RewardProgramAPI.Models.Order;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RewardProgramAPI.Controllers
{
    /// <summary>
    /// Orders Information
    /// </summary>
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly RewardProgramDbContext _context;

        public OrdersController(RewardProgramDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Get List of Orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public IEnumerable<ViewModels.Order> GetAll()
        {
            return _context.Orders.Include(x => x.ProductOrders).ThenInclude(x => x.Product).Select(x =>
                new ViewModels.Order()
                {
                    Id = x.Id,
                    Date = x.DateTime,
                    Customer = new ViewModels.Customer()
                    {
                        Id = x.Customer.Id,
                        Name = x.Customer.Name
                    },
                    Points = x.Points,
                    NoOfProducts = x.ProductOrders.Count(),
                    TotalAmount = x.ProductOrders.Sum(y => (int) (y.Quantity * y.Product.Price))
                }).ToList();
        }

        /// <summary>
        /// Get Single Order Info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ViewModels.Order),200)]
        [Produces("application/json")]
        public IActionResult Get(int id)
        {
            var order = _context.Orders.Include(x => x.ProductOrders).ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.Id == id);
            ViewModels.Order orderView = null;
            if (order != null)
                orderView = new ViewModels.Order()
                {
                    Id = order.Id,
                    Date = order.DateTime,
                    Points = order.Points,
                    NoOfProducts = order.ProductOrders.Count(),
                    TotalAmount = order.ProductOrders.Sum(y => (int) (y.Quantity * y.Product.Price)),
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
        /// <summary>
        /// Place a New Purchase Order
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ViewModels.Order),200)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult Post([Bind] NewOrder newOrder)
        {
            if (ModelState.IsValid)
            {
                var customer = _context.Customers.FirstOrDefault(x => x.Id == newOrder.CustomerId);
                if (customer != null)
                {
                    var products = newOrder.Products.Select(x => x.Id).Except(_context.Products.Select(x => x.Id));
                    if (products.Count() !=0)
                        return NotFound($"Some Products are not available! Please check");
                    var productOrders = new List<ProductOrder>();
                    decimal totalPurchase = 0.0m;
                    int totalPoints = 0;
                    var order = new Order()
                    {
                        DateTime = DateTime.Now,
                        CustomerId = customer.Id
                    };
                    _context.Orders.Add(order);
                    foreach (var prod in newOrder.Products)
                    {
                        var product = _context.Products.FirstOrDefault(x => x.Id == prod.Id);
                        productOrders.Add(new ProductOrder()
                        {
                            ProductId = prod.Id,
                            Quantity = prod.Quantity,
                            Order= order
                        });
                        totalPurchase += (prod.Quantity * product.Price);
                    }

                    
                    if (totalPurchase > 50)
                        totalPoints += (int) (totalPurchase - 50);
                    if (totalPurchase > 100)
                        totalPoints += (int) (totalPurchase - 100);
                    order.Points = totalPoints;
                    order.ProductOrders = productOrders;
                    _context.ProductOrders.AddRange(productOrders);
                    _context.SaveChanges();
                    return RedirectToAction("Get", new {id=order.Id});
                }
                else
                {
                    return NotFound($"Customer with Id {newOrder.CustomerId.ToString()} Not available.");
                }
            }
            return BadRequest("Invalid Data");
        }
    }
}