using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RewardProgramAPI.Data;
using RewardProgramAPI.ViewModels;

namespace RewardProgramAPI.Controllers;
/// <summary>
/// Reward Points Information
/// </summary>
[ApiController]
[Route("rewardpoints")]
public class RewardPointsController : ControllerBase
{
    private readonly RewardProgramDbContext _context;

    public RewardPointsController(RewardProgramDbContext context)
    {
        this._context = context;
    }
    /// <summary>
    /// Get List of All Reward Points with Customer details
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    public IEnumerable<RewardPoint> GetAll()
    {
        return _context.ProductOrders.GroupBy(x => x.Order.CustomerId)
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
    /// <returns>Customer Information with Reward Points</returns>
    [HttpGet("{customerId}")]
    [ProducesResponseType(typeof(string),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IEnumerable<CustomerWithRewardPoints>),200)]
    [Produces("application/json")]
    public IActionResult Get(int customerId)
    {
        var customer = _context.Customers.Include(x=>x.Orders).FirstOrDefault(x => x.Id == customerId);
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