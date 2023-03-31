using dotnet_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ApiContext _DbProdutos;

        public ProdutoController(ApiContext context)
        {
            _DbProdutos = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            return _DbProdutos.Produtos != null ?
                        Ok(await _DbProdutos.Produtos.ToListAsync()) :
                        Problem("Entity set 'Context.Produtos'  is null.");
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _DbProdutos.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _DbProdutos.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            return Ok();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _DbProdutos.Add(produto);
                await _DbProdutos.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _DbProdutos.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _DbProdutos.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _DbProdutos.Update(produto);
                    await _DbProdutos.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
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
            return Ok(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _DbProdutos.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _DbProdutos.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_DbProdutos.Produtos == null)
            {
                return Problem("Entity set 'Context.Produtos'  is null.");
            }
            var produto = await _DbProdutos.Produtos.FindAsync(id);
            if (produto != null)
            {
                _DbProdutos.Produtos.Remove(produto);
            }

            await _DbProdutos.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return (_DbProdutos.Produtos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
