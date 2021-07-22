using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.ViewModels.Authors;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Authors
        public IActionResult Index(CancellationToken cancellationToken)
        {
            TempData.Keep();
            return View(_context.Authors.AsAsyncEnumerable().WithCancellation(cancellationToken));
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Birthday,Email")] Author author)
        {
            if (ModelState.IsValid)
            {
                author.Id = Guid.NewGuid();
                _context.Add(author);
                await _context.SaveChangesAsync();
                TempData["NewAuthorName"] = author.Name;
                HttpContext.Session.Set("NewAuthorName", System.Text.Encoding.Default.GetBytes(author.Name));
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Birthday,Email")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var author = await _context.Authors.FindAsync(id);
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(Guid id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }

        // GET: Authors/{authorId}/Books
        [Route("[controller]/{id}/Books")]
        public async Task<IActionResult> Books(Guid id)
        {
            List<Book> books = await _context.Books.Include(b => b.Authors).ToListAsync();
            return View(
                books.Select(b =>
                new SelectBook
                {
                    Book = b,
                    isSelected = b.Authors.Any(a => a.Id == id)
                }).ToImmutableList());
        }

        // GET: Authors/{authorId}/Books
        [HttpPost("[controller]/{id}/Books")]
        public async Task<IActionResult> Books(Guid id, IEnumerable<SelectBook> selectBooks)
        {
            var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            author.Books.Clear();
            IEnumerable<Book> selectedList = selectBooks.Where(b => b.isSelected).Select(b => b.Book).ToImmutableList();
            //Attach+book+without+tracking
            foreach (var book in selectedList)
            {
                author.Books.Add(await _context.Books.FirstOrDefaultAsync(p => p.Id == book.Id));
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = id });
        }

    }
}
