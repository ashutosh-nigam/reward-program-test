using System;
using System.Collections.Generic;

namespace RewardProgramAPI.ViewModels;

public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Points { get; set; }
    public int NoOfProducts { get; set; }
    public decimal TotalAmount { get; set; }
    public ViewModels.Customer Customer { get; set; }
    public List<ViewModels.Product> Products { get; set; }
}