using System;
using System.Collections;
using System.Collections.Generic;

namespace RewardProgramAPI.ViewModels;
/// <summary>
/// Customer Model Extension for Getting Info with Reward Points and order details.
/// </summary>
public class CustomerWithRewardPoints : Customer
{
    public int TotalPoints { get; set; }
    public IList<OrderDetail> OrderDetails { get; set; }
}

/// <summary>
/// Orders Details View Model
/// </summary>
public class OrderDetail
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int Points { get; set; }
}