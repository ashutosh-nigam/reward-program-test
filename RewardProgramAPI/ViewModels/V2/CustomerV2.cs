using System.Collections.Generic;
using System.Reflection.Emit;

namespace RewardProgramAPI.ViewModels.V2;

/// <summary>
/// Get Customer Information. Display Total Points for 3 Months.
/// </summary>
public class CustomerV2 : ViewModels.Customer
{
    /// <summary>
    /// Total Points earned in last 3 months
    /// </summary>
    public int TotalPointsEarned { get; set; }
    /// <summary>
    /// Total Points earned monthly (Month, Total Points)
    /// </summary>
    public Dictionary<string,int> PointsEarnedMonthly { get; set; }
}