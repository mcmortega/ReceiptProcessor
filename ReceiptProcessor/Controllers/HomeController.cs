using Microsoft.AspNetCore.Mvc;
using ReceiptProcessor.Services;
using ReceiptProcessor.Models;

namespace ReceiptProcessor.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReceiptService _receiptService;

        public HomeController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        public async Task<IActionResult> Index()
        {
            var receipts = await _receiptService.GetAllReceiptsAsync();
            return View(receipts);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null)
            {
                return Json(new { success = false, message = "No file selected" });
            }

            var result = await _receiptService.UploadAndProcessReceiptAsync(file);
            return Json(result);
        }

        public async Task<IActionResult> Details(string id)
        {
            var receipt = await _receiptService.GetReceiptByIdAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }
            return View(receipt);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}