using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RewardProgramAPI.Data;
using RewardProgramAPI.ViewModels;
using RewardProgramAPI.ViewModels.V2;

namespace RewardProgramAPI.Controllers.V2;


/// <summary>
/// Customers 2.0 Endpoint with newer details. V2
/// </summary>
[ApiController]
[ApiVersion("2.0")]
[Route("customers")]
public class CustomersController : ControllerBase
{
    private readonly RewardProgramDbContext _context;
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="context"></param>
    public CustomersController(RewardProgramDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get Customers basic information like Id, Name, points earned in last 3 months etc.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    public IEnumerable<ViewModels.V2.CustomerV2> GetALL()
    {
        var customers = _context.Customers.Include(x => x.Orders)
            .Select(cust => new ViewModels.V2.CustomerV2()
            {
                Id = cust.Id,
                Name = cust.Name,
                TotalPointsEarned = cust.Orders.Where(ord => ord.DateTime > DateTime.Now.AddMonths(-3)).Sum(x => x.Points)
            }).ToList();
        foreach (var customer in customers)
        {
            customer.PointsEarnedMonthly = _context.Orders.Where(x => x.CustomerId == customer.Id)
                .Where(ord => ord.DateTime > DateTime.Now.AddMonths(-3)).ToList()
                .GroupBy(x => x.DateTime.ToString("MMM")).Select(x => new {key = x.Key, val = x.Sum(y => y.Points)})
                .ToDictionary(x => x.key.ToString(), y => y.val);
        }
        return customers;
    }
    /// <summary>
    /// Get Individual Customer Information
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(string),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ViewModels.V2.CustomerWithRewardPointsV2),200)]
    [Produces("application/json")]
    public IActionResult Get(int id)
    {
        var customer = _context.Customers.Where(x => x.Id == id).Include(x => x.Orders).FirstOrDefault();
        if (customer != null)
        {
            var customerViewModel = new ViewModels.V2.CustomerWithRewardPointsV2()
            {
                Id = customer.Id,
                Name = customer.Name,
                TotalPointsEarned = customer.Orders.Where(order => order.DateTime > DateTime.Now.AddMonths(-3))
                    .Sum(x => x.Points)
            };
            var orderInfo = customer.Orders.Where(order => order.DateTime > DateTime.Now.AddMonths(-3)).ToList();
            var monthlyOrdersColl = orderInfo.GroupBy(x => x.DateTime.ToString("MMM"))
                .Select(x => new
                {
                    Key = x.Key, orderInfo = x.Select(y => y)
                }).ToList();
            List<MonthlyOrder> monthlyOrders = new List<MonthlyOrder>();
            foreach (var monthlyOrderCol in monthlyOrdersColl)
            {
                var m= monthlyOrderCol.orderInfo.Select(x => new ViewModels.OrderDetail()
                {
                    Id = x.Id,
                    OrderDate = x.DateTime,
                    Points = x.Points
                });
                monthlyOrders.Add(new MonthlyOrder()
                {
                     MonthName = monthlyOrderCol.Key,
                     OrderDetails = m.ToList()
                });
            }
            customerViewModel.MonthlyOrders = monthlyOrders;
            return Ok(customerViewModel);
        }
        else
        {
            return NotFound($"Customer with Id : {id.ToString()}  Not Found");
        }
    }
}