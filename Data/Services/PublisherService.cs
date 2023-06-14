using My_Books.Data.Models;
using My_Books.Data.ViewModels;

namespace My_Books.Data.Services
{
    public class PublisherService
    {
        private readonly ApplicationDbContext _context;

        public PublisherService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add Publisher

        public void AddPublisher(PublisherVM publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
        }

        // Get Publisher with authors and Books

        public PublisherwithBooksAndAuthorVM GetPublisherwithBooksAndAuthor(int publisherId)
        {
            var _publisherwithBooksAndAuthor = _context.Publishers.Where(p => p.Id == publisherId).Select(n => new PublisherwithBooksAndAuthorVM()
            {
                Name = n.Name,
                BookAuthors = n.Books.Select(b => new BookAuthorVM()
                {
                    BookName = b.Title,
                    BookAuthors = b.Book_Authors.Select(n => n.Author.FullName).ToList()
                }).ToList()
            }).FirstOrDefault();
            return _publisherwithBooksAndAuthor;
        }
    }
}
