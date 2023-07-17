using My_Books.Data.Models;

namespace My_Books.Data.ViewModels
{
    public class BookFilesVM
    {
        public int Id { get; set; }
        public IFormFile? blobUrl { get; set; }
        public string FileName { get; set; }
        public int BookId { get; set; }
        
    }
}
