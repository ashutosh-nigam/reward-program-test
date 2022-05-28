using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RewardProgramAPI.Data;

namespace RewardProgramAPI.Controllers;

/// <summary>
/// Products Information
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly RewardProgramDbContext _context;

    public ProductsController(RewardProgramDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Get List of All Products and their details
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType( typeof(IEnumerable<ViewModels.Product>),StatusCodes.Status200OK)]
    [Produces("application/json")]
    public IEnumerable<ViewModels.Product> GetAll()
    {
        return _context.Products.Select(x => new ViewModels.Product()
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price
        });
    }
}