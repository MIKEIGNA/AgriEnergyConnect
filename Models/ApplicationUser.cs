using Microsoft.AspNetCore.Identity;

namespace AgriEnergyConnect.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int FarmerID { get; set; }
        public string Role { get; set; } = "Farmer";
    }
}
