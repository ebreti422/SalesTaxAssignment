using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesTaxAssignment.Data;
using SalesTaxAssignment.Models;

namespace SalesTaxAssignment.Controllers
{
    public class BasketItemsController : Controller
    {
        private readonly SalesTaxAssignmentContext _context;

        public BasketItemsController(SalesTaxAssignmentContext context)
        {
            _context = context;
        }

        // GET: BasketItems
        public async Task<IActionResult> Index()
        {
            var currentItems = _context.BasketItem
                .Include(b => b.Product)
                .Where(b => b.BasketKey == "Current");
            return View(await currentItems.ToListAsync());
        }

        // GET: BasketItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentItems = await _context.BasketItem
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currentItems == null)
            {
                return NotFound();
            }

            return View(currentItems);
        }

        // GET: BasketItems/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id");
            return View();
        }

        // POST: BasketItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,Quantity")] BasketItem basketItem)
        {
            if (ModelState.IsValid)
            {
                basketItem.BasketKey = "Current"; // 👈 Force it here
                _context.Add(basketItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", basketItem.ProductId);
            return View(basketItem);
        }

        // GET: BasketItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentItems = await _context.BasketItem.FindAsync(id);
            if (currentItems == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", currentItems.ProductId);
            return View(currentItems);
        }

        // POST: BasketItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Quantity")] BasketItem basketItem)
        {
            if (id != basketItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(basketItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasketItemExists(basketItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", basketItem.ProductId);
            return View(basketItem);
        }

        // GET: BasketItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentItems = await _context.BasketItem
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currentItems == null)
            {
                return NotFound();
            }

            return View(currentItems);
        }

        // POST: BasketItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currentItems = await _context.BasketItem.FindAsync(id);
            if (currentItems != null)
            {
                _context.BasketItem.Remove(currentItems);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BasketItemExists(int id)
        {
            return _context.BasketItem.Any(e => e.Id == id);
        }
    }
}
