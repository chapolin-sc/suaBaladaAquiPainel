#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaBaladaAqui2.Models;
using suaBaladaAqui2.ServiceModels;
using suaBaladaAqui2.Amazon;

namespace suaBaladaAqui.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly suaBaladaAqui2Context _context;

        public BookController(suaBaladaAqui2Context context)
        {
            _context = context;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            return View(await _context.book.ToListAsync());
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nome,nomeFotografo,data")] BookServiceModel bookServiceModel)
        {            
            if (ModelState.IsValid)
            {
                var bucket = new S3classe();
            
                var nomeBucketAws = NomeBucketTratamento(bookServiceModel.nome);

                if(!await bucket.CriarBucketAsync(nomeBucketAws)){
                    ViewBag.ErroNome = true;
                    return View();
                }else{
                    var bookModel = new BookModel(){
                        Nome = bookServiceModel.nome,
                        Fotografo = bookServiceModel.nomeFotografo,
                        data = bookServiceModel.data,
                        fotos = null,
                        NomeBucketnaAws = nomeBucketAws
                    };

                    _context.Add(bookModel);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(bookServiceModel);
        }

        //Trata os nomes do book para ciração do bucket na aws
        private string NomeBucketTratamento(string nomeBucket){
            var nome = nomeBucket.ToLower();
            nome = nome.Replace(' ', '-');
            nome = "suabaladaaqui-" + nome;
            return nome;
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.book.FindAsync(id);
            if (bookModel == null)
            {
                return NotFound();
            }
            return View(bookModel);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Fotografo,data")] BookModel bookModel)
        {
            if (id != bookModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookModelExists(bookModel.Id))
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
            return View(bookModel);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookModel = await _context.book.FindAsync(id);
            _context.book.Remove(bookModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookModelExists(int id)
        {
            return _context.book.Any(e => e.Id == id);
        }
    }
}
