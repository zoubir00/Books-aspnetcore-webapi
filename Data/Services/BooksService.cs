using My_Books.Data.Models;
using My_Books.Data.ViewModels;
using System.Text.Json.Serialization;
using System.Text.Json;
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

        // Get all books V
        public List<BookWithAuthorsVM> GetAllBooks() 
        {
            var _bookwithAuthors = _context.Books.Select(b => new BookWithAuthorsVM()
            {
                Title = b.Title,
                Description = b.Description,
                IsRead = b.IsRead,
                DateRead = b.IsRead ? b.DateRead.Value : null,
                Rate = b.IsRead ? b.Rate.Value : null,
                Genre = b.Genre,
                CoverUrl = b.CoverUrl,
                publisherName = b.Publisher.Name,
                AuthorsName = b.Book_Authors.Select(ba => ba.Author.FullName).ToList()
            }).ToList();
            return _bookwithAuthors;


        }
        // Get book by Id V

        public BookWithAuthorsVM GetBookById(int bookId)
        {
            var _bookWithAuthors = _context.Books.Where(n=>n.Id==bookId).Select(book => new BookWithAuthorsVM()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                publisherName = book.Publisher.Name,
                AuthorsName = book.Book_Authors.Select(n => n.Author.FullName).ToList()

            }).FirstOrDefault();
            return _bookWithAuthors;
        }

        public Book UpdateBook(int bookId,BookVM book)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(book, options);

            var _book= _context.Books.FirstOrDefault(n => n.Id == bookId)!;
            if (_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate.Value : null;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;
                _book.PublisherId = book.publisherId;
                //// Update author
                //var existAuthors = _context.Book_Authors.Where(n => n.bookId == _book.Id).Select(a => a.AuthorId).ToList();
                //var newAuthorIds = book.AuthorsIds.Except(existAuthors).ToList();
                //var removedAuthorIds = existAuthors.Except(book.AuthorsIds).ToList();

                //foreach (var id in removedAuthorIds)
                //{
                //    var bookAuthorToRemove = _context.Book_Authors.FirstOrDefault(ab => ab.bookId == _book.Id && ab.AuthorId == id);
                //    if (bookAuthorToRemove != null)
                //    {
                //        _context.Book_Authors.Remove(bookAuthorToRemove);
                //    }
                //}

                //foreach (var id in newAuthorIds)
                //{
                //    var _Author_Book = new Book_Authors()
                //    {
                //        bookId = _book.Id,
                //        AuthorId = id
                //    };
                //    _context.Book_Authors.Add(_Author_Book);
                    
                //}
                
                _context.SaveChanges();
            }
            return _book;
        }
        
        // Delete
        public void DelteBook(int bookId)
        {
            var bookToDelete = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if (bookToDelete != null)
            {
                _context.Books.Remove(bookToDelete);
                _context.SaveChanges();
            }  
        }
        
        //Add book V
        public void AddBookWithAuthorsAndPublisher(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead=book.IsRead ? book.DateRead.Value:null,
                Rate= book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherId=book.publisherId

            };
            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach(var id in book.AuthorsIds)
            {
                var _Author_Book = new Book_Authors()
                {
                    bookId = _book.Id,
                    AuthorId = id
                };
                _context.Book_Authors.AddRange(_Author_Book);
                _context.SaveChanges();
            }
        }
    }
}
