using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;
using AgriEnergyConnect.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AgriEnergyConnect.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class FarmerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<FarmerController> _logger;

        public FarmerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<FarmerController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        [Route("Farmer/Index")]
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "Home"; // Highlights "Home" tab
            return View();
        }

        [HttpGet]
        [Route("Farmer/AddProduct")]
        public IActionResult AddProduct()
        {
            ViewData["ActivePage"] = "Products"; // Highlights "Products" tab
            return View();
        }

        [HttpPost]
        [Route("Farmer/AddProduct")]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null)
                {
                    var product = new Product(
                        farmerId: currentUser.Id,
                        name: model.Name,
                        category: model.Category,
                        productionDate: model.ProductionDate
                    );

                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Product added successfully.");
                    return RedirectToAction(nameof(ViewProducts));
                }
                else
                {
                    _logger.LogError("Current user is null.");
                    ModelState.AddModelError("", "User not found.");
                }
            }
            else
            {
                _logger.LogError("Model state is invalid.");
            }
            ViewData["ActivePage"] = "Products";
            return View(model);
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

        [HttpGet]
        [Route("Farmer/ViewProducts")]
        public async Task<IActionResult> ViewProducts()
        {
            ViewData["ActivePage"] = "Products"; // Highlights "Products" tab
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                var products = _context.Products
                    .Where(p => p.FarmerID == currentUser.Id)
                    .Select(p => new ProductViewModel
                    {
                        Name = p.Name,
                        Category = p.Category,
                        ProductionDate = p.ProductionDate
                    })
                    .ToList();
                return View(products);
            }
            else
            {
                _logger.LogError("Current user is null.");
                ModelState.AddModelError("", "User not found.");
            }
            return View();
        }
    }
}
