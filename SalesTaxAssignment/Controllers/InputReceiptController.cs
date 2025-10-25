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
            ViewBag.Inputs = new SelectList(_context.Input, "Id", "Name");
            return View();
        }

        public IActionResult Receipt(int inputId)
        {
            var input = _context.Input
                .Include(i => i.BasketItems)
                .ThenInclude(b => b.Product)
                .FirstOrDefault(i => i.Id == inputId);

            if (input == null) return NotFound();

            var receipt = _receiptService.GenerateReceipt(input.BasketItems.ToList());
            return View(receipt);
        }
    }
}