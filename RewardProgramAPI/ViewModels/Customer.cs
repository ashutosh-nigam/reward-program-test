using System.ComponentModel.DataAnnotations;

namespace RewardProgramAPI.ViewModels;
/// <summary>
/// Customer View Model To Display Customer Information
/// </summary>
public class Customer
{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
}