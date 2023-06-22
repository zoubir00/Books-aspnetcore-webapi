using My_Books.Data.Models;
using My_Books.Data.ViewModels;

namespace My_Books.Data.Services
{
    public class AuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add method
        public void AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName
            };
            _context.Authors.Add(_author);
            _context.SaveChanges();
        }
        public AuthorwithBooksVM GetAuthorById(int AuthorId)
        {
            var _authorwithBooks = _context.Authors.Where(a => a.Id == AuthorId).Select(n => new AuthorwithBooksVM()
            {
                FullName=n.FullName,
                BooksTitle = n.Book_Authors.Select(b => b.Book.Title).ToList()
            }).FirstOrDefault();
            return _authorwithBooks;
        }

        public List<Author> GetAllAuthors()
        {
            var _authorList = _context.Authors.ToList();
            return _authorList;
        }
    }
}
