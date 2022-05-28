using System;
using System.Collections.Generic;

namespace RewardProgramAPI.ViewModels;
/// <summary>
/// Order View Model to display Order Information
/// </summary>
public class Order
{
    /// <summary>
    /// Order Id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Order Datetime
    /// </summary>
    public DateTime Date { get; set; }
    /// <summary>
    /// Points Earned in this Order
    /// </summary>
    public int Points { get; set; }
    /// <summary>
    /// No of Products ordered
    /// </summary>
    public int NoOfProducts { get; set; }
    /// <summary>
    /// Total Amount of Order
    /// </summary>
    public decimal TotalAmount { get; set; }
    /// <summary>
    /// Customer Information
    /// </summary>
    public ViewModels.Customer Customer { get; set; }
    /// <summary>
    /// List of Orders
    /// </summary>
    public List<ViewModels.Product> Products { get; set; }
}