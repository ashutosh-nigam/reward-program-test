using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RewardProgramAPI.Controllers;
using RewardProgramAPI.Data;
using Xunit;

namespace RewardProgramAPI.Tests.Controllers;

public class ProductsControllerTests
{
    private readonly ProductsController _productsController;
    private readonly RewardProgramDbContext _rewardProgramDbContext;
    public ProductsControllerTests()
    {
        _rewardProgramDbContext = TestData.GetDbContext();
        _productsController = new ProductsController(_rewardProgramDbContext);
    }
    [Fact]
    public void WhenGetAllMethodCalledShouldReturnListOfProducts()
    {
        var listOfProducts = _productsController.GetAll();
        Assert.True(listOfProducts.Count()==5);
    }

}