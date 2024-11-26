using System;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.ViewModels
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Product Name is required")]
        public string Name { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Production Date is required")]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }
    }
}
