using Microsoft.AspNetCore.Mvc;
using SalesTaxAssignment.Data;
using SalesTaxAssignment.Models;

namespace SalesTaxAssignment.Controllers
{
    public class InputController : Controller
    {
        private readonly SalesTaxAssignmentContext _context;

        public InputController(SalesTaxAssignmentContext context)
        {
            _context = context;
        }

        public IActionResult CreateInput()
        {
            var products = _context.Product.ToList();

            var model = new CreateInputViewModel
            {
                Products = products.Select(p => new ProductInputViewModel
                {
                    ProductId = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = 0
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateInput(CreateInputViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.BasketKey))
            {
                ModelState.AddModelError("BasketKey", "BasketKey is required.");
            }

            if (ModelState.IsValid)
            {
                foreach (var item in model.Products.Where(p => p.Quantity > 0))
                {
                    var basketItem = new BasketItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        BasketKey = model.BasketKey
                    };

                    _context.BasketItem.Add(basketItem);
                }

                _context.SaveChanges();
                return RedirectToAction("Index", "InputReceipt");
            }

            return View(model);
        }
    }
}

