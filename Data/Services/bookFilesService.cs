using My_Books.Data.Models;

namespace My_Books.Data.Services
{
    public class bookFilesService
    {
        private readonly ApplicationDbContext _context;
        public bookFilesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<BookFiles> GetBookFiles()
        {
            var _bookFiles = _context.BookFiles.Select(b => new BookFiles
            {
                Id = b.Id,
                FileName = b.FileName,
                blobUrl = b.blobUrl,
                BookId = b.BookId
            }).ToList();
            return _bookFiles;
        }
    }
}
