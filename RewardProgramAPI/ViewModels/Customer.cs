using System.ComponentModel.DataAnnotations;

namespace RewardProgramAPI.ViewModels;
/// <summary>
/// Customer View Model To Display Customer Information
/// </summary>
public class Customer
{
        /// <summary>
        /// Customer Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Customer Name
        /// </summary>
        [Required]
        public string Name { get; set; }
}