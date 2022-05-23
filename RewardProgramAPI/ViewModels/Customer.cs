using System.ComponentModel.DataAnnotations;

namespace RewardProgramAPI.ViewModels;

public class Customer
{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
}