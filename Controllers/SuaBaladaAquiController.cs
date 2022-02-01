using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaBaladaAqui.Models;
using suaBaladaAqui.ViewsModels;

namespace suaBaladaAqui.Controllers
{
    public class SuaBaladaAquiController : Controller
    {
        private int elementosPorPagina = 9; //valor contante
        private int elementosIgnorados = 0;

        private readonly suaBaladaAquiContext _context;

        public SuaBaladaAquiController(suaBaladaAquiContext context){
            _context = context;
        }

        public async Task<ActionResult> Index(int? id)
        {
        
            ViewBag.paginaAtual = 0;

             if (id != null)
            {
                ViewBag.paginaAtual = (int)(id - 1);
                elementosIgnorados = (int)(elementosPorPagina * (id - 1));
            }

            var query = (from evento in _context.Eventos
                        where evento.DataEvento >= DateTime.Today
                        //(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1)
                        orderby evento.DataEvento
                        select new boxBaladaViewsModels(evento.Evento1, evento.DataEvento.ToString("dd/MM"), evento.Cidade, 
                        evento.LocalName, evento.Imagem ));

            ViewBag.numeroBaladas = await _context.Eventos.CountAsync();
            ViewBag.numeroPaginas = Math.Ceiling((double)ViewBag.numeroBaladas / (double)elementosPorPagina);
                        

            return View(await query.AsNoTracking().Skip(elementosIgnorados).Take(elementosPorPagina).ToListAsync());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pesquisa([Bind("tipo")] string _tipo)
        {

            var tipo = "";
             if (ModelState.IsValid)
             {
                 tipo = _tipo;
             }
            /*if (usuariosModel == null)
            {
                return NotFound();
            }*/
            
            return View();
        }

    }
}