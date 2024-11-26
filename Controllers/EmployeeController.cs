using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;
using AgriEnergyConnect.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AgriEnergyConnect.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddFarmer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFarmer(FarmerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var farmer = new Farmer
                {
                    UserName = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address
                };
                _context.Farmers.Add(farmer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewFarmers));
            }
            return View(model);
        }

        public IActionResult ViewFarmers()
        {
            var farmers = _context.Farmers
                .Select(farmer => new FarmerViewModel
                {
                    FarmerId = farmer.Id,
                    Name = farmer.UserName,
                    Email = farmer.Email,
                    PhoneNumber = farmer.PhoneNumber,
                    Address = farmer.Address
                })
                .ToList();

            return View(farmers);
        }

        public IActionResult ViewProducts()
        {
            var products = _context.Products
                .Select(product => new ProductViewModel
                {
                    Name = product.Name,
                    Category = product.Category,
                    ProductionDate = product.ProductionDate
                })
                .ToList();

            return View(products);
        }
        [HttpGet]
        public IActionResult SearchProducts(string productName, string farmerName, string category, DateTime? startDate, DateTime? endDate)
        {
            // Base query
            var query = _context.Products.AsQueryable();

            // Apply filters
            if (!string.IsNullOrWhiteSpace(productName))
            {
                query = query.Where(p => p.Name.Contains(productName));
            }

            if (!string.IsNullOrWhiteSpace(farmerName))
            {
                query = query.Where(p => _context.Farmers
                    .Any(f => f.UserName.Contains(farmerName)));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(p => p.Category == category);
            }

            if (startDate.HasValue)
            {
                query = query.Where(p => p.ProductionDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(p => p.ProductionDate <= endDate.Value);
            }

            // Execute query
            var products = query.Select(p => new ProductViewModel
            {
                Name = p.Name,
                Category = p.Category,
                ProductionDate = p.ProductionDate
            }).ToList();

            return View("ViewProducts", products);
        }

        public IActionResult SearchFarmers(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction(nameof(ViewFarmers));
            }

            var farmers = _context.Farmers
                .Where(f => f.UserName.Contains(query) ||
                            f.Email.Contains(query) ||
                            f.PhoneNumber.Contains(query) ||
                            f.Address.Contains(query))
                .Select(farmer => new FarmerViewModel
                {
                    FarmerId = farmer.Id,
                    Name = farmer.UserName,
                    Email = farmer.Email,
                    PhoneNumber = farmer.PhoneNumber,
                    Address = farmer.Address
                })
                .ToList();

            return View("ViewFarmers", farmers);
        }
    }
}
