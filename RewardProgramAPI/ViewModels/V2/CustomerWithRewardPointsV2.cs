using System.Collections.Generic;

namespace RewardProgramAPI.ViewModels.V2;
/// <summary>
/// Get individual Customer with Order Information 
/// </summary>
public class CustomerWithRewardPointsV2 : CustomerV2
{
    /// <summary>
    /// List of Orders Monthly
    /// </summary>
    public List<MonthlyOrder> MonthlyOrders { get; set; }
    
}
/// <summary>
/// Monthly Orders
/// </summary>
public class MonthlyOrder
{
    /// <summary>
    /// Name of the Month
    /// </summary>
    public string MonthName { get; set; }
    /// <summary>
    /// Order Information
    /// </summary>
    public IList<OrderDetail> OrderDetails { get; set; }
}