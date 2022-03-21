using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaBaladaAqui2.Models;
using suaBaladaAqui2.ViewsModels;

namespace suaBaladaAqui2.Controllers
{
    public class SuaBaladaAqui2Controller : Controller
    {
        private readonly suaBaladaAqui2Context _context;

        public SuaBaladaAqui2Controller(suaBaladaAqui2Context context){
            _context = context;
        }

        public async Task<ActionResult> Index(int? id)
        {
            var elementosPorPagina = 12; //valor constante
            var elementosIgnorados = 0;
            
            ViewBag.paginaAtual = 1;

             if (id != null)
            {
                ViewBag.paginaAtual = (int)id;
                elementosIgnorados = (int)(elementosPorPagina * (id - 1));
            }

            var DtEventosVisiveis = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var query = (from evento in _context.eventos
                        where evento.DataEvento >= DtEventosVisiveis
                        orderby evento.DataEvento
                        select new boxBaladaViewsModels(evento.Evento1, evento.DataEvento.ToString("dd/MM"), evento.Cidade, 
                        evento.LocalName, evento.Imagem));

            ViewBag.numeroBaladas = await query.CountAsync();
            ViewBag.numeroPaginas = Math.Ceiling((double)ViewBag.numeroBaladas / (double)elementosPorPagina);

            var auxBox = await query.AsNoTracking().Skip(elementosIgnorados).Take(elementosPorPagina).ToListAsync();
            var dadosIndex = new principalViewModel(dadosCarousel(), auxBox);
                        
            return View(dadosIndex);
        }


        public List<carouselViewModel> dadosCarousel(){

            var query = ( from carousel in _context.carousel
                          where carousel.Ativa == true
                          orderby carousel.Ordenacao
                          select new carouselViewModel (carousel.Imagem, carousel.FrasePrincipal, 
                          carousel.FraseSecundaria));

            var listaCarousel = query.AsNoTracking().ToList();

            return listaCarousel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pesquisa(string tipo, string pesquisa)
        {
            var elementosPorPagina = 12; //valor constante
            var elementosIgnorados = 0;
            
            try{    

                if(pesquisa == null || pesquisa == ""){
                    return RedirectToAction(nameof(Index));
                }

                var DtEventosVisiveis = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                var query = (from evento in _context.eventos
                            where evento.DataEvento >= DtEventosVisiveis
                            where evento.Evento1.Contains(pesquisa)
                            orderby evento.DataEvento 
                            select new boxBaladaViewsModels(evento.Evento1, evento.DataEvento.ToString("dd/MM"), evento.Cidade, 
                            evento.LocalName, evento.Imagem));
                
                ViewBag.numeroPaginas = 1;  

                var auxBox = await query.AsNoTracking().Skip(elementosIgnorados).Take(elementosPorPagina).ToListAsync();
                var dadosIndex = new principalViewModel(dadosCarousel(), auxBox);
                    
                return View("Index", dadosIndex);

            }catch{
            }
            
            return View();
        }

    }
}


//02/02/2022 21:00:00 > 02/02/2022 05:00:00 