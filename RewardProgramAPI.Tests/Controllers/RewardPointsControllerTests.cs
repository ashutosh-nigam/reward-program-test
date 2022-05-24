using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RewardProgramAPI.Controllers;
using RewardProgramAPI.Data;
using Xunit;

namespace RewardProgramAPI.Tests.Controllers;

public class RewardPointsControllerTests
{
    private readonly RewardPointsController _rewardPointsController;
    private readonly RewardProgramDbContext _rewardProgramDbContext;
    public RewardPointsControllerTests()
    {
        _rewardProgramDbContext = TestData.GetDbContext();
        _rewardPointsController = new RewardPointsController(_rewardProgramDbContext);
    }
    [Fact]
    public void WhenGetAllMethodCalledShouldReturnListOfTotalPointsWithCustomerDetails()
    {
        var listOfPoints = _rewardPointsController.GetAll();
        Assert.True(listOfPoints.Count()==2);
    }

    [Fact]
    public void WhenGetMethodCalledShouldReturnSingleCustomerWithOtherDetails()
    {
        var objectResult = _rewardPointsController.Get(1) as OkObjectResult ;
        
        Assert.NotNull(objectResult);
        Assert.Equal(200,objectResult.StatusCode);
        var customer = objectResult.Value as ViewModels.CustomerWithRewardPoints;
        Assert.Equal("Ashutosh Nigam",customer.Name );
    }
    
    [Fact]
    public void WhenGetMethodCalledAndCustomerDoesNotExistsShouldReturnNotFound()
    {
        var objectResult = _rewardPointsController.Get(5) as NotFoundObjectResult ;
        Assert.NotNull(objectResult);
        Assert.Equal(404,objectResult.StatusCode);
    }
}