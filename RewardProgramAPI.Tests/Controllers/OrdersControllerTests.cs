using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RewardProgramAPI.Controllers;
using RewardProgramAPI.Data;
using RewardProgramAPI.ViewModels;
using Xunit;

namespace RewardProgramAPI.Tests.Controllers;

public class OrdersControllerTests
{
    private readonly OrdersController _ordersController;
    private readonly RewardProgramDbContext _rewardProgramDbContext;
    public OrdersControllerTests()
    {
        _rewardProgramDbContext = TestData.GetDbContext();
        _ordersController = new OrdersController(_rewardProgramDbContext);
    }
    [Fact]
    public void WhenGetAllMethodCalledShouldReturnListOfAllOrdersWithProductDetails()
    {
        var listOfOrders = _ordersController.GetAll();
        var ordersCount = _rewardProgramDbContext.Orders.Count();
        Assert.True(listOfOrders.Count()==ordersCount);
    }

    [Fact]
    public void WhenGetMethodCalledShouldReturnSingleOrderWithOtherDetails()
    {
        var objectResult = _ordersController.Get(1) as OkObjectResult ;
        
        Assert.NotNull(objectResult);
        Assert.Equal(200,objectResult.StatusCode);
        var order = objectResult.Value as ViewModels.Order;
        Assert.Equal("Ashutosh Nigam",order.Customer.Name );
        Assert.Equal(3,order.NoOfProducts);
    }
    
    [Fact]
    public void WhenGetMethodCalledAndOrderDoesNotExistsShouldReturnNotFound()
    {
        var objectResult = _ordersController.Get(5) as NotFoundObjectResult ;
        Assert.NotNull(objectResult);
        Assert.Equal(404,objectResult.StatusCode);
    }

    [Fact]
    public void WhenPostMethodCalledAndModelIsValidShouldSaveDataAndReturnOrderResponse()
    {
        var newOrder = new ViewModels.NewOrder()
        {
            CustomerId = 2,
            Products = new List<ProductInfo>()
            {
                new ProductInfo() {Id = 3, Quantity = 2}
            }
        };
        var objectResult = _ordersController.Post(newOrder) as OkObjectResult;
        Assert.NotNull(objectResult);
        Assert.Equal(200,objectResult.StatusCode);
        var order = objectResult.Value as ViewModels.Order;
        Assert.Equal("John",order.Customer.Name );
        Assert.Equal(1,order.NoOfProducts);
        Assert.Equal(1250,order.Points);
    }
    
    [Fact]
    public void WhenPostMethodCalledAndCustomerNotExistShouldReturnNotFound()
    {
        var newOrder = new ViewModels.NewOrder()
        {
            CustomerId = 4,
            Products = new List<ProductInfo>()
            {
                new ProductInfo() {Id = 3, Quantity = 2}
            }
        };
        var objectResult = _ordersController.Post(newOrder) as NotFoundObjectResult;
        Assert.NotNull(objectResult);
        Assert.Equal(404, objectResult.StatusCode);
        Assert.Equal("Customer with Id 4 Not available.",objectResult.Value);
    }
    
    [Fact]
    public void WhenPostMethodCalledAndProductNotExistShouldReturnNotFound()
    {
        var newOrder = new ViewModels.NewOrder()
        {
            CustomerId = 2,
            Products = new List<ProductInfo>()
            {
                new ProductInfo() {Id = 7, Quantity = 2}
            }
        };
        var objectResult = _ordersController.Post(newOrder) as NotFoundObjectResult;
        Assert.NotNull(objectResult);
        Assert.Equal(404, objectResult.StatusCode);
        Assert.Equal("Some Products are not available! Please check",objectResult.Value);
       
    }
}