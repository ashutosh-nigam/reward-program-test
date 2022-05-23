namespace RewardProgramAPI.ViewModels;
/// <summary>
/// Reward Points View Model to display Reward Points Information with Customer
/// </summary>
public class RewardPoint
{
    public int TotalRewardPoints { get; set; }
    public Customer Customer { get; set; }
}