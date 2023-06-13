using My_Books.Data.Models;
using My_Books.Data.ViewModels;
using System.Threading;

namespace My_Books.Data.Services
{
    public class BooksService
    {
        private readonly ApplicationDbContext _context;

        public BooksService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks() => _context.Books.ToList();
        public Book GetBookById(int bookId) => _context.Books.FirstOrDefault(n=>n.Id==bookId)!;

        public Book UpdateBook(int bookId,BookVM book)
        {
            var _book= _context.Books.FirstOrDefault(n => n.Id == bookId)!;
            if (_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate.Value : null;
                _book.Genre = book.Genre;
                _book.Author = book.Author;
                _book.CoverUrl = book.CoverUrl;

                _context.SaveChanges();
            }
            return _book;
        }
        public void DelteBook(int bookId)
        {
            var bookToDelete = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if (bookToDelete != null)
            {
                _context.Books.Remove(bookToDelete);
                _context.SaveChanges();
            }  
        }
        // Delete

        public void Add(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead=book.IsRead ? book.DateRead.Value:null,
                Rate= book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                Author = book.Author,
                CoverUrl = book.CoverUrl,

            };
            _context.Books.Add(_book);
            _context.SaveChanges();
        }
    }
}
