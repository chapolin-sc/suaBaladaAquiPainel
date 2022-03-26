#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaBaladaAqui2.Models;

namespace suaBaladaAqui2.Controllers
{
    [Authorize]
    public class EventosController : Controller
    {

        private readonly suaBaladaAqui2Context _context;

        public EventosController(suaBaladaAqui2Context context)
        {
            _context = context;
        }

        // GET: Eventos
        public async Task<IActionResult> Index(int? id)
        {

            var elementosPorPagina = 15; //valor constante
            var elementosIgnorados = 0;

            ViewBag.paginaAtual = 1;

            if(id != null){
                ViewBag.paginaAtual = (int)id;
                elementosIgnorados = (int)(elementosPorPagina * (id - 1));
            }

            ViewBag.numeroBaladas = await _context.eventos.CountAsync();
            ViewBag.numeroPaginas = Math.Ceiling((double)ViewBag.numeroBaladas / (double)elementosPorPagina);

            var query = (from eventos in _context.eventos
                         orderby eventos.DataEvento
                         select eventos
            );

            return View(await query.AsNoTracking().Skip(elementosIgnorados).Take(elementosPorPagina).ToListAsync());
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventosModel = await _context.eventos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventosModel == null)
            {
                return NotFound();
            }

            return View(eventosModel);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Evento1,Imagem,Atracoes,Cidade,Bairro,Endereco,LocalName,DataEvento,FoneContato," +
        "Organizador,Responsavel")] EventosModel eventosModel, IFormFile imagemFoto)
        {
            string imreBase64Dados;
            /*if (ModelState.IsValid)
            {*/
                if(imagemFoto != null)
                {
                    using (MemoryStream ms = new MemoryStream()){
                        await imagemFoto.OpenReadStream().CopyToAsync(ms);
                        imreBase64Dados = Convert.ToBase64String(ms.ToArray());
                        eventosModel.Imagem = string.Format("data:" + imagemFoto.ContentType + ";base64,{0}", imreBase64Dados);
                    }

                    _context.Add(eventosModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            return View();
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventosModel = await _context.eventos.FindAsync(id);
            if (eventosModel == null)
            {
                return NotFound();
            }

            ViewBag.imagemEvento = eventosModel.Imagem;
            return View(eventosModel);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
        [Bind("Id,Evento1,Imagem,Atracoes,Cidade,Bairro,Endereco,LocalName,DataEvento,FoneContato," +
        "Organizador,Responsavel")] EventosModel eventosModel, IFormFile imagemFoto)
        {
            if (id != eventosModel.Id)
            {
                return NotFound();
            }

            string imreBase64Dados;

            if (eventosModel != null){

                 if(imagemFoto != null)
                {
                    using (MemoryStream ms = new MemoryStream()){
                        await imagemFoto.OpenReadStream().CopyToAsync(ms);
                        imreBase64Dados = Convert.ToBase64String(ms.ToArray());
                        eventosModel.Imagem = string.Format("data:" + imagemFoto.ContentType + ";base64,{0}", imreBase64Dados);
                    }
                }
                try
                {
                    _context.Update(eventosModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventosModelExists(eventosModel.Id))
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

            ViewBag.imagemEvento = eventosModel.Imagem;
            return View(eventosModel);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventosModel = await _context.eventos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventosModel == null)
            {
                return NotFound();
            }

            return View(eventosModel);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventosModel = await _context.eventos.FindAsync(id);
            _context.eventos.Remove(eventosModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventosModelExists(int id)
        {
            return _context.eventos.Any(e => e.Id == id);
        }
    }
}
