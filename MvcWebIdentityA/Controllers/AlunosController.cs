using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcWebIdentityA.Context;
using MvcWebIdentityA.Entities;

namespace MvcWebIdentityA.Controllers
{
<<<<<<< HEAD
    [Authorize]
=======
    [Authorize(Roles = "Admin")]
>>>>>>> 23dde132088ba3ad476ff30ee0c52779fe2cbcfb
    public class AlunosController : Controller
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

<<<<<<< HEAD
        [AllowAnonymous] // dara acesso a todos aos Usuários, mesmo não estando autenticado
=======
        // GET: Alunos
>>>>>>> 23dde132088ba3ad476ff30ee0c52779fe2cbcfb
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alunos.ToListAsync());
        }

<<<<<<< HEAD
        //[Authorize(Roles = "User, Admin, Gerente")]
        [Authorize(Policy = "RequireUserAdminGerenteRole")]
=======
        // GET: Alunos/Details/5
>>>>>>> 23dde132088ba3ad476ff30ee0c52779fe2cbcfb
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(m => m.AlunoId == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

<<<<<<< HEAD
        //[Authorize(Roles = "User, Admin, Gerente")]
        [Authorize(Policy = "RequireUserAdminGerenteRole")]
=======
        // GET: Alunos/Create
>>>>>>> 23dde132088ba3ad476ff30ee0c52779fe2cbcfb
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alunos/Create
<<<<<<< HEAD
=======
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
>>>>>>> 23dde132088ba3ad476ff30ee0c52779fe2cbcfb
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlunoId,Nome,Email,Idade,Curso")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

<<<<<<< HEAD
        [Authorize(Roles = "Admin, Gerente")]
=======
        // GET: Alunos/Edit/5
>>>>>>> 23dde132088ba3ad476ff30ee0c52779fe2cbcfb
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        // POST: Alunos/Edit/5
<<<<<<< HEAD
=======
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
>>>>>>> 23dde132088ba3ad476ff30ee0c52779fe2cbcfb
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlunoId,Nome,Email,Idade,Curso")] Aluno aluno)
        {
            if (id != aluno.AlunoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.AlunoId))
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
            return View(aluno);
        }

<<<<<<< HEAD
        [Authorize(Roles = " Admin")]
=======
        // GET: Alunos/Delete/5
>>>>>>> 23dde132088ba3ad476ff30ee0c52779fe2cbcfb
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(m => m.AlunoId == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.AlunoId == id);
        }
    }
}
