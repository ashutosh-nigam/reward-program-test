using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RewardProgramAPI.ViewModels;

/// <summary>
/// New Order Model used to pass new Order
/// </summary>
public class NewOrder
{
    /// <summary>
    /// Customer Id
    /// </summary>
    [Required]
    public int CustomerId { get; set; }
    /// <summary>
    /// List of Products
    /// </summary>
    [Required]
    public IList<ProductInfo> Products { get; set; }
}
/// <summary>
/// ProductInfo for new Orders
/// </summary>
public class ProductInfo
{
    /// <summary>
    /// Product ID
    /// </summary>
    [Required]
    public int Id { get; set; }
    /// <summary>
    /// Order Quantity
    /// </summary>
    public int Quantity { get; set; } = 1;
}