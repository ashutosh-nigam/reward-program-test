using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RewardProgramAPI.ViewModels;

/// <summary>
/// New Order Model used to pass new Order
/// </summary>
public class NewOrder
{
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public IList<ProductInfo> Products { get; set; }
}
/// <summary>
/// ProductInfo for new Orders
/// </summary>
public class ProductInfo
{
    [Required]
    public int Id { get; set; }
    public int Quantity { get; set; } = 1;
}