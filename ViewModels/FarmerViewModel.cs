using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.ViewModels
{
    public class FarmerViewModel
    {
        public int FarmerId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }
}
