#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaBaladaAqui2.Models;

namespace suaBaladaAqui2.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly suaBaladaAqui2Context _context;

        public UsuariosController(suaBaladaAqui2Context context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosModel = await _context.usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuariosModel == null)
            {
                return NotFound();
            }

            return View(usuariosModel);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Login,Senha,Tipo,DataCadastro")] UsuariosModel usuariosModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuariosModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuariosModel);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosModel = await _context.usuarios.FindAsync(id);
            if (usuariosModel == null)
            {
                return NotFound();
            }
            return View(usuariosModel);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Login,Senha,Tipo,DataCadastro")] UsuariosModel usuariosModel)
        {
            if (id != usuariosModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuariosModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosModelExists(usuariosModel.Id))
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
            return View(usuariosModel);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosModel = await _context.usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuariosModel == null)
            {
                return NotFound();
            }

            return View(usuariosModel);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuariosModel = await _context.usuarios.FindAsync(id);
            _context.usuarios.Remove(usuariosModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosModelExists(int id)
        {
            return _context.usuarios.Any(e => e.Id == id);
        }

    }
}
