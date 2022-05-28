namespace RewardProgramAPI.ViewModels;
/// <summary>
/// Reward Points View Model to display Reward Points Information with Customer
/// </summary>
public class RewardPoint
{
    /// <summary>
    /// Total Reward Points Earned 
    /// </summary>
    public int TotalRewardPoints { get; set; }
    /// <summary>
    /// Customer Information
    /// </summary>
    public Customer Customer { get; set; }
}