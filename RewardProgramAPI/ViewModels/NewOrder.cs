using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RewardProgramAPI.ViewModels;

public class NewOrder
{
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public IList<NewProduct> Products { get; set; }
}

public class NewProduct
{
    [Required]
    public int Id { get; set; }
    public int Quantity { get; set; } = 1;
}