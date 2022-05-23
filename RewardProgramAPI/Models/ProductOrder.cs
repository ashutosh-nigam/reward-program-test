using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RewardProgramAPI.Models;

/// <summary>
/// Product Order, FK table to have many to many relation between Product and Orders table
/// </summary>
public class ProductOrder
{
    [Key]
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    [Required,DefaultValue(1)]
    public int Quantity { get; set; }
    public virtual Product Product { get; set; }
    [Key]
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
}