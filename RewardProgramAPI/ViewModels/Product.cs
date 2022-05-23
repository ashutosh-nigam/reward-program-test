using System.ComponentModel.DataAnnotations;

namespace RewardProgramAPI.ViewModels;
/// <summary>
/// Product View Model to display Product Information with Price Quantity
/// </summary>
public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}