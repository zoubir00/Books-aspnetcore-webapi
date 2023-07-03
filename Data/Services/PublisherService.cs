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


        // get all publishers

        public List<PublisherwithBooksAndAuthorVM> GetPublishers()
        {
            var _publishers = _context.Publishers.Select(p => new PublisherwithBooksAndAuthorVM
            {
                Name = p.Name,
                BookAuthors = p.Books.Select(n => new BookAuthorVM()
                {
                    BookName = n.Title,
                    BookAuthors = n.Book_Authors.Select(b => b.Author.FullName).ToList()
                }).ToList()
            }).ToList();
            return _publishers;
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

        //Delete
        public void DeletePublisher(int Id)
        {
            var publisherToDelete = _context.Publishers.FirstOrDefault(n => n.Id == Id);
            if (publisherToDelete != null)
            {   
                _context.Publishers.Remove(publisherToDelete);
                _context.SaveChanges();
            }
               
        }
    }
}
