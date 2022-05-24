using Microsoft.EntityFrameworkCore;
using RewardProgramAPI.Data;

namespace RewardProgramAPI.Tests;

public static class TestData
{
    public static RewardProgramDbContext GetDbContext()
    {
        var contextOptions = new DbContextOptionsBuilder<RewardProgramDbContext>()
            .UseInMemoryDatabase(databaseName: "MovieListDatabase")
            .Options;


        RewardProgramDbContext rewardProgramDbContext = new RewardProgramDbContext(contextOptions);
        return rewardProgramDbContext;
    }
}