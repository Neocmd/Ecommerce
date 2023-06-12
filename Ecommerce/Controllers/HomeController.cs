using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly EcommerceContext _logger;

        public HomeController(EcommerceContext logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return _logger.Prodottis != null ?
                          View(await _logger.Prodottis.ToListAsync()) :
                          Problem("Entity set 'EcommerceContext.Prodottis'  is null.");
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}