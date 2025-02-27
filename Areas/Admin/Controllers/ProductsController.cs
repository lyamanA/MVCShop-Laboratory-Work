using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCShop.Areas.Admin.Data;
using MVCShop.Areas.Admin.Models;
using MVCShop.Helpers;

namespace MVCShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        #region Get


        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var appDbContext = await _context.Products.Include(p => p.Category).ToListAsync();

            return View(appDbContext);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        #endregion

        #region Create

    
        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Product product)
        {

            product.ImageUrl = FileManager.UploadFile(product.ImageFile);


            if (ModelState.IsValid)
            {
                Console.WriteLine(product);
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            return View(product);
        }

        #endregion

        #region Edit

    
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            var currentProduct = _context.Products.AsNoTracking().FirstOrDefault(prod => prod.Id == id);
            if (currentProduct is null)
            {
                return NotFound();
            }

            product.ImageUrl = currentProduct.ImageUrl;
            Console.WriteLine(product);
          
            if (ModelState.IsValid)
            {
                try
                {
                    //If photo is changing
                    if (product.ImageFile is not null) 
                    {
                        product.ImageUrl = FileManager.UploadFile(product.ImageFile);
                        FileManager.DeleteFile(currentProduct.ImageUrl);
                    }

                    _context.Products.Update(product);
                    _context.SaveChanges();
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }
        #endregion

        #region Delete

    
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Delete image on product delete
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                FileManager.DeleteFile(product.ImageUrl);
                _context.Products.Remove(product);

            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
