using System;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        
        [Required]
        public string FarmerID { get; set; } = string.Empty; // Ensure this is always initialized

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public DateTime ProductionDate { get; set; } = DateTime.Now;

        public Farmer? Farmer { get; set; }

        // Default constructor
        public Product()
        {
        }

        // Constructor to initialize all required properties
        public Product(string farmerId, string name, string category, DateTime productionDate)
        {
            FarmerID = farmerId;
            Name = name;
            Category = category;
            ProductionDate = productionDate;
        }
    }
}
