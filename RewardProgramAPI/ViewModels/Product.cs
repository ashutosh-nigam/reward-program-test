using System.ComponentModel.DataAnnotations;

namespace RewardProgramAPI.ViewModels;
/// <summary>
/// Product View Model to display Product Information with Price Quantity
/// </summary>
public class Product
{
    /// <summary>
    /// Product Id
    /// </summary>
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// Product Name
    /// </summary>
    [Required]
    public string Name { get; set; }
    /// <summary>
    /// Price of Product
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Quantity
    /// </summary>
    public int Quantity { get; set; }
}