#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaBaladaAqui2.Models;
using suaBaladaAqui2.Amazon;

namespace suaBaladaAqui.Controllers
{
    public class FotosController : Controller
    {
        private readonly suaBaladaAqui2Context _context;

        public FotosController(suaBaladaAqui2Context context)
        {
            _context = context;
        }

        // GET: Fotos
        public async Task<IActionResult> Index(int? bookid)
        {   
            ViewBag.bookid = bookid;
            var bookResult = await _context.book.Where(b=>b.Id == bookid)
                                        .Select(b => new
                                            {
                                                bookAtual = b,
                                                fotos = b.fotos   
                                            })
                                        .FirstOrDefaultAsync();
            return View(bookResult.fotos);
        }

        // GET: Fotos/Details/5
        public async Task<IActionResult> Details(int? id, int? bookid)
        {
            ViewBag.bookid = bookid;
            if (id == null)
            {
                return NotFound();
            }

            var fotosModel = await _context.fotos
                .FirstOrDefaultAsync(m => m.id == id);
            if (fotosModel == null)
            {
                return NotFound();
            }

            return View(fotosModel);
        }

        // GET: Fotos/Create
        public IActionResult Create(int? bookid)
        {
            ViewBag.bookid = bookid;
            return View();
        }

        // POST: Fotos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Foto,Data,Book")] FotosModel fotosModel, IList<IFormFile> Foto,
        int bookid)
        {
            var bucketS3 = new S3classe();
            BookModel bookAtual = await _context.book.FindAsync(bookid);
            
            if (Foto != null)
            {
                foreach(var f in Foto){
                    var foto = new FotosModel(){
                        Foto = f.FileName,
                        Data = DateTime.Now,
                        Book = bookAtual
                    };                    
                    if(await bucketS3.SalvandoArquivosNoBucketS3Async(f, bookAtual.NomeBucketnaAws, f.FileName))
                    {
                        _context.fotos.Add(foto);
                    }   
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new{ bookid = bookid });
            }
            return View(fotosModel);
        }

        // GET: Fotos/Edit/5
        public async Task<IActionResult> Edit(int? id, int? bookid)
        {
            ViewBag.bookid = bookid;
            if (id == null)
            {
                return NotFound();
            }

            var fotosModel = await _context.fotos.FindAsync(id);
            if (fotosModel == null)
            {
                return NotFound();
            }
            return View(fotosModel);
        }

        // POST: Fotos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Foto,Data")] FotosModel fotosModel)
        {
            if (id != fotosModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fotosModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotosModelExists(fotosModel.id))
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
            return View(fotosModel);
        }

        // GET: Fotos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotosModel = await _context.fotos
                .FirstOrDefaultAsync(m => m.id == id);
            if (fotosModel == null)
            {
                return NotFound();
            }

            return View(fotosModel);
        }

        // POST: Fotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fotosModel = await _context.fotos.FindAsync(id);
            _context.fotos.Remove(fotosModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FotosModelExists(int id)
        {
            return _context.fotos.Any(e => e.id == id);
        }
    }
}
