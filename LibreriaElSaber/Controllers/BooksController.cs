using LibreriaElSaber.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaElSaber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly List<Book> _books;

        public BooksController()
        {
            _books = new List<Book>
            {
                new Book { Id = 1, Titulo = "Book 1", Autor = "Author 1" },
                new Book { Id = 2, Titulo = "Book 2", Autor = "Author 2" },
                new Book { Id = 3, Titulo = "Book 3", Autor = "Author 3" }
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return _books;
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _books.Find(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public ActionResult<Book> CreateBook(Book book)
        {
            book.Id = _books.Count + 1;
            _books.Add(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            var book = _books.Find(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            book.Titulo = updatedBook.Titulo;
            book.Autor = updatedBook.Autor;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _books.Find(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            _books.Remove(book);
            return NoContent();
        }
    }
}
