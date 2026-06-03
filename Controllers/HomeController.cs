using Microsoft.AspNetCore.Mvc;
using PortfolioManagementMvc.Data;
using System.Linq;

namespace PortfolioManagementMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Veri tabanındaki varlıkları çekiyoruz
            var assetList = _context.Assets.ToList();

            // Fiyatları eldeki verilere göre dolduruyoruz
            foreach (var asset in assetList)
            {
                if (asset.AssetSymbol != null)
                {
                    switch (asset.AssetSymbol.ToUpper())
                    {
                        case "GARAN":
                            asset.CurrentPrice = 135.00m;
                            asset.Forecast2028 = 280.00m;
                            break;
                        case "ANSGR":
                            asset.CurrentPrice = 85.00m;
                            asset.Forecast2028 = 190.00m;
                            break;
                        case "ALTINS1":
                            asset.CurrentPrice = 26.50m;
                            asset.Forecast2028 = 55.00m;
                            break;
                        default:
                            asset.CurrentPrice = 50.00m;
                            asset.Forecast2028 = 100.00m;
                            break;
                    }
                }
            }

            return View(assetList);
        }
    }
}