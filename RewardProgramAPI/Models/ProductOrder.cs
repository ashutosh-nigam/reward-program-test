using System.ComponentModel.DataAnnotations;

namespace RewardProgramAPI.Models;

/// <summary>
/// Product Order, FK table to have many to many relation between Product and Orders table
/// </summary>
public class ProductOrder
{
    [Key]
    public int ProductId { get; set; }
    public Product Product { get; set; }
    [Key]
    public int OrderId { get; set; }
    public Order Order { get; set; }
}