using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RewardProgramAPI.Data;
using RewardProgramAPI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RewardProgramAPI.Controllers
{
    [ApiController]
    [Route("purchase")]
    public class PurchaseController : ControllerBase
    {
        private RewardProgramDbContext context;

        public PurchaseController(RewardProgramDbContext context)
        {
            this.context = context;
        }
        // GET: /<controller>/
        [HttpGet]
        public IEnumerable<Order> Index()
        {

            return context.Orders.ToList();
        }
    }
}

