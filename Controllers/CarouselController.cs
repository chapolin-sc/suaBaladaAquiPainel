#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using suaBaladaAqui2.Models;

namespace suaBaladaAqui.Controllers
{
    public class CarouselController : Controller
    {
        private readonly suaBaladaAqui2Context _context;

        public CarouselController(suaBaladaAqui2Context context)
        {
            _context = context;
        }

        // GET: Carousel
        public async Task<IActionResult> Index()
        {
            return View(await _context.carousel.ToListAsync());
        }

        // GET: Carousel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carouselModel = await _context.carousel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carouselModel == null)
            {
                return NotFound();
            }

            return View(carouselModel);
        }

        // GET: Carousel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carousel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imagem,FrasePrincipal,FraseSecundaria,Ativa," +
        "Ordenacao")] CarouselModel carouselModel, IFormFile imagemFoto)
        {  
            
            string imreBase64Dados;

                if(imagemFoto != null)
                {
                    using (MemoryStream ms = new MemoryStream()){
                        await imagemFoto.OpenReadStream().CopyToAsync(ms);
                        imreBase64Dados = Convert.ToBase64String(ms.ToArray());
                        carouselModel.Imagem = string.Format("data:" + imagemFoto.ContentType + ";base64,{0}", imreBase64Dados);
                    }

                    _context.Add(carouselModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            return View(carouselModel);
        }

        // GET: Carousel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carouselModel = await _context.carousel.FindAsync(id);
            if (carouselModel == null)
            {
                return NotFound();
            }

            ViewBag.imagemEvento = carouselModel.Imagem;
            return View(carouselModel);
        }

        // POST: Carousel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imagem,FrasePrincipal,FraseSecundaria,Ativa,Ordenacao")] CarouselModel carouselModel)
        {
            if (id != carouselModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carouselModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarouselModelExists(carouselModel.Id))
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
            return View(carouselModel);
        }

        // GET: Carousel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carouselModel = await _context.carousel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carouselModel == null)
            {
                return NotFound();
            }

            return View(carouselModel);
        }

        // POST: Carousel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carouselModel = await _context.carousel.FindAsync(id);
            _context.carousel.Remove(carouselModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarouselModelExists(int id)
        {
            return _context.carousel.Any(e => e.Id == id);
        }
    }
}
