using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortfolioManagementMvc.Data;
using PortfolioManagementMvc.Models;

namespace PortfolioManagementMvc.Controllers
{
    public class AssetController : Controller
    {
        private readonly AppDbContext _context;

        public AssetController(AppDbContext context)
        {
            _context = context;
        }

        // SAYFA 1 - Index
        public IActionResult Index(string? search, string? sort)
        {
            var assets = _context.Assets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                assets = assets.Where(a =>
                    a.AssetSymbol.ToLower().Contains(search.ToLower()) ||
                    a.AssetName.ToLower().Contains(search.ToLower()));

            assets = sort switch
            {
                "symbol"      => assets.OrderBy(a => a.AssetSymbol),
                "symbol_desc" => assets.OrderByDescending(a => a.AssetSymbol),
                "year"        => assets.OrderBy(a => a.TargetYear),
                "year_desc"   => assets.OrderByDescending(a => a.TargetYear),
                _             => assets.OrderBy(a => a.AssetId)
            };

            ViewBag.Search = search;
            ViewBag.Sort   = sort;
            return View(assets.ToList());
        }

        // SAYFA 2 - RegisterTransaction GET
        [HttpGet]
        public IActionResult RegisterTransaction(int? assetId)
        {
            ViewBag.AssetList = new SelectList(_context.Assets.ToList(), "AssetId", "AssetSymbol");
            var model = new TransactionModel();
            if (assetId.HasValue)
                model.SelectedAssetId = assetId.Value;
            return View(model);
        }

        // SAYFA 2 - RegisterTransaction POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterTransaction(TransactionModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AssetList = new SelectList(_context.Assets.ToList(), "AssetId", "AssetSymbol");
                return View(model);
            }

            var asset = _context.Assets.Find(model.SelectedAssetId);
            if (asset == null)
            {
                ModelState.AddModelError(string.Empty, "Selected asset was not found.");
                ViewBag.AssetList = new SelectList(_context.Assets.ToList(), "AssetId", "AssetSymbol");
                return View(model);
            }

            _context.Transactions.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Confirmation", new { id = model.TransactionId });
        }

        // SAYFA 3 - Confirmation
        public IActionResult Confirmation(int id)
        {
            var transaction = _context.Transactions
                .Include(x => x.Asset)
                .FirstOrDefault(t => t.TransactionId == id);

            if (transaction == null)
                return NotFound();

            return View(transaction);
        }

        // SAYFA 4 - TransactionsList
        public IActionResult TransactionsList()
        {
            var list = _context.Transactions
                .Include(t => t.Asset)
                .ToList();
            return View(list);
        }

        // SAYFA 5 - DeleteTransaction GET
        [HttpGet]
        public IActionResult DeleteTransaction(int id)
        {
            var transaction = _context.Transactions
                .Include(t => t.Asset)
                .FirstOrDefault(t => t.TransactionId == id);

            if (transaction == null)
                return NotFound();

            return View(transaction);
        }

        // SAYFA 5 - DeleteTransaction POST
        [HttpPost, ActionName("DeleteTransaction")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var transaction = _context.Transactions.Find(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                _context.SaveChanges();
            }
            return RedirectToAction("TransactionsList");
        }
    }
}