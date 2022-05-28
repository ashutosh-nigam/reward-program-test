using System;
using System.Collections;
using System.Collections.Generic;

namespace RewardProgramAPI.ViewModels;
/// <summary>
/// Customer Model Extension for Getting Info with Reward Points and order details.
/// </summary>
public class CustomerWithRewardPoints : Customer
{
    /// <summary>
    /// Total Points Earned So far.
    /// </summary>
    public int TotalPoints { get; set; }
    /// <summary>
    /// List of Orders
    /// </summary>
    public IList<OrderDetail> OrderDetails { get; set; }
}

/// <summary>
/// Orders Details View Model
/// </summary>
public class OrderDetail
{
    /// <summary>
    /// Order Id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Order Datetime
    /// </summary>
    public DateTime OrderDate { get; set; }
    /// <summary>
    /// Points earned in every order
    /// </summary>
    public int Points { get; set; }
}