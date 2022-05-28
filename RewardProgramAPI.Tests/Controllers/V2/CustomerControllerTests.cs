using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RewardProgramAPI.Controllers.V2;
using RewardProgramAPI.Data;
using RewardProgramAPI.ViewModels;
using Xunit;

namespace RewardProgramAPI.Tests.Controllers.V2;

public class CustomerControllerTests
{
    private readonly CustomersController _customersController;
    private readonly RewardProgramDbContext _rewardProgramDbContext;
    public CustomerControllerTests()
    {
        _rewardProgramDbContext = TestData.GetDbContext();
        _customersController = new CustomersController(_rewardProgramDbContext);
    }
    [Fact]
    public void WhenGetAllMethodCalledShouldReturnListOfCustomers()
    {
        var listOfCustomers = _customersController.GetAll();
        Assert.True(listOfCustomers.Count()==2);
    }

    [Fact]
    public void WhenGetMethodCalledShouldReturnSingleCustomerWithOtherDetails()
    {
        var objectResult = _customersController.Get(1) as OkObjectResult ;
        
        Assert.NotNull(objectResult);
        Assert.Equal(200,objectResult.StatusCode);
        var customer = objectResult.Value as ViewModels.V2.CustomerWithRewardPointsV2;
        Assert.Equal("Ashutosh Nigam",customer.Name );
    }
    
    [Fact]
    public void WhenGetMethodCalledAndCustomerDoesNotExistsShouldReturnNotFound()
    {
        var objectResult = _customersController.Get(5) as NotFoundObjectResult ;
        Assert.NotNull(objectResult);
        Assert.Equal(404,objectResult.StatusCode);
    }
}