using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RewardProgramAPI.Data;
using RewardProgramAPI.ViewModels;

namespace RewardProgramAPI.Controllers;

[ApiController]
public class RewardPointsController : ControllerBase
{
    private RewardProgramDbContext context;

    public RewardPointsController(RewardProgramDbContext context)
    {
        this.context = context;
    }
    [HttpGet("rewardpoints/all")]
    public IEnumerable<RewardPoint> GetAll()
    {
        return context.ProductOrders.GroupBy(x => x.Order.CustomerId)
            .Select(x => new {x.First().Order.Customer, Total = x.Sum(y => y.Order.Points)}).ToList()
            .Select(x => new RewardPoint()
            {
                TotalRewardPoints = x.Total,
                Customer = new Customer()
                {
                    Id = x.Customer.Id,
                    Name = x.Customer.Name
                }
            }).AsEnumerable();
    }
    
    /// <summary>
    /// Get Reward Points for Particular Customer
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [HttpGet("rewardpoints/{customerId}")]
    public IActionResult Get(int customerId)
    {
        var customer = context.Customers.Include(x=>x.Orders).FirstOrDefault(x => x.Id == customerId);
        if (customer == null)
        {
            return NotFound($"Customer with Id :{customerId.ToString()} Not Found.");
        }

        var customerView = new CustomerWithRewardPoints()
        {
            Id = customer.Id,
            Name = customer.Name,
            TotalPoints = customer.Orders.Sum(x => x.Points)
        };
        return Ok(customerView);
    }
}