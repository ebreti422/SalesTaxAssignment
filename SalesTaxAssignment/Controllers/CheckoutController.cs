using Microsoft.AspNetCore.Mvc;
using SalesTaxAssignment.Services;

namespace SalesTaxAssignment.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IReceiptService _receiptService;

        public CheckoutController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        public IActionResult Index(string basketKey)
        {
            var receipt = _receiptService.GenerateReceipt(basketKey);
            return View(receipt);
        }
    }
}
