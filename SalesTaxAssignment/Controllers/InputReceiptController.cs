using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesTaxAssignment.Data;
using SalesTaxAssignment.Services;

namespace SalesTaxAssignment.Controllers
{
    public class InputReceiptController : Controller
    {
        private readonly SalesTaxAssignmentContext _context;
        private readonly IReceiptService _receiptService;

        public InputReceiptController(SalesTaxAssignmentContext context, IReceiptService receiptService)
        {
            _context = context;
            _receiptService = receiptService;
        }

        public IActionResult Index()
        {
            var basketKeys = _context.BasketItem
                .Select(b => b.BasketKey)
                .Distinct()
                .ToList();

            ViewBag.Inputs = new SelectList(basketKeys);
            return View();
        }

        // Explicit route for seeded inputs
        [HttpGet("InputReceipt/ReceiptById")]
        public IActionResult ReceiptById(int inputId)
        {
            var input = _context.Input
                .Include(i => i.BasketItems)
                .ThenInclude(b => b.Product)
                .FirstOrDefault(i => i.Id == inputId);

            if (input == null)
                return NotFound();

            var receipt = _receiptService.GenerateReceipt(input.BasketItems.ToList());
            return View("Receipt", receipt);
        }

        // Explicit route for custom inputs
        [HttpGet("InputReceipt/Receipt")]
        public IActionResult Receipt(string BasketKey)
        {
            if (string.IsNullOrWhiteSpace(BasketKey))
                return NotFound();

            var basketItems = _context.BasketItem
                .Where(b => b.BasketKey == BasketKey)
                .Include(b => b.Product)
                .ToList();

            if (!basketItems.Any())
                return NotFound();

            var receipt = _receiptService.GenerateReceipt(basketItems);
            return View(receipt);
        }
    }
}
