using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcWebIdentityA.Context;
using MvcWebIdentityA.Entities;

namespace MvcWebIdentityA.Controllers;

[Authorize]
public class ProdutosController : Controller
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index() => _context.Produtos != null
        ? View(await _context.Produtos.AsNoTracking().ToListAsync())
        : Problem("Entity set 'AppDbContext.Produtos' é null.");

    [Authorize(Policy = "IsFuncionarioClaimAccess")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(m => m.ProdutoId == id);
        return produto == null ? NotFound() : View(produto);
    }

    [Authorize(Policy = "IsFuncionarioClaimAccess")]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProdutoId, Nome, Preco")] Produto produto)
    {
        if (!ModelState.IsValid) return View(produto);

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "IsAdminClaimAccess")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var produto = await _context.Produtos.FindAsync(id);
        return produto == null ? NotFound() : View(produto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProdutoId, Nome, Preco")] Produto produto)
    {
        if (id != produto.ProdutoId) return NotFound();

        if (!ModelState.IsValid) return View(produto);

        try
        {
            _context.Update(produto);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ProdutoExists(produto.ProdutoId)) return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "IsAdminClaimAccess")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Produtos == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(m => m.ProdutoId == id);

        return produto == null ? NotFound() : View(produto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> ProdutoExists(int produtoId) => await _context.Produtos.AnyAsync(e => e.ProdutoId == produtoId);
}
