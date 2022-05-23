using System;
using System.Collections;
using System.Collections.Generic;

namespace RewardProgramAPI.ViewModels;

public class CustomerWithRewardPoints : Customer
{
    public int TotalPoints { get; set; }
    public IList<OrderDetail> OrderDetails { get; set; }
}

public class OrderDetail
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int Points { get; set; }
}